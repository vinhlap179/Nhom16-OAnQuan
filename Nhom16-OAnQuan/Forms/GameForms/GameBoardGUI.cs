using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nhom16_OAnQuan.Classes;

namespace Nhom16_OAnQuan.Forms.GameForms
{
    public partial class GameBoardGUI : Form
    {
        private OAnQuanLogic _game;
        private OAnQuanDrawer _drawer;  // Họa sĩ vẽ bàn cờ
        private Label[] oVuong = new Label[12];
        private int oDaChon = -1;
        private bool dangDiChuyen = false;

        public GameBoardGUI()
        {
            InitializeComponent();
            _game = new OAnQuanLogic();
            _drawer = new OAnQuanDrawer();

            // Gắn sự kiện click
            btnTrai.Click += btnHuong_Click;
            btnPhai.Click += btnHuong_Click;
            btnTrai.Tag = -1;
            btnPhai.Tag = 1;

            this.BackColor = Color.FromArgb(40, 40, 40); // Màu nền tối cho ngầu
            this.Text = "Ô Ăn Quan - Đấu với Máy";
        }

        private void GameBoardGUI_Load(object sender, EventArgs e)
        {
            TaoBanCoUI();
            CapNhatGiaoDien();
        }

        // --- 1. XỬ LÝ GAME OVER & ĐỔI LƯỢT ---
        private async void DoiLuot()
        {
            if (_game.GameOver) return;

            // Đổi lượt (Người <-> Bot)
            _game.LaLuotNguoiChoi = !_game.LaLuotNguoiChoi;
            CapNhatGiaoDien();

            // 1. Kiểm tra kết thúc game
            _game.CheckEndGame();
            if (_game.GameOver)
            {
                CapNhatGiaoDien(); // Update điểm lần cuối

                // --- [SỬA Ở ĐÂY] GỌI FORM KẾT QUẢ XỊN XÒ ---
                // Truyền điểm Người và điểm Bot sang
                ResultForm resultForm = new ResultForm(_game.DiemNguoi1, _game.DiemNguoi2);
                resultForm.ShowDialog(); // Hiện form lên
                // -------------------------------------------

                this.Close(); // Đóng bàn cờ
                return;
            }

            // 2. Kiểm tra vay quân
            if (_game.CheckAndBorrowStones())
            {
                string ai = _game.LaLuotNguoiChoi ? "Bạn" : "Bot";
                MessageBox.Show($"{ai} hết quân, hệ thống tự động vay 5 điểm!", "Vay Quân");
                CapNhatGiaoDien();
                await Task.Delay(1000);
            }

            // 3. Nếu đến lượt Bot -> Gọi Bot chạy
            if (!_game.LaLuotNguoiChoi) await BotTuDi();
        }

        // --- 2. BOT TỰ ĐỘNG ĐI ---
        private async Task BotTuDi()
        {
            if (_game.GameOver) return;
            await Task.Delay(700); // Bot nghĩ 0.7 giây cho giống thật

            var move = _game.TimNuocDiChoBot();

            // Nếu Bot không tìm được nước đi (thường là đã hết quân -> sẽ rơi vào case Vay Quân ở trên)
            if (move.start == -1) { DoiLuot(); return; }

            await ThucHienNuocDi(move.start, move.dir, false);
            DoiLuot();
        }

