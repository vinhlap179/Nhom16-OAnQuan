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
        // --- PHẦN KHAI BÁO BIẾN ---
        private OAnQuanLogic _game;     // Cái này chứa luật chơi logic
        private OAnQuanDrawer _drawer;  // Cái này để vẽ vời (họa sĩ)
        private Label[] oVuong = new Label[12]; // Mảng 12 ô trên bàn cờ

        // Biến trạng thái
        private int oDaChon = -1;
        private bool _isAnimating = false; // Cờ hiệu: Đang rải quân thì cấm bấm lung tung
        private bool _isMyTurn = false;    // Cờ hiệu: Đến lượt mình chưa?
        private bool _dangThoat = false;   // Cờ hiệu: Mình có đang bấm thoát ko? (Để fix lỗi hiện thông báo 2 bên)

        // Biến kết nối mạng
        private string _roomId;
        private string _myUID;
        private bool _isHost; // True là Chủ phòng (P1), False là Khách (P2)
        private FirestoreChangeListener _listener; // Cái này để "hóng" tin từ server về
        private int _lastProcessedMoveCount = 0; // Đếm số nước đi để ko bị lặp lại

        public GameOnline(string roomId, string myUID, bool isHost)
        {
            InitializeComponent();
            _roomId = roomId;
            _myUID = myUID;
            _isHost = isHost;

            _game = new OAnQuanLogic();
            _drawer = new OAnQuanDrawer();

            // Gán hàm xử lý click cho 2 nút
            btnTrai.Click += btnHuong_Click;
            btnPhai.Click += btnHuong_Click;

            // Quy ước: -1 là đi Trái, 1 là đi Phải
            btnTrai.Tag = -1;
            btnPhai.Tag = 1;

            this.BackColor = Color.FromArgb(40, 40, 40);
            // Hiện title cho dễ biết mình là ai
            this.Text = isHost ? $"CHỦ PHÒNG (P1) - Room: {roomId}" : $"KHÁCH (P2) - Room: {roomId}";

            this.Load += GameOnline_Load;
            this.FormClosing += GameOnline_FormClosing;
        }

        private void GameOnline_Load(object sender, EventArgs e)
        {
            TaoBanCoUI();       // Vẽ mấy cái ô ra
            CapNhatGiaoDien();  // Hiện điểm số lên
            LangNghePhong();    // Bắt đầu kết nối mạng để hóng tin
        }

        // -----------------------------------------------------------
        // 1. HÀM XỬ LÝ SAU KHI ĐI XONG (QUAN TRỌNG NHẤT)
        // Chạy xong animation thì nhảy vào đây để tính toán tiếp
        // -----------------------------------------------------------
        private async Task CheckDoiLuotVaKetThuc(bool isP1JustMoved)
        {
            // --- Check xem Game Over chưa ---
            _game.CheckEndGame();
            if (_game.GameOver)
            {
                CapNhatGiaoDien();

                string winnerName = "";
                if (_game.DiemNguoi1 > _game.DiemNguoi2) winnerName = "CHỦ PHÒNG (P1)";
                else if (_game.DiemNguoi2 > _game.DiemNguoi1) winnerName = "KHÁCH (P2)";
                else winnerName = "HÒA";

                string msg = "";
                // Logic hiển thị thông báo cho ngầu
                if ((_isHost && _game.DiemNguoi1 > _game.DiemNguoi2) || (!_isHost && _game.DiemNguoi2 > _game.DiemNguoi1))
                    msg = "NGON! BẠN THẮNG RỒI! 🏆";
                else if (_game.DiemNguoi1 == _game.DiemNguoi2) msg = "HÒA CẢ LÀNG! 🤝";
                else msg = "TOANG! BẠN THUA RỒI! 😢";

                MessageBox.Show($"{msg}\nNgười thắng: {winnerName}\nTỷ số: P1({_game.DiemNguoi1}) - P2({_game.DiemNguoi2})");

                // Chỉ chủ phòng mới đc quyền xóa phòng trên server cho sạch rác
                if (_isHost)
                {
                    try { await FirestoreService.DB.Collection("rooms").Document(_roomId).DeleteAsync(); } catch { }
                }

                this.Close(); // Đóng game
                return;
            }

            // --- Logic Vay Quân ---
            // Mẹo: Đảo ngược biến lượt đi để kiểm tra túi tiền của người TIẾP THEO
            _game.LaLuotNguoiChoi = !isP1JustMoved;

            if (_game.CheckAndBorrowStones())
            {
                CapNhatGiaoDien();
                // Dừng 1s cho người chơi kịp nhìn thấy 5 viên sỏi vừa rải
                await Task.Delay(1000);
            }

            // --- Đổi lượt trên Server ---
            // Chỉ thằng vừa đi xong mới được quyền báo server đổi lượt (để tránh 2 máy cùng gửi gây lỗi)
            if (_isMyTurn)
            {
                DocumentReference doc = FirestoreService.DB.Collection("rooms").Document(_roomId);
                DocumentSnapshot snap = await doc.GetSnapshotAsync();

                string hostID = snap.GetValue<string>("HostUID");
                string guestID = snap.GetValue<string>("GuestUID");
                // Nếu mình là Host thì chuyển lượt cho Guest, và ngược lại
                string nextUID = (_myUID == hostID) ? guestID : hostID;

                await doc.UpdateAsync("Turn", nextUID);
            }
        }

        // -----------------------------------------------------------
        // 2. HÀM CHẠY HIỆU ỨNG RẢI SỎI (ANIMATION)
        // Hàm này chạy trên cả 2 máy để đồng bộ hình ảnh
        // -----------------------------------------------------------
        private async Task ThucHienNuocDi(int start, int dir)
        {
            if (start < 0 || start >= 12 || _game.BanCo[start] <= 0) return;

            // Xác định ai là người đi nước này? (Để cộng điểm cho đúng)
            // 7-11 là P1 đi. 1-5 là P2 đi.
            bool isP1Move = (start >= 7 && start <= 11);

            _isAnimating = true; // Khóa màn hình lại, ko cho bấm nữa
            btnTrai.Visible = btnPhai.Visible = false;

            // Bốc quân lên tay
            int stones = _game.BanCo[start];
            _game.BanCo[start] = 0;
            CapNhatGiaoDien(); await Task.Delay(250);

            // Rải quân từng ô
            int pos = start;
            while (stones > 0)
            {
                pos = _game.NextIndex(pos, dir);
                _game.BanCo[pos]++;
                CapNhatGiaoDien(); await Task.Delay(250);
                stones--;
            }

            // Xử lý logic: Vào quan, đi tiếp hay ăn điểm?
            while (true)
            {
                int next = _game.NextIndex(pos, dir);
                if (_game.IsQuan(next)) break; // Gặp quan thì dừng

                if (_game.BanCo[next] > 0) // Có quân -> Bốc đi tiếp
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
                else // Ô trống -> Ăn điểm
                {
                    int nextEmpty = _game.NextIndex(next, dir);
                    if (_game.BanCo[nextEmpty] > 0)
                    {
                        int cap = _game.BanCo[nextEmpty]; _game.BanCo[nextEmpty] = 0;

                        // Cộng điểm cho người vừa đi
                        if (isP1Move) _game.DiemNguoi1 += cap; else _game.DiemNguoi2 += cap;
                        CapNhatGiaoDien(); await Task.Delay(400);

                        // Check ăn chuỗi (ăn liên hoàn)
                        int cur = nextEmpty;
                        while (true)
                        {
                            int ePos = _game.NextIndex(cur, dir);
                            int tPos = _game.NextIndex(ePos, dir);
                            // Nếu ô tiếp theo trống VÀ ô sau nữa có quân -> Ăn tiếp
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

            _isAnimating = false; // Xong rồi, mở khóa
            CapNhatGiaoDien();

            // Gọi hàm check kết thúc/vay quân/đổi lượt
            await CheckDoiLuotVaKetThuc(isP1Move);
        }

        // -----------------------------------------------------------
        // 3. PHẦN LẮNG NGHE TỪ SERVER VỀ MÁY (LISTENER)
        // -----------------------------------------------------------
        private void LangNghePhong()
        {
            DocumentReference doc = FirestoreService.DB.Collection("rooms").Document(_roomId);
            _listener = doc.Listen(async snapshot =>
            {
                // Nếu form đóng rồi thì thôi, đừng chạy nữa kẻo lỗi
                if (this.IsDisposed || !this.IsHandleCreated) return;

                if (!snapshot.Exists) { MessageBox.Show("Phòng này bay màu rồi!"); this.Close(); return; }

                // A. Cập nhật ai đang đến lượt
                if (snapshot.TryGetValue("Turn", out string currentTurnUID))
                {
                    // Dùng Invoke để vẽ lại giao diện an toàn
                    this.Invoke((MethodInvoker)delegate {
                        _isMyTurn = (currentTurnUID == _myUID); // Nếu UID trên mạng trùng mình -> Lượt mình
                        CapNhatTrangThaiLuot();
                    });
                }

                // B. Check xem đối thủ có thoát game không
                if (snapshot.TryGetValue("Status", out string status))
                {
                    // Nếu status là PlayerLeft VÀ mình không phải đứa đang thoát
                    if (status == "PlayerLeft" && !_dangThoat)
                    {
                        this.Invoke((MethodInvoker)delegate {
                            MessageBox.Show("Đối thủ sợ quá chạy mất dép rồi! Bạn thắng.");
                            this.Close();
                        });
                    }
                }

                // C. Nhận nước đi mới từ đối thủ
                if (snapshot.TryGetValue("MoveCount", out int moveCount))
                {
                    // Nếu số đếm tăng lên -> Có nước đi mới
                    if (moveCount > _lastProcessedMoveCount)
                    {
                        _lastProcessedMoveCount = moveCount;
                        int start = snapshot.GetValue<int>("LastStart");
                        int dir = snapshot.GetValue<int>("LastDir");

                        this.Invoke((MethodInvoker)async delegate {
                            await ThucHienNuocDi(start, dir); // Chạy hoạt hình
                        });
                    }
                }
            });
        }

        // Khi mình bấm nút Chọn Hướng thì chạy hàm này
        private async void btnHuong_Click(object sender, EventArgs e)
        {
            // Kiểm tra kỹ: Chưa chọn ô? Chưa đến lượt? Đang chạy? -> Cút
            if (oDaChon < 0 || !_isMyTurn || _isAnimating) return;

            int dir = (int)(sender as Button).Tag;
            int start = oDaChon;

            // Khóa giao diện ngay lập tức
            oDaChon = -1;
            btnTrai.Visible = btnPhai.Visible = false;
            _isAnimating = true;

            // Đẩy dữ liệu lên Server (Chỉ đẩy thôi, chưa chạy animation vội)
            // Đợi Server phản hồi về Listener thì cả 2 máy mới cùng chạy
            DocumentReference doc = FirestoreService.DB.Collection("rooms").Document(_roomId);
            Dictionary<string, object> updates = new Dictionary<string, object>
            {
                { "LastStart", start },
                { "LastDir", dir },
                { "MoveCount", _lastProcessedMoveCount + 1 } // Tăng số đếm lên
            };
            await doc.UpdateAsync(updates);
        }

        // -----------------------------------------------------------
        // 4. CÁC HÀM HỖ TRỢ GIAO DIỆN (UI)
        // -----------------------------------------------------------

        // Hàm kiểm tra xem ô này có phải của mình không (để chặn click bậy)
        private bool IsMySide(int index)
        {
            if (_isHost && index >= 7 && index <= 11) return true; // Host bấm 7-11
            if (!_isHost && index >= 1 && index <= 5) return true; // Guest bấm 1-5
            return false;
        }

        // Hàm xoay bàn cờ 180 độ cho Guest đỡ bị ngược tay
        private (int col, int row) GetUiCoordinates(int index)
        {
            int col = 0, row = 0;
            // Vị trí chuẩn của Host
            if (index == 0) { col = 0; row = 1; }
            else if (index == 6) { col = 6; row = 1; }
            else if (index >= 1 && index <= 5) { col = index; row = 0; }
            else if (index >= 7 && index <= 11) { col = index - 6; row = 2; }

            // Nếu là Guest -> Xoay ngược lại
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
                lbl.Paint += oVuong_Paint; // Nhờ thợ vẽ

                // Phân quyền click chuột
                if (IsMySide(i)) { lbl.Cursor = Cursors.Hand; lbl.Click += oDan_Click; }
                else lbl.Cursor = Cursors.Default;

                oVuong[i] = lbl;
                var pos = GetUiCoordinates(i); // Tính vị trí đặt ô
                tblBanCo.Controls.Add(lbl, pos.col, pos.row);
            }
        }

        private void CapNhatGiaoDien()
        {
            for (int i = 0; i < 12; i++) if (oVuong[i] != null) oVuong[i].Invalidate(); // Vẽ lại

            // Hiển thị điểm số
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

            bool allowClick = _isMyTurn && !_isAnimating;
            for (int i = 0; i < 12; i++)
            {
                if (IsMySide(i)) oVuong[i].Enabled = allowClick && _game.BanCo[i] > 0;
                else oVuong[i].Enabled = false; // Ô đối thủ luôn khóa
            }
            // Nút chỉ hiện khi đã chọn ô
            bool showBtn = allowClick && (oDaChon >= 0);
            btnTrai.Visible = btnPhai.Visible = showBtn;
        }

        private void oVuong_Paint(object sender, PaintEventArgs e)
        {
            Label lbl = sender as Label;
            int index = (int)lbl.Tag;
            // Gọi class Drawer vẽ hình sỏi và gỗ
            _drawer.DrawCell(e.Graphics, lbl.ClientRectangle, index, _game.BanCo[index], (index == oDaChon));
        }

        private void oDan_Click(object sender, EventArgs e)
        {
            if (!_isMyTurn || _isAnimating) return; // Chặn click
            int index = (int)(sender as Label).Tag;
            if (!IsMySide(index) || _game.BanCo[index] == 0) return;

            // Highlight ô vừa chọn
            int old = oDaChon; oDaChon = index;
            if (old != -1) oVuong[old].Invalidate();
            oVuong[oDaChon].Invalidate();

            // Tính vị trí để hiện nút Trái/Phải ngay dưới ô đó
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

        // Hàm này chạy khi bấm nút X tắt form
        private async void GameOnline_FormClosing(object sender, FormClosingEventArgs e)
        {
            _dangThoat = true; // Đánh dấu là mình đang thoát
            _listener?.StopAsync(); // Ngắt kết nối

            try
            {
                // Báo lên server là mình thoát để thằng kia biết mà thắng
                DocumentReference doc = FirestoreService.DB.Collection("rooms").Document(_roomId);
                await doc.UpdateAsync("Status", "PlayerLeft");
            }
            catch { }
        }
    }
}