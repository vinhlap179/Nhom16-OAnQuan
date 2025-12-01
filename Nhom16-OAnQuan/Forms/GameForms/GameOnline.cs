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
        // --- 1. KHAI BÁO BIẾN ---
        private OAnQuanLogic _game;
        private OAnQuanDrawer _drawer;
        private Label[] oVuong = new Label[12];

        // Biến xử lý thao tác
        private int oDaChon = -1;
        private bool dangDiChuyen = false; // Khóa khi animation chạy
        private bool _isMyTurn = false;    // Khóa khi chưa đến lượt

        // Biến Online
        private string _roomId;
        private string _myUID;
        private bool _isHost; // True = P1 (7-11), False = P2 (1-5)
        private FirestoreChangeListener _listener;
        private int _lastProcessedMoveCount = 0;

        // --- 2. KHỞI TẠO (CONSTRUCTOR) ---
        public GameOnline(string roomId, string myUID, bool isHost)
        {
            InitializeComponent();
            _roomId = roomId;
            _myUID = myUID;
            _isHost = isHost; // Nhận vai trò từ WaitingRoom

            _game = new OAnQuanLogic();
            _drawer = new OAnQuanDrawer();

            // Gắn sự kiện cho 2 nút (Dùng chung 1 hàm xử lý)
            btnTrai.Click += btnHuong_Click;
            btnPhai.Click += btnHuong_Click;

            // Định nghĩa hướng: -1 là Trái, 1 là Phải
            btnTrai.Tag = -1;
            btnPhai.Tag = 1;

            this.BackColor = Color.FromArgb(40, 40, 40);
            this.Text = isHost ? $"HOST (P1) - ID: {roomId}" : $"GUEST (P2) - ID: {roomId}";

            // Kết nối sự kiện Load và Đóng form
            this.Load += GameOnline_Load;
            this.FormClosing += GameOnline_FormClosing;
        }

        private void GameOnline_Load(object sender, EventArgs e)
        {
            TaoBanCoUI();       // Vẽ ô
            CapNhatGiaoDien();  // Vẽ điểm
            LangNghePhong();    // Kết nối Firebase
        }

        // --- 3. LOGIC CHECK END GAME & VAY QUÂN (ĐÃ FIX) ---
        private async Task CheckDoiLuotVaKetThuc(bool isP1JustMoved)
        {
            // 1. Kiểm tra kết thúc game
            _game.CheckEndGame();
            if (_game.GameOver)
            {
                CapNhatGiaoDien();

                string winnerName = "";
                if (_game.DiemNguoi1 > _game.DiemNguoi2) winnerName = "CHỦ PHÒNG (P1)";
                else if (_game.DiemNguoi2 > _game.DiemNguoi1) winnerName = "KHÁCH (P2)";
                else winnerName = "HÒA";

                string msg = "";
                if ((_isHost && _game.DiemNguoi1 > _game.DiemNguoi2) || (!_isHost && _game.DiemNguoi2 > _game.DiemNguoi1))
                    msg = "CHÚC MỪNG! BẠN ĐÃ CHIẾN THẮNG! 🏆";
                else if (_game.DiemNguoi1 == _game.DiemNguoi2) msg = "HÒA!";
                else msg = "RẤT TIẾC, BẠN ĐÃ THUA!";

                MessageBox.Show($"{msg}\nNgười thắng: {winnerName}\nTỷ số: P1({_game.DiemNguoi1}) - P2({_game.DiemNguoi2})");

                // Host xóa phòng
                if (_isHost)
                {
                    try { await FirestoreService.DB.Collection("rooms").Document(_roomId).DeleteAsync(); } catch { }
                }

                this.Close();
                return;
            }

            // 2. LOGIC VAY QUÂN (QUAN TRỌNG: ĐẢO NGƯỢC LOGIC)
            // Kiểm tra người SẮP ĐI (người kế tiếp) có cần vay không
            _game.LaLuotNguoiChoi = !isP1JustMoved;

            if (_game.CheckAndBorrowStones())
            {
                CapNhatGiaoDien();
                // Có thể hiện thông báo vay quân ở đây nếu muốn
                await Task.Delay(1000);
            }

            // 3. ĐỔI LƯỢT TRÊN SERVER
            if (_isMyTurn)
            {
                DocumentReference doc = FirestoreService.DB.Collection("rooms").Document(_roomId);
                DocumentSnapshot snap = await doc.GetSnapshotAsync();

                string hostID = snap.GetValue<string>("HostUID");
                string guestID = snap.GetValue<string>("GuestUID");
                string nextUID = (_myUID == hostID) ? guestID : hostID;

                await doc.UpdateAsync("Turn", nextUID);
            }
        }

        // --- 4. ANIMATION ---
        private async Task ThucHienNuocDi(int start, int dir)
        {
            if (start < 0 || start >= 12 || _game.BanCo[start] <= 0) return;

            // Xác định ai đi: 7-11 là P1, 1-5 là P2
            bool isP1Move = (start >= 7 && start <= 11);

            dangDiChuyen = true;
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
                        if (isP1Move) _game.DiemNguoi1 += cap; else _game.DiemNguoi2 += cap;
                        CapNhatGiaoDien(); await Task.Delay(400);

                        int cur = nextEmpty;
                        while (true)
                        {
                            int ePos = _game.NextIndex(cur, dir);
                            int tPos = _game.NextIndex(ePos, dir);
                            if (_game.BanCo[ePos] == 0 && _game.BanCo[tPos] > 0)
                            {
                                int c2 = _game.BanCo[tPos]; _game.BanCo[tPos] = 0;
                                if (isP1Move) _game.DiemNguoi1 += c2; else _game.DiemNguoi2 += c2;
                                CapNhatGiaoDien(); await Task.Delay(400);
                                cur = tPos; continue;
                            }
                            break;
                        }
                    }
                    break;
                }
            }

            dangDiChuyen = false;
            CapNhatGiaoDien();

            // GỌI HÀM CHECK VỚI THÔNG TIN NGƯỜI VỪA ĐI
            await CheckDoiLuotVaKetThuc(isP1Move);
        }

        // --- 5. CÁC HÀM UI & LISTEN ---
        private void LangNghePhong()
        {
            DocumentReference doc = FirestoreService.DB.Collection("rooms").Document(_roomId);
            _listener = doc.Listen(async snapshot =>
            {
                if (!snapshot.Exists) { MessageBox.Show("Phòng hủy!"); this.Close(); return; }

                if (snapshot.TryGetValue("Turn", out string currentTurnUID))
                {
                    this.Invoke((MethodInvoker)delegate {
                        _isMyTurn = (currentTurnUID == _myUID);
                        CapNhatTrangThaiLuot();
                    });
                }

                if (snapshot.TryGetValue("Status", out string status))
                {
                    if (status == "PlayerLeft")
                    {
                        MessageBox.Show("Đối thủ đã thoát! Bạn thắng.");
                        this.Close();
                    }
                }

                if (snapshot.TryGetValue("MoveCount", out int moveCount))
                {
                    if (moveCount > _lastProcessedMoveCount)
                    {
                        _lastProcessedMoveCount = moveCount;
                        int start = snapshot.GetValue<int>("LastStart");
                        int dir = snapshot.GetValue<int>("LastDir");

                        this.Invoke((MethodInvoker)async delegate {
                            await ThucHienNuocDi(start, dir);
                        });
                    }
                }
            });
        }

        private async void btnHuong_Click(object sender, EventArgs e)
        {
            if (oDaChon < 0 || !_isMyTurn || dangDiChuyen ) return;

            int dir = (int)(sender as Button).Tag;
            int start = oDaChon;

            oDaChon = -1;
            btnTrai.Visible = btnPhai.Visible = false;
            dangDiChuyen = true;

            DocumentReference doc = FirestoreService.DB.Collection("rooms").Document(_roomId);
            Dictionary<string, object> updates = new Dictionary<string, object>
            {
                { "LastStart", start },
                { "LastDir", dir },
                { "MoveCount", _lastProcessedMoveCount + 1 }
            };
            await doc.UpdateAsync(updates);
        }

        // --- 6. HỖ TRỢ GIAO DIỆN ---
        private bool IsMySide(int index)
        {
            if (_isHost && index >= 7 && index <= 11) return true;
            if (!_isHost && index >= 1 && index <= 5) return true;
            return false;
        }

        private (int col, int row) GetUiCoordinates(int index)
        {
            int col = 0, row = 0;
            if (index == 0) { col = 0; row = 1; }
            else if (index == 6) { col = 6; row = 1; }
            else if (index >= 1 && index <= 5) { col = index; row = 0; }
            else if (index >= 7 && index <= 11) { col = index - 6; row = 2; }

            if (!_isHost) { col = 6 - col; row = 2 - row; }
            return (col, row);
        }

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

                if (IsMySide(i)) { lbl.Cursor = Cursors.Hand; lbl.Click += oDan_Click; }
                else lbl.Cursor = Cursors.Default;

                oVuong[i] = lbl;
                var pos = GetUiCoordinates(i);
                tblBanCo.Controls.Add(lbl, pos.col, pos.row);
            }
        }

        private void CapNhatGiaoDien()
        {
            for (int i = 0; i < 12; i++) if (oVuong[i] != null) oVuong[i].Invalidate();

            if (_isHost)
            {
                lblDiemNguoi1.Text = $"BẠN (P1): {_game.DiemNguoi1}";
                lblDiemNguoi2.Text = $"KHÁCH (P2): {_game.DiemNguoi2}";
            }
            else
            {
                lblDiemNguoi1.Text = $"CHỦ (P1): {_game.DiemNguoi1}";
                lblDiemNguoi2.Text = $"BẠN (P2): {_game.DiemNguoi2}";
            }
            CapNhatTrangThaiLuot();
        }

        private void CapNhatTrangThaiLuot()
        {
            lblThongBao.Text = _isMyTurn ? "ĐẾN LƯỢT BẠN!" : "Đợi đối thủ...";
            lblThongBao.ForeColor = _isMyTurn ? Color.Lime : Color.Yellow;

            bool allowClick = _isMyTurn && !dangDiChuyen;
            for (int i = 0; i < 12; i++)
            {
                if (IsMySide(i)) oVuong[i].Enabled = allowClick && _game.BanCo[i] > 0;
                else oVuong[i].Enabled = false;
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

        private void oDan_Click(object sender, EventArgs e)
        {
            if (!_isMyTurn || dangDiChuyen) return;
            int index = (int)(sender as Label).Tag;
            if (!IsMySide(index) || _game.BanCo[index] == 0) return;

            int old = oDaChon; oDaChon = index;
            if (old != -1) oVuong[old].Invalidate();
            oVuong[oDaChon].Invalidate();

            try
            {
                var pt = tblBanCo.PointToScreen(oVuong[oDaChon].Location);
                pt = this.PointToClient(pt);
                btnTrai.Location = new Point(pt.X - btnTrai.Width - 5, pt.Y + 20);
                btnPhai.Location = new Point(pt.X + oVuong[oDaChon].Width + 5, pt.Y + 20);
            }
            catch { }
            CapNhatTrangThaiLuot();
        }

        private async void GameOnline_FormClosing(object sender, FormClosingEventArgs e)
        {
            _listener?.StopAsync();
            try
            {
                // Báo cho đối thủ biết mình thoát
                DocumentReference doc = FirestoreService.DB.Collection("rooms").Document(_roomId);
                await doc.UpdateAsync("Status", "PlayerLeft");
            }
            catch { }
        }
    }
}