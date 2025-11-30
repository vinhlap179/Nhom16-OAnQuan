using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Cloud.Firestore;
using Nhom16_OAnQuan.Classes;

namespace Nhom16_OAnQuan.Forms.GameForms
{
    public partial class GameOnline : Form
    {
        // --- LOGIC & UI ---
        private OAnQuanLogic _game;
        private OAnQuanDrawer _drawer;
        private Label[] oVuong = new Label[12];
        private int oDaChon = -1;

        // --- BIẾN TRẠNG THÁI (QUAN TRỌNG) ---
        private bool _isAnimating = false; // Đang chạy hiệu ứng -> Khóa tất cả
        private bool _isMyTurn = false;    // Có phải lượt mình không?

        // --- BIẾN ONLINE ---
        private string _roomId;
        private string _myUID;
        private bool _isHost;
        private FirestoreChangeListener _listener;
        private int _lastProcessedMoveCount = 0;

        public GameOnline(string roomId, string myUID, bool isHost)
        {
            InitializeComponent();
            _roomId = roomId;
            _myUID = myUID;
            _isHost = isHost;

            _game = new OAnQuanLogic();
            _drawer = new OAnQuanDrawer();

            btnTrai.Click += btnHuong_Click;
            btnPhai.Click += btnHuong_Click;

            // Cố định hướng logic: -1 (Trái), 1 (Phải)
            btnTrai.Tag = -1;
            btnPhai.Tag = 1;

            this.BackColor = Color.FromArgb(40, 40, 40);
            this.Text = isHost ? $"HOST (P1) - ID: {roomId}" : $"GUEST (P2) - ID: {roomId}";

            this.Load += GameOnline_Load;
            this.FormClosing += GameOnline_FormClosing;
        }

        private void GameOnline_Load(object sender, EventArgs e)
        {
            TaoBanCoUI();
            CapNhatGiaoDien();
            LangNghePhong();
        }

        // --- 1. KIỂM TRA QUYỀN SỞ HỮU Ô (MỚI) ---
        private bool IsMySide(int index)
        {
            // Host sở hữu 7-11
            if (_isHost && index >= 7 && index <= 11) return true;
            // Guest sở hữu 1-5
            if (!_isHost && index >= 1 && index <= 5) return true;

            return false;
        }

        // --- 2. TÍNH TOÁN VỊ TRÍ HIỂN THỊ ---
        private (int col, int row) GetUiCoordinates(int index)
        {
            int col = 0, row = 0;
            // Vị trí chuẩn HOST
            if (index == 0) { col = 0; row = 1; }
            else if (index == 6) { col = 6; row = 1; }
            else if (index >= 1 && index <= 5) { col = index; row = 0; }
            else if (index >= 7 && index <= 11) { col = index - 6; row = 2; }

            // GUEST xoay 180 độ
            if (!_isHost) { col = 6 - col; row = 2 - row; }
            return (col, row);
        }

        // --- 3. TẠO GIAO DIỆN ---
        private void TaoBanCoUI()
        {
            tblBanCo.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tblBanCo.Controls.Clear();
            tblBanCo.BackColor = Color.Transparent;

            for (int i = 0; i < 12; i++)
            {
                var lbl = new Label
                {
                    Tag = i,
                    Text = "",
                    BorderStyle = BorderStyle.None,
                    Dock = DockStyle.Fill,
                    Margin = new Padding(5),
                    BackColor = Color.Transparent
                };
                lbl.Paint += oVuong_Paint;

                // Gắn sự kiện click cho TẤT CẢ các ô Dân (để hàm Click tự lọc)
                if ((i >= 1 && i <= 5) || (i >= 7 && i <= 11))
                {
                    lbl.Cursor = Cursors.Hand;
                    lbl.Click += oDan_Click;
                }
                else
                {
                    lbl.Cursor = Cursors.Default;
                }

                oVuong[i] = lbl;
                var pos = GetUiCoordinates(i);
                tblBanCo.Controls.Add(lbl, pos.col, pos.row);
            }
        }