        // --- 3. ANIMATION RẢI QUÂN ---
        private async Task ThucHienNuocDi(int start, int dir, bool laNguoi)
        {
            if (start < 0 || start >= 12 || _game.BanCo[start] <= 0) return;

            dangDiChuyen = true; // Khóa bàn cờ
            btnTrai.Visible = btnPhai.Visible = false;

            int stones = _game.BanCo[start];
            _game.BanCo[start] = 0;
            CapNhatGiaoDien(); await Task.Delay(250);

            int pos = start;
            // Rải quân
            while (stones > 0)
            {
                pos = _game.NextIndex(pos, dir);
                _game.BanCo[pos]++;
                CapNhatGiaoDien(); await Task.Delay(250);
                stones--;
            }

            // Xử lý logic (Ăn, Đi tiếp, Dừng)
            while (true)
            {
                int next = _game.NextIndex(pos, dir);
                if (_game.IsQuan(next)) break; // Gặp quan -> Dừng

                if (_game.BanCo[next] > 0) // Có quân -> Đi tiếp
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
                else // Ô trống -> Ăn
                {
                    int nextEmpty = _game.NextIndex(next, dir);
                    if (_game.BanCo[nextEmpty] > 0)
                    {
                        lblThongBao.Text = "Ăn quân!";
                        int cap = _game.BanCo[nextEmpty]; _game.BanCo[nextEmpty] = 0;

                        if (laNguoi) _game.DiemNguoi1 += cap;
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
                                lblThongBao.Text = "Ăn chuỗi!";
                                int c2 = _game.BanCo[tPos]; _game.BanCo[tPos] = 0;
                                if (laNguoi) _game.DiemNguoi1 += c2; else _game.DiemNguoi2 += c2;
                                CapNhatGiaoDien(); await Task.Delay(400);
                                cur = tPos; continue;
                            }
                            break;
                        }
                    }
                    break;
                }
            }
            dangDiChuyen = false; // Mở khóa
            CapNhatGiaoDien();
        }

        // --- 4. UI & SỰ KIỆN ---

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

                lbl.Paint += oVuong_Paint; // Gắn thợ vẽ

                // Chỉ cho người chơi bấm ô 7-11
                if (i >= 7 && i <= 11)
                {
                    lbl.Cursor = Cursors.Hand;
                    lbl.Click += oDan_Click;
                }
                else
                {
                    lbl.Cursor = Cursors.Default;
                }

                oVuong[i] = lbl;

                // Xếp vào bàn cờ
                if (i == 0) tblBanCo.Controls.Add(lbl, 0, 1);
                else if (i == 6) tblBanCo.Controls.Add(lbl, 6, 1);
                else if (i >= 1 && i <= 5) tblBanCo.Controls.Add(lbl, i, 0);
                else if (i >= 7 && i <= 11) tblBanCo.Controls.Add(lbl, i - 6, 2);
            }
        }

        private void oVuong_Paint(object sender, PaintEventArgs e)
        {
            Label lbl = sender as Label;
            int index = (int)lbl.Tag;
            // Nhờ Drawer vẽ giùm
            _drawer.DrawCell(e.Graphics, lbl.ClientRectangle, index, _game.BanCo[index], (index == oDaChon));
        }

        private void CapNhatGiaoDien()
        {
            for (int i = 0; i < 12; i++) if (oVuong[i] != null) oVuong[i].Invalidate();

            lblDiemNguoi1.Text = $"Bạn: {_game.DiemNguoi1}";
            lblDiemNguoi2.Text = $"Bot: {_game.DiemNguoi2}";
            lblThongBao.Text = _game.GameOver ? "Kết thúc." : (_game.LaLuotNguoiChoi ? "Lượt: Bạn" : "Lượt: Bot");

            // Khóa ô khi không phải lượt hoặc đang chạy animation
            bool choPhep = _game.LaLuotNguoiChoi && !dangDiChuyen && !_game.GameOver;
            for (int i = 0; i < 12; i++)
            {
                if (i >= 7 && i <= 11) oVuong[i].Enabled = choPhep && _game.BanCo[i] > 0;
                else oVuong[i].Enabled = false;
            }

            btnTrai.Visible = btnPhai.Visible = (oDaChon >= 0);
        }

        private void oDan_Click(object sender, EventArgs e)
        {
            if (dangDiChuyen || _game.GameOver || !_game.LaLuotNguoiChoi) return;

            int index = (int)(sender as Label).Tag;
            if (index < 7 || index > 11) return;
            if (_game.BanCo[index] == 0) return;

            int oldSelect = oDaChon;
            oDaChon = index;
            if (oldSelect != -1) oVuong[oldSelect].Invalidate();
            oVuong[oDaChon].Invalidate();

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
            int dir = (int)(sender as Button).Tag;

            int start = oDaChon; // Lưu lại
            oDaChon = -1;
            btnTrai.Visible = btnPhai.Visible = false;

            await ThucHienNuocDi(start, dir, true);
            DoiLuot();
        }
    }
}