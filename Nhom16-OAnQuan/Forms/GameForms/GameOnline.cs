using System;
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

        // --- BIẾN TRẠNG THÁI ---
        private bool _isAnimating = false; // Đang chạy hiệu ứng
        private bool _isMyTurn = false;    // Đến lượt mình chưa?

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

            // Tag mặc định: -1 (Trái/CCW), 1 (Phải/CW)
            // Lát nữa trong sự kiện Click ta sẽ xử lý đảo chiều cho Guest sau
            btnTrai.Tag = -1;
            btnPhai.Tag = 1;

            this.BackColor = Color.FromArgb(40, 40, 40);
            this.Text = isHost ? "HOST (Bạn đi hàng 7-11)" : "GUEST (Bạn đi hàng 1-5)";

            // KẾT NỐI SỰ KIỆN LOAD (QUAN TRỌNG)
            this.Load += GameOnline_Load;
            this.FormClosing += GameOnline_FormClosing;
        }

        private void GameOnline_Load(object sender, EventArgs e)
        {
            TaoBanCoUI();
            CapNhatGiaoDien();
            LangNghePhong();
        }

        // --- 1. TẠO UI & XOAY BÀN CỜ ---

        // Hàm tính toán vị trí hiển thị (Guest sẽ được xoay 180 độ)
        private (int col, int row) GetUiCoordinates(int index)
        {
            int col = 0, row = 0;
            // B1: Tính vị trí chuẩn cho Host
            if (index == 0) { col = 0; row = 1; }
            else if (index == 6) { col = 6; row = 1; }
            else if (index >= 1 && index <= 5) { col = index; row = 0; }
            else if (index >= 7 && index <= 11) { col = index - 6; row = 2; }

            // B2: Nếu là Guest -> Xoay ngược lại
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

                // Phân quyền click: Host bấm 7-11, Guest bấm 1-5
                bool isMyRow = _isHost ? (i >= 7 && i <= 11) : (i >= 1 && i <= 5);
                if (isMyRow) { lbl.Cursor = Cursors.Hand; lbl.Click += oDan_Click; }
                else lbl.Cursor = Cursors.Default;

                oVuong[i] = lbl;

                // Đặt vào lưới theo tọa độ đã xoay
                var pos = GetUiCoordinates(i);
                tblBanCo.Controls.Add(lbl, pos.col, pos.row);
            }
        }

        // --- 2. LẮNG NGHE DỮ LIỆU TỪ FIREBASE ---
        private void LangNghePhong()
        {
            DocumentReference doc = FirestoreService.DB.Collection("rooms").Document(_roomId);
            _listener = doc.Listen(async snapshot =>
            {
                if (!snapshot.Exists)
                {
                    MessageBox.Show("Phòng đã bị hủy!");
                    this.Close(); return;
                }

                // A. Xử lý Lượt
                if (snapshot.TryGetValue("Turn", out string currentTurnUID))
                {
                    this.Invoke((MethodInvoker)delegate {
                        _isMyTurn = (currentTurnUID == _myUID);
                        CapNhatTrangThaiLuot();
                    });
                }

                // B. Xử lý Nước đi mới
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

        // --- 3. GỬI NƯỚC ĐI (ĐÃ SỬA LOGIC HƯỚNG CHO GUEST) ---
        private async void btnHuong_Click(object sender, EventArgs e)
        {
            if (oDaChon < 0) return;
            if (!_isMyTurn || _isAnimating) return;

            // Lấy hướng từ Tag (-1 hoặc 1)
            int dir = (int)(sender as Button).Tag;

            // --- QUAN TRỌNG: ĐẢO HƯỚNG CHO GUEST ---
            // Vì Guest bị xoay bàn cờ 180 độ, nên "Trái" trên màn hình lại là "Phải" trong Logic mảng
            if (!_isHost) dir = -dir;
            // ----------------------------------------

            int start = oDaChon;

            // Khóa UI
            oDaChon = -1;
            btnTrai.Visible = btnPhai.Visible = false;
            _isAnimating = true;

            // Gửi lên Server
            DocumentReference doc = FirestoreService.DB.Collection("rooms").Document(_roomId);
            Dictionary<string, object> updates = new Dictionary<string, object>
            {
                { "LastStart", start },
                { "LastDir", dir },
                { "MoveCount", _lastProcessedMoveCount + 1 }
            };
            await doc.UpdateAsync(updates);
        }

        // --- 4. HÀM ANIMATION & TÍNH ĐIỂM (ĐÃ FIX LỖI ĐIỂM) ---
        private async Task ThucHienNuocDi(int start, int dir)
        {
            if (start < 0 || start >= 12 || _game.BanCo[start] <= 0) return;

            // --- XÁC ĐỊNH AI LÀ NGƯỜI ĐI ĐỂ CỘNG ĐIỂM ---
            // Nếu start thuộc 7-11 -> Là Host (P1) đi -> Cộng điểm vào DiemNguoi1
            // Nếu start thuộc 1-5  -> Là Guest (P2) đi -> Cộng điểm vào DiemNguoi2
            bool isHostMove = (start >= 7 && start <= 11);
            // ---------------------------------------------

            _isAnimating = true;
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

                        // --- CỘNG ĐIỂM DỰA VÀO NGƯỜI ĐI ---
                        if (isHostMove) _game.DiemNguoi1 += cap;
                        else _game.DiemNguoi2 += cap;
                        // ----------------------------------

                        CapNhatGiaoDien(); await Task.Delay(400);

                        int cur = nextEmpty;
                        while (true)
                        {
                            int ePos = _game.NextIndex(cur, dir);
                            int tPos = _game.NextIndex(ePos, dir);
                            if (_game.BanCo[ePos] == 0 && _game.BanCo[tPos] > 0)
                            {
                                int c2 = _game.BanCo[tPos]; _game.BanCo[tPos] = 0;

                                // --- CỘNG ĐIỂM CHUỖI ---
                                if (isHostMove) _game.DiemNguoi1 += c2;
                                else _game.DiemNguoi2 += c2;
                                // -----------------------

                                CapNhatGiaoDien(); await Task.Delay(400);
                                cur = tPos; continue;
                            }
                            break;
                        }
                    }
                    break;
                }
            }
            _isAnimating = false;
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

            // Chỉ người vừa đi mới gửi lệnh đổi lượt
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

        // --- 5. UI UTILS ---
        private void CapNhatGiaoDien()
        {
            for (int i = 0; i < 12; i++) if (oVuong[i] != null) oVuong[i].Invalidate();

            // Hiển thị điểm rõ ràng: BẠN vs ĐỐI THỦ
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

            bool showButtons = _isMyTurn && !_isAnimating && (oDaChon >= 0);
            btnTrai.Visible = btnPhai.Visible = showButtons;
        }

        private void oVuong_Paint(object sender, PaintEventArgs e)
        {
            Label lbl = sender as Label;
            int index = (int)lbl.Tag;
            _drawer.DrawCell(e.Graphics, lbl.ClientRectangle, index, _game.BanCo[index], (index == oDaChon));
        }

        private void oDan_Click(object sender, EventArgs e)
        {
            if (!_isMyTurn || _isAnimating) return; // Chặn click tuyệt đối

            int index = (int)(sender as Label).Tag;
            if (_game.BanCo[index] == 0) return;

            int old = oDaChon;
            oDaChon = index;
            if (old != -1) oVuong[old].Invalidate();
            oVuong[oDaChon].Invalidate();

            try
            {
                var pt = tblBanCo.PointToScreen(oVuong[oDaChon].Location);
                pt = this.PointToClient(pt);
                // Nút luôn hiện dưới ô đã chọn (vì đã xoay bàn cờ)
                btnTrai.Location = new Point(pt.X - btnTrai.Width - 5, pt.Y + 20);
                btnPhai.Location = new Point(pt.X + oVuong[oDaChon].Width + 5, pt.Y + 20);
            }
            catch { }

            CapNhatGiaoDien();
        }

        private void GameOnline_FormClosing(object sender, FormClosingEventArgs e)
        {
            _listener?.StopAsync();
        }
    }
}