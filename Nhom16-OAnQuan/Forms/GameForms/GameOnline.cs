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
            this.Text = isHost ? "HOST (Bạn là P1 - Hàng Dưới)" : "GUEST (Bạn là P2 - Hàng Dưới [Đã xoay])";

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

        // --- 3. LOGIC XOAY BÀN CỜ (QUAN TRỌNG NHẤT) ---
        // Hàm này quyết định ô nào nằm ở đâu trên màn hình
        private (int col, int row) GetUiCoordinates(int index)
        {
            int col = 0, row = 0;

            // BƯỚC A: Tính vị trí mặc định (Góc nhìn của HOST)
            if (index == 0) { col = 0; row = 1; }      // Quan 0 (Trái)
            else if (index == 6) { col = 6; row = 1; } // Quan 6 (Phải)
            else if (index >= 1 && index <= 5) { col = index; row = 0; }     // Hàng trên (Bot/P2)
            else if (index >= 7 && index <= 11) { col = index - 6; row = 2; } // Hàng dưới (P1)

            // BƯỚC B: Nếu là GUEST -> Xoay ngược bàn cờ 180 độ
            // Để Guest cũng thấy hàng của mình (1-5) nằm ở dưới
            if (!_isHost)
            {
                col = 6 - col;
                row = 2 - row;
            }

            return (col, row);
        }

        // --- 4. TẠO GIAO DIỆN ---
        private void TaoBanCoUI()
        {
            tblBanCo.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tblBanCo.Controls.Clear();
            tblBanCo.BackColor = Color.Transparent;

            for (int i = 0; i < 12; i++)
            {
                var lbl = new Label
                {
                    Tag = i, // Tag luôn lưu Index thật (0-11) trong mảng Logic
                    Text = "",
                    BorderStyle = BorderStyle.None,
                    Dock = DockStyle.Fill,
                    Margin = new Padding(5),
                    BackColor = Color.Transparent
                };
                lbl.Paint += oVuong_Paint; // Gắn hàm vẽ

                // --- PHÂN QUYỀN CLICK ---
                // Host chỉ được bấm 7-11. Guest chỉ được bấm 1-5.
                bool isMyRow = false;
                if (_isHost && i >= 7 && i <= 11) isMyRow = true;
                if (!_isHost && i >= 1 && i <= 5) isMyRow = true;

                if (isMyRow)
                {
                    lbl.Cursor = Cursors.Hand;
                    lbl.Click += oDan_Click;
                }
                else
                {
                    lbl.Cursor = Cursors.Default;
                }

                oVuong[i] = lbl; // Lưu lại để dùng sau

                // Đặt vào TableLayout theo tọa độ đã tính toán (có xoay)
                var pos = GetUiCoordinates(i);
                tblBanCo.Controls.Add(lbl, pos.col, pos.row);
            }
        }

        // --- 5. LẮNG NGHE FIREBASE (REALTIME) ---
        private void LangNghePhong()
        {
            DocumentReference doc = FirestoreService.DB.Collection("rooms").Document(_roomId);
            _listener = doc.Listen(async snapshot =>
            {
                if (!snapshot.Exists) { MessageBox.Show("Phòng hủy!"); this.Close(); return; }

                // A. Cập nhật lượt đi
                if (snapshot.TryGetValue("Turn", out string currentTurnUID))
                {
                    this.Invoke((MethodInvoker)delegate {
                        // Nếu UID trên mạng trùng UID mình -> Lượt mình
                        _isMyTurn = (currentTurnUID == _myUID);
                        CapNhatTrangThaiLuot();
                    });
                }

                // B. Nhận nước đi từ đối thủ
                if (snapshot.TryGetValue("MoveCount", out int moveCount))
                {
                    // Nếu số lượt đi tăng lên -> Có người vừa đi
                    if (moveCount > _lastProcessedMoveCount)
                    {
                        _lastProcessedMoveCount = moveCount;
                        int start = snapshot.GetValue<int>("LastStart");
                        int dir = snapshot.GetValue<int>("LastDir");

                        // Chạy Animation trên máy mình (Dù là mình đi hay nó đi đều chạy)
                        this.Invoke((MethodInvoker)async delegate {
                            await ThucHienNuocDi(start, dir);
                            CheckSauNuocDi(); // Kiểm tra kết thúc/đổi lượt
                        });
                    }
                }
            });
        }

        // --- 6. SỰ KIỆN CLICK (GỬI LỆNH ĐI) ---
        private void oDan_Click(object sender, EventArgs e)
        {
            // Kiểm tra chặt chẽ: Không phải lượt hoặc đang chạy -> Nghỉ
            if (!_isMyTurn || dangDiChuyen) return;

            int index = (int)(sender as Label).Tag;
            if (_game.BanCo[index] == 0) return; // Ô trống

            // Highlight ô đã chọn
            int old = oDaChon;
            oDaChon = index;
            if (old != -1) oVuong[old].Invalidate();
            oVuong[oDaChon].Invalidate();

            // Hiển thị nút bấm
            try
            {
                var pt = tblBanCo.PointToScreen(oVuong[oDaChon].Location);
                pt = this.PointToClient(pt);
                // Nút luôn hiện ở dưới ô (vì ô đã xoay xuống dưới rồi)
                btnTrai.Location = new Point(pt.X - btnTrai.Width - 5, pt.Y + 20);
                btnPhai.Location = new Point(pt.X + oVuong[oDaChon].Width + 5, pt.Y + 20);
            }
            catch { }

            CapNhatTrangThaiLuot(); // Để hiện nút
        }

        private async void btnHuong_Click(object sender, EventArgs e)
        {
            if (oDaChon < 0) return;
            if (!_isMyTurn || dangDiChuyen) return;

            // Lấy hướng: -1 (Trái) hoặc 1 (Phải)
            int dir = (int)(sender as Button).Tag;
            int start = oDaChon;

            // Khóa ngay lập tức để không bấm lung tung
            oDaChon = -1;
            btnTrai.Visible = btnPhai.Visible = false;
            dangDiChuyen = true; // Khóa animation

            // Gửi dữ liệu lên Firestore
            // (Chưa chạy animation vội, đợi Firestore báo về ở hàm LangNghePhong thì mới chạy)
            DocumentReference doc = FirestoreService.DB.Collection("rooms").Document(_roomId);
            Dictionary<string, object> updates = new Dictionary<string, object>
            {
                { "LastStart", start },
                { "LastDir", dir },
                { "MoveCount", _lastProcessedMoveCount + 1 }
            };
            await doc.UpdateAsync(updates);
        }

        // --- 7. ANIMATION (CHẠY TRÊN CẢ 2 MÁY) ---
        private async Task ThucHienNuocDi(int start, int dir)
        {
            // Xác định ai là người vừa đi để cộng điểm
            // Host đi ô 7-11, Guest đi ô 1-5
            bool isP1Move = (start >= 7 && start <= 11);

            dangDiChuyen = true; // Bắt đầu chạy -> Khóa
            btnTrai.Visible = btnPhai.Visible = false;

            // Bốc quân
            int stones = _game.BanCo[start];
            _game.BanCo[start] = 0;
            CapNhatGiaoDien(); await Task.Delay(250);

            // Rải quân
            int pos = start;
            while (stones > 0)
            {
                pos = _game.NextIndex(pos, dir);
                _game.BanCo[pos]++;
                CapNhatGiaoDien(); await Task.Delay(250);
                stones--;
            }

            // Xử lý logic (Ăn/Tiếp/Dừng)
            while (true)
            {
                int next = _game.NextIndex(pos, dir);
                if (_game.IsQuan(next)) break; // Dừng ở Quan

                if (_game.BanCo[next] > 0)
                { // Đi tiếp
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
                { // Ăn quân
                    int nextEmpty = _game.NextIndex(next, dir);
                    if (_game.BanCo[nextEmpty] > 0)
                    {
                        int cap = _game.BanCo[nextEmpty]; _game.BanCo[nextEmpty] = 0;

                        // CỘNG ĐIỂM ĐÚNG NGƯỜI
                        if (isP1Move) _game.DiemNguoi1 += cap;
                        else _game.DiemNguoi2 += cap;

                        CapNhatGiaoDien(); await Task.Delay(400);

                        // Ăn chuỗi
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

            dangDiChuyen = false; // Mở khóa animation
            CapNhatGiaoDien();
        }

        // --- 8. LOGIC SAU KHI ĐI XONG ---
        private async void CheckSauNuocDi()
        {
            // Kiểm tra kết thúc
            _game.CheckEndGame();
            if (_game.GameOver)
            {
                CapNhatGiaoDien();
                MessageBox.Show($"Kết thúc! P1: {_game.DiemNguoi1} - P2: {_game.DiemNguoi2}");
                this.Close(); return;
            }

            // Tự động vay quân nếu hết
            if (_game.CheckAndBorrowStones())
            {
                CapNhatGiaoDien(); await Task.Delay(1000);
            }

            // --- QUAN TRỌNG: ĐỔI LƯỢT ---
            // Chỉ người vừa đi (người có lượt lúc nãy) mới được quyền báo Server đổi lượt
            if (_isMyTurn)
            {
                DocumentReference doc = FirestoreService.DB.Collection("rooms").Document(_roomId);
                DocumentSnapshot snap = await doc.GetSnapshotAsync();

                string hostID = snap.GetValue<string>("HostUID");
                string guestID = snap.GetValue<string>("GuestUID");

                // Nếu mình là Host thì chuyển cho Guest, và ngược lại
                string nextUID = (_myUID == hostID) ? guestID : hostID;

                await doc.UpdateAsync("Turn", nextUID);
            }
        }

        // --- 9. CÁC HÀM CẬP NHẬT GIAO DIỆN ---
        private void CapNhatGiaoDien()
        {
            // Vẽ lại
            for (int i = 0; i < 12; i++) if (oVuong[i] != null) oVuong[i].Invalidate();

            // Hiện điểm (Phân biệt Bạn/Đối thủ)
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

            // Nút chỉ hiện khi: Đến lượt + Đã chọn ô + Không đang chạy
            bool showBtn = _isMyTurn && (oDaChon >= 0) && !dangDiChuyen;
            btnTrai.Visible = btnPhai.Visible = showBtn;
        }

        private void oVuong_Paint(object sender, PaintEventArgs e)
        {
            Label lbl = sender as Label;
            int index = (int)lbl.Tag;
            // Gọi Drawer vẽ
            _drawer.DrawCell(e.Graphics, lbl.ClientRectangle, index, _game.BanCo[index], (index == oDaChon));
        }

        private void GameOnline_FormClosing(object sender, FormClosingEventArgs e)
        {
            _listener?.StopAsync(); // Ngắt kết nối khi thoát
        }
    }
}