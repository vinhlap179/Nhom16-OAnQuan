using System.Drawing;
using System.Windows.Forms;

namespace Nhom16_OAnQuan.Forms.GameForms
{
    public partial class GameBoardGUI : Form
    {
        // Tạo 12 ô lên TableLayoutPanel
        private void TaoBanCoUI()
        {
            tableLayoutPanel1.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tableLayoutPanel1.Controls.Clear();

            Font fontDan = new Font("Segoe UI", 14, System.Drawing.FontStyle.Bold);
            Font fontQuan = new Font("Segoe UI", 16, System.Drawing.FontStyle.Bold);

            for (int i = 0; i < 12; i++)
            {
                var lbl = new Label
                {
                    Tag = i,
                    TextAlign = ContentAlignment.MiddleCenter,
                    BorderStyle = BorderStyle.FixedSingle,
                    Dock = DockStyle.Fill,
                    Margin = new Padding(5),
                    AutoEllipsis = true,
                    Name = $"lbl_{i}"
                };

                if (i == 0 || i == 6) // Ô Quan
                {
                    lbl.BackColor = Color.Goldenrod;
                    lbl.ForeColor = Color.White;
                    lbl.Font = fontQuan;
                    lbl.Cursor = Cursors.Default;
                }
                else // Ô Dân
                {
                    lbl.BackColor = Color.LightSteelBlue;
                    lbl.ForeColor = Color.Black;
                    lbl.Font = fontDan;
                    lbl.Cursor = Cursors.Hand;
                    lbl.Click += oDan_Click;
                }

                oVuong[i] = lbl;

                // Vị trí: Hàng 0 (1..5), Hàng 1 (Quan 0/6), Hàng 2 (7..11)
                if (i == 0) tableLayoutPanel1.Controls.Add(lbl, 0, 1);
                else if (i == 6) tableLayoutPanel1.Controls.Add(lbl, 6, 1);
                else if (i >= 1 && i <= 5) tableLayoutPanel1.Controls.Add(lbl, i, 0);        // top: 1..5 -> col 1..5
                else if (i >= 7 && i <= 11) tableLayoutPanel1.Controls.Add(lbl, i - 6, 2);    // bottom: 7..11 -> col 1..5
            }
        }

        // Cập nhật giao diện
        private void CapNhatGiaoDien()
        {
            for (int i = 0; i < 12; i++)
                if (oVuong[i] != null) oVuong[i].Text = banCo[i].ToString();

            lblDiemNguoi1.Text = $"Người 1: {diemNguoi1}";
            lblDiemNguoi2.Text = $"Bot: {diemNguoi2}";
            lblThongBao.Text = gameOver ? "Trò chơi đã kết thúc."
                                        : (laLuotNguoiChoi ? "Lượt: Người chơi" : "Lượt: Bot");

            bool enableDanNguoi = laLuotNguoiChoi && !dangDiChuyen && oDaChon < 0 && !gameOver;

            for (int i = 0; i < 12; i++)
            {
                bool isQuan = (i == 0 || i == 6);
                bool isDanNguoi = (i >= 7 && i <= 11);
                bool isDanBot = (i >= 1 && i <= 5);

                if (isQuan) oVuong[i].Enabled = false;
                else if (isDanNguoi) oVuong[i].Enabled = enableDanNguoi && banCo[i] > 0;
                else if (isDanBot) oVuong[i].Enabled = false;
            }

            btnTrai.Visible = (oDaChon >= 0);
            btnPhai.Visible = (oDaChon >= 0);
        }
    }
}