        // --- 4. LẮNG NGHE FIREBASE ---
        private void LangNghePhong()
        {
            DocumentReference doc = FirestoreService.DB.Collection("rooms").Document(_roomId);
            _listener = doc.Listen(async snapshot =>
            {
                if (!snapshot.Exists) { MessageBox.Show("Phòng hủy!"); this.Close(); return; }

                // A. Cập nhật lượt
                if (snapshot.TryGetValue("Turn", out string currentTurnUID))
                {
                    this.Invoke((MethodInvoker)delegate {
                        // Nếu UID trên server == UID của mình -> Lượt mình
                        _isMyTurn = (currentTurnUID == _myUID);
                        CapNhatTrangThaiLuot();
                    });
                }

                // B. Cập nhật nước đi
                if (snapshot.TryGetValue("MoveCount", out int moveCount))
                {
                    if (moveCount > _lastProcessedMoveCount)
                    {
                        _lastProcessedMoveCount = moveCount;
                        int start = snapshot.GetValue<int>("LastStart");
                        int dir = snapshot.GetValue<int>("LastDir");

                        this.Invoke((MethodInvoker)async delegate {
                            await ThucHienNuocDi(start, dir);
                            CheckDoiLuotVaKetThuc();
                        });
                    }
                }
            });
        }

        // --- 5. SỰ KIỆN CLICK (LOGIC KHÓA LƯỢT CHẶT CHẼ) ---
        private void oDan_Click(object sender, EventArgs e)
        {
            // 1. Nếu không phải lượt mình -> CÚT
            if (!_isMyTurn) return;

            // 2. Nếu đang chạy hiệu ứng -> CÚT
            if (_isAnimating) return;

            int index = (int)(sender as Label).Tag;

            // 3. Nếu bấm vào ô không phải của mình -> CÚT
            if (!IsMySide(index)) return;

            // 4. Nếu ô trống -> CÚT
            if (_game.BanCo[index] == 0) return;

            // --- HỢP LỆ ---
            int old = oDaChon;
            oDaChon = index;
            if (old != -1) oVuong[old].Invalidate();
            oVuong[oDaChon].Invalidate();

            // Hiện nút
            try
            {
                var pt = tblBanCo.PointToScreen(oVuong[oDaChon].Location);
                pt = this.PointToClient(pt);
                btnTrai.Location = new Point(pt.X - btnTrai.Width - 5, pt.Y + 20);
                btnPhai.Location = new Point(pt.X + oVuong[oDaChon].Width + 5, pt.Y + 20);
            }
            catch { }

            CapNhatGiaoDien();
        }

        private async void btnHuong_Click(object sender, EventArgs e)
        {
            if (oDaChon < 0) return;
            if (!_isMyTurn || _isAnimating) return;

            int dir = (int)(sender as Button).Tag;
            // Guest bị xoay 180 độ nên nút Phải màn hình lại là Trái logic
            if (!_isHost) dir = -dir;

            int start = oDaChon;

            // Khóa ngay lập tức
            oDaChon = -1;
            btnTrai.Visible = btnPhai.Visible = false;
            _isAnimating = true; // KHÓA ANIMATION

            // Gửi Server
            DocumentReference doc = FirestoreService.DB.Collection("rooms").Document(_roomId);
            Dictionary<string, object> updates = new Dictionary<string, object>
            {
                { "LastStart", start },
                { "LastDir", dir },
                { "MoveCount", _lastProcessedMoveCount + 1 }
            };
            await doc.UpdateAsync(updates);
        }

