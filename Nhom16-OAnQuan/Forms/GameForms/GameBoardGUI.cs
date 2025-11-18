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
        private OAnQuanDrawer _drawer;  // Họa sĩ
        private Label[] oVuong = new Label[12];
        private int oDaChon = -1;
        private bool dangDiChuyen = false;

        public GameBoardGUI()
        {
            InitializeComponent();
            _game = new OAnQuanLogic();
            _drawer = new OAnQuanDrawer(); // Khởi tạo họa sĩ (sẽ load ảnh gỗ)

            btnTrai.Click += btnHuong_Click;
            btnPhai.Click += btnHuong_Click;
            btnTrai.Tag = -1;
            btnPhai.Tag = 1;

            this.BackColor = Color.FromArgb(40, 40, 40);
        }

        private void GameBoardGUI_Load(object sender, EventArgs e)
        {
            TaoBanCoUI();
            CapNhatGiaoDien();
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
                    Text = "", // Xóa text, để Drawer vẽ
                    BorderStyle = BorderStyle.None,
                    Dock = DockStyle.Fill,
                    Margin = new Padding(5),
                    BackColor = Color.Transparent
                };

                // Gắn sự kiện vẽ
                lbl.Paint += oVuong_Paint;

                // Sự kiện Click
                if (i >= 7 && i <= 11)
                {
                    lbl.Cursor = Cursors.Hand;
                    lbl.Click += oDan_Click;
                }
                else lbl.Cursor = Cursors.Default;

                oVuong[i] = lbl;

                if (i == 0) tblBanCo.Controls.Add(lbl, 0, 1);
                else if (i == 6) tblBanCo.Controls.Add(lbl, 6, 1);
                else if (i >= 1 && i <= 5) tblBanCo.Controls.Add(lbl, i, 0);
                else if (i >= 7 && i <= 11) tblBanCo.Controls.Add(lbl, i - 6, 2);
            }
        }

        // --- HÀM GỌI DRAWER VẼ ---
        private void oVuong_Paint(object sender, PaintEventArgs e)
        {
            Label lbl = sender as Label;
            int index = (int)lbl.Tag;

            // Nhờ Drawer vẽ hộ lên Graphics của Label này
            _drawer.DrawCell(
                e.Graphics,
                lbl.ClientRectangle,
                index,
                _game.BanCo[index],
                (index == oDaChon)
            );
        }

        private void CapNhatGiaoDien()
        {
            for (int i = 0; i < 12; i++) if (oVuong[i] != null) oVuong[i].Invalidate(); // Vẽ lại

            lblDiemNguoi1.Text = $"Bạn: {_game.DiemNguoi1}";
            lblDiemNguoi2.Text = $"Bot: {_game.DiemNguoi2}";
            lblThongBao.Text = _game.GameOver ? "Kết thúc." : (_game.LaLuotNguoiChoi ? "Lượt: Bạn" : "Lượt: Bot");

            bool choPhep = _game.LaLuotNguoiChoi && !dangDiChuyen && oDaChon < 0 && !_game.GameOver;
            for (int i = 7; i <= 11; i++) oVuong[i].Enabled = choPhep && _game.BanCo[i] > 0;

            btnTrai.Visible = btnPhai.Visible = (oDaChon >= 0);
        }

        // --- LOGIC EVENTS ---
        private void oDan_Click(object sender, EventArgs e)
        {
            if (dangDiChuyen || _game.GameOver || !_game.LaLuotNguoiChoi) return;
            int index = (int)(sender as Label).Tag;

            if (index < 7 || index > 11) return;
            if (_game.BanCo[index] == 0) return;

            int old = oDaChon;
            oDaChon = index;
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

        private async void DoiLuot()
        {
            if (_game.GameOver) return;
            _game.LaLuotNguoiChoi = !_game.LaLuotNguoiChoi;
            CapNhatGiaoDien();

            _game.CheckEndGame();
            if (_game.GameOver)
            {
                CapNhatGiaoDien();
                MessageBox.Show($"Hết Game!\nTỷ số: {_game.DiemNguoi1} - {_game.DiemNguoi2}");
                this.Close();
                return;
            }

            if (_game.CheckAndBorrowStones())
            {
                string ai = _game.LaLuotNguoiChoi ? "Bạn" : "Bot";
                MessageBox.Show($"{ai} hết quân, vay 5 điểm!", "Vay Quân");
                CapNhatGiaoDien();
                await Task.Delay(500);
            }

            if (!_game.LaLuotNguoiChoi) await BotTuDi();
        }

        private async Task BotTuDi()
        {
            if (_game.GameOver) return;
            await Task.Delay(500);
            var move = _game.TimNuocDiChoBot();
            if (move.start == -1) { DoiLuot(); return; }
            await ThucHienNuocDi(move.start, move.dir, false);
            DoiLuot();
        }

        private async Task ThucHienNuocDi(int start, int dir, bool laNguoi)
        {
            if (start < 0 || start >= 12 || _game.BanCo[start] <= 0) return;

            dangDiChuyen = true;
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
                        lblThongBao.Text = "Ăn quân!";
                        int cap = _game.BanCo[nextEmpty]; _game.BanCo[nextEmpty] = 0;
                        if (laNguoi) _game.DiemNguoi1 += cap; else _game.DiemNguoi2 += cap;
                        CapNhatGiaoDien(); await Task.Delay(400);

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
            dangDiChuyen = false;
            CapNhatGiaoDien();
        }
    }
}