        // --- 6. XỬ LÝ LOGIC CHẠY QUÂN ---
        private async Task ThucHienNuocDi(int start, int dir)
        {
            // Xác định ai đi để cộng điểm
            bool isHostMove = (start >= 7 && start <= 11);

            _isAnimating = true; // Đảm bảo khóa khi nhận lệnh từ server
            btnTrai.Visible = btnPhai.Visible = false;

            int stones = _game.BanCo[start];
            _game.BanCo[start] = 0;
            CapNhatGiaoDien(); await Task.Delay(250);

            int pos = start;
            while (stones > 0)
            {
                pos = _game.NextIndex(pos, dir);
                _game.BanCo[pos]++;
                CapNhatGiaoDien(); await Task.Delay(250);
                stones--;
            }

            while (true)
            {
                int next = _game.NextIndex(pos, dir);
                if (_game.IsQuan(next)) break;

                if (_game.BanCo[next] > 0)
                {
                    stones = _game.BanCo[next]; _game.BanCo[next] = 0; pos = next;
                    CapNhatGiaoDien(); await Task.Delay(250);
                    while (stones > 0)
                    {
                        pos = _game.NextIndex(pos, dir); _game.BanCo[pos]++;
                        CapNhatGiaoDien(); await Task.Delay(250);
                        stones--;
                    }
                }
                else
                {
                    int nextEmpty = _game.NextIndex(next, dir);
                    if (_game.BanCo[nextEmpty] > 0)
                    {
                        int cap = _game.BanCo[nextEmpty]; _game.BanCo[nextEmpty] = 0;

                        if (isHostMove) _game.DiemNguoi1 += cap;
                        else _game.DiemNguoi2 += cap;

                        CapNhatGiaoDien(); await Task.Delay(400);

                        int cur = nextEmpty;
                        while (true)
                        {
                            int ePos = _game.NextIndex(cur, dir);
                            int tPos = _game.NextIndex(ePos, dir);
                            if (_game.BanCo[ePos] == 0 && _game.BanCo[tPos] > 0)
                            {
                                int c2 = _game.BanCo[tPos]; _game.BanCo[tPos] = 0;
                                if (isHostMove) _game.DiemNguoi1 += c2;
                                else _game.DiemNguoi2 += c2;
                                CapNhatGiaoDien(); await Task.Delay(400);
                                cur = tPos; continue;
                            }
                            break;
                        }
                    }
                    break;
                }
            }
            _isAnimating = false; // Mở khóa animation (nhưng vẫn phụ thuộc isMyTurn)
            CapNhatGiaoDien();
        }

        private async void CheckDoiLuotVaKetThuc()
        {
            _game.CheckEndGame();
            if (_game.GameOver)
            {
                CapNhatGiaoDien();
                MessageBox.Show($"Kết thúc! P1: {_game.DiemNguoi1} - P2: {_game.DiemNguoi2}");
                this.Close(); return;
            }

            if (_game.CheckAndBorrowStones())
            {
                CapNhatGiaoDien(); await Task.Delay(1000);
            }

            // Chỉ người vừa đi mới đổi lượt trên Server
            if (_isMyTurn)
            {
                DocumentReference doc = FirestoreService.DB.Collection("rooms").Document(_roomId);
                DocumentSnapshot snap = await doc.GetSnapshotAsync();
                string hostID = snap.GetValue<string>("HostUID");
                string guestID = snap.GetValue<string>("GuestUID");
                string next = (_myUID == hostID) ? guestID : hostID;
                await doc.UpdateAsync("Turn", next);
            }
        }

        // --- 7. UI UTILS ---
        private void CapNhatGiaoDien()
        {
            for (int i = 0; i < 12; i++) if (oVuong[i] != null) oVuong[i].Invalidate();

            if (_isHost)
            {
                lblDiemNguoi1.Text = $"BẠN (P1): {_game.DiemNguoi1}";
                lblDiemNguoi2.Text = $"ĐỐI THỦ (P2): {_game.DiemNguoi2}";
            }
            else
            {
                lblDiemNguoi1.Text = $"ĐỐI THỦ (P1): {_game.DiemNguoi1}";
                lblDiemNguoi2.Text = $"BẠN (P2): {_game.DiemNguoi2}";
            }
            CapNhatTrangThaiLuot();
        }

        private void CapNhatTrangThaiLuot()
        {
            lblThongBao.Text = _isMyTurn ? "ĐẾN LƯỢT BẠN!" : "Đợi đối thủ...";
            lblThongBao.ForeColor = _isMyTurn ? Color.Lime : Color.Yellow;

            // Khóa/Mở các ô của mình
            bool allowClick = _isMyTurn && !_isAnimating;

            for (int i = 0; i < 12; i++)
            {
                if (IsMySide(i)) oVuong[i].Enabled = allowClick && _game.BanCo[i] > 0;
                else oVuong[i].Enabled = false; // Luôn khóa ô đối thủ
            }

            bool showBtn = allowClick && (oDaChon >= 0);
            btnTrai.Visible = btnPhai.Visible = showBtn;
        }

        private void oVuong_Paint(object sender, PaintEventArgs e)
        {
            Label lbl = sender as Label;
            int index = (int)lbl.Tag;
            _drawer.DrawCell(e.Graphics, lbl.ClientRectangle, index, _game.BanCo[index], (index == oDaChon));
        }

        private void GameOnline_FormClosing(object sender, FormClosingEventArgs e)
        {
            _listener?.StopAsync();
        }
    }
}