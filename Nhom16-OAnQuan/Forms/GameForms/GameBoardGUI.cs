using System;
using System.Collections.Generic; // Thêm
using System.ComponentModel; // Thêm
using System.Data; // Thêm
using System.Drawing;
using System.Linq; // Thêm
using System.Text; // Thêm
using System.Threading.Tasks; // Thêm
using System.Windows.Forms;

// Namespace của project bạn
namespace Nhom16_OAnQuan.Forms.GameForms
{
    // Đã đổi tên class thành GameBoardGUI
    public partial class GameBoardGUI : Form
    {
        // ====== STATE VARIABLES (Biến toàn cục của game) ======
        private int[] banCo = new int[12];      // 0..11 (0,6 là Quan)
        private Label[] oVuong = new Label[12]; // 12 label UI khớp 12 ô logic
        private int diemNguoi1;                 // điểm người (Dân 1: 7..11)
        private int diemNguoi2;                 // điểm bot   (Dân 2: 1..5)
        private bool laLuotNguoiChoi = true;    // true: người, false: bot
        private int oDaChon = -1;               // ô đã chọn để rải
        private bool dangDiChuyen = false;      // đang rải/ăn => chặn thao tác
        private bool gameOver = false;

        // Vòng đi quanh bàn (chu vi) theo chiều thuận (clockwise)
        private readonly int[] ring = new int[] { 7, 8, 9, 10, 11, 6, 5, 4, 3, 2, 1, 0 };
        private int[] ringIndexOf = new int[12]; // map ô -> vị trí trong ring

        // Constructor của GameBoardGUI
        public GameBoardGUI()
        {
            InitializeComponent();

            // Thêm code setup từ logic game
            // (Lưu ý: Bạn phải có btnTrai, btnPhai trong file .Designer)
            btnTrai.Click += btnHuong_Click;
            btnPhai.Click += btnHuong_Click;
            btnTrai.Tag = -1;
            btnPhai.Tag = +1;
        }

        // ====== SỰ KIỆN LOAD (Đã gộp code) ======
        private void GameBoardGUI_Load(object sender, EventArgs e)
        {
            // Tạo map ngược ô -> vị trí trong ring
            for (int i = 0; i < ring.Length; i++)
                ringIndexOf[ring[i]] = i;

            TaoBanCoUI();      // tạo 12 Label, add vào tblBanCo theo vị trí yêu cầu
            KhoiTaoGame();     // reset mảng banCo + điểm
            CapNhatGiaoDien();
        }

        // ====== A. TẠO BÀN CỜ UI (chạy 1 lần) ======
        private void TaoBanCoUI()
        {
            // (Lưu ý: Bạn phải có TableLayoutPanel tên là tblBanCo)
            tblBanCo.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tblBanCo.Controls.Clear();

            Font fontDan = new Font("Segoe UI", 14, FontStyle.Bold);
            Font fontQuan = new Font("Segoe UI", 16, FontStyle.Bold);

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

                // Style Quan vs Dân
                if (i == 0 || i == 6)
                {
                    lbl.BackColor = Color.Goldenrod;
                    lbl.ForeColor = Color.White;
                    lbl.Font = fontQuan;
                    lbl.Cursor = Cursors.Default; // không click
                }
                else
                {
                    lbl.BackColor = Color.LightSteelBlue;
                    lbl.ForeColor = Color.Black;
                    lbl.Font = fontDan;
                    lbl.Cursor = Cursors.Hand;
                    lbl.Click += oDan_Click; // handler chung cho ô dân
                }

                oVuong[i] = lbl;

                // Vị trí trong TableLayoutPane
                if (i == 0)
                {
                    // Quan trái
                    tblBanCo.Controls.Add(lbl, 0, 1);
                }
                else if (i == 6)
                {
                    // Quan phải
                    tblBanCo.Controls.Add(lbl, 6, 1);
                }
                else if (i >= 1 && i <= 5)
                {
                    // Dân 2: map 1..5 -> col 1..5, row 0 // Hàng trên
                    int col = i;
                    tblBanCo.Controls.Add(lbl, col, 0);
                }
                else if (i >= 7 && i <= 11)
                {
                    // Dân 1: map 7..11 -> col 1..5, row 2 /// Hàng dưới
                    int col = i - 6;
                    tblBanCo.Controls.Add(lbl, col, 2);
                }
            }
        }


        // ====== B. CẬP NHẬT GIAO DIỆN ======
        private void CapNhatGiaoDien()
        {
            for (int i = 0; i < 12; i++)
            {
                if (oVuong[i] != null)
                {
                    oVuong[i].Text = banCo[i].ToString();
                }
            }

            // (Lưu ý: Bạn phải có 3 Labels này)
            lblDiemNguoi1.Text = $"Người 1: {diemNguoi1}";
            lblDiemNguoi2.Text = $"Bot: {diemNguoi2}";
            lblThongBao.Text = gameOver
                ? "Trò chơi đã kết thúc."
                : (laLuotNguoiChoi ? "Lượt: Người chơi" : "Lượt: Bot");

            // Enable/Disable ô dân theo lượt + tình trạng đang chọn hướng/đang di chuyển
            bool enableDanNguoi = laLuotNguoiChoi && !dangDiChuyen && oDaChon < 0 && !gameOver;

            for (int i = 0; i < 12; i++)
            {
                bool isQuan = (i == 0 || i == 6);
                bool isDanNguoi = (i >= 7 && i <= 11);
                bool isDanBot = (i >= 1 && i <= 5);

                if (isQuan)
                {
                    oVuong[i].Enabled = false;
                }
                else if (isDanNguoi)
                {
                    oVuong[i].Enabled = enableDanNguoi && banCo[i] > 0;
                }
                else if (isDanBot)
                {
                    oVuong[i].Enabled = false; // người không bấm vào ô bot
                }
            }

            // Hiện/ẩn nút hướng nếu đang chờ chọn hướng
            btnTrai.Visible = (oDaChon >= 0);
            btnPhai.Visible = (oDaChon >= 0);
        }

        // ====== Khởi tạo mảng & điểm ======
        private void KhoiTaoGame()
        {
            // Dân: 5, Quan: 10
            for (int i = 0; i < 12; i++)
            {
                if (i == 0 || i == 6) banCo[i] = 10; // Quan
                else banCo[i] = 5; // Dân
            }

            diemNguoi1 = 0;
            diemNguoi2 = 0;
            laLuotNguoiChoi = true;
            oDaChon = -1;
            dangDiChuyen = false;
            gameOver = false;
        }

        // ====== C. CLICK CHỌN Ô DÂN (người) ======
        private void oDan_Click(object sender, EventArgs e)
        {
            if (dangDiChuyen || gameOver) return;
            if (!laLuotNguoiChoi) return;

            var lblClicked = sender as Label;
            if (lblClicked == null) return;

            int index = (int)lblClicked.Tag;

            if (index < 7 || index > 11) return; // không thuộc phe người
            if (banCo[index] == 0) return;

            oDaChon = index;

            // Vô hiệu hóa các ô dân khác trong lúc chọn hướng
            for (int i = 7; i <= 11; i++)
            {
                if (i != oDaChon) oVuong[i].Enabled = false;
            }

            // Đặt 2 nút hướng gần ô đã chọn
            try
            {
                var ptScreen = tblBanCo.PointToScreen(oVuong[oDaChon].Location);
                var ptForm = this.PointToClient(ptScreen);

                btnTrai.Location = new Point(ptForm.X - btnTrai.Width - 6, ptForm.Y + oVuong[oDaChon].Height / 2 - btnTrai.Height / 2);
                btnPhai.Location = new Point(ptForm.X + oVuong[oDaChon].Width + 6, ptForm.Y + oVuong[oDaChon].Height / 2 - btnPhai.Height / 2);
            }
            catch { /* an toàn */ }

            CapNhatGiaoDien();
        }

        // ====== D. NÚT HƯỚNG: RẢI QUÂN & ĂN QUÂN ======
        private async void btnHuong_Click(object sender, EventArgs e)
        {
            if (dangDiChuyen || gameOver) return;
            if (!laLuotNguoiChoi) return;
            if (oDaChon < 0) return;

            var btn = sender as Button;
            int dir = (int)btn.Tag; // -1 = trái (ccw), +1 = phải (cw)

            btnTrai.Visible = false;
            btnPhai.Visible = false;

            // Thực hiện nước đi thật
            await ThucHienNuocDi(oDaChon, dir, true);

            oDaChon = -1;
            CapNhatGiaoDien();

            // Đổi lượt -> có thể gọi bot
            DoiLuot();
        }

        // ====== E. BOT TỰ ĐI (Minimax 1-ply đơn giản) ======
        private async Task BotTuDi()
        {
            if (gameOver) return;
            await Task.Delay(350);

            // Tìm nước đi tốt nhất: start in [1..5], dir in {-1,+1}
            int bestStart = -1;
            int bestDir = +1;
            int bestValue = int.MinValue;

            // Nếu bot không có quân, cố gắng cho qua (DoiLuot sẽ xử lý vay/quét kết thúc)
            bool coQuanDeDi = false;

            for (int start = 1; start <= 5; start++)
            {
                if (banCo[start] == 0) continue;
                coQuanDeDi = true;

                foreach (int dir in new int[] { -1, +1 })
                {
                    int gainBot = 0;
                    int[] boardAfter = SimulateMove(banCo, start, dir, false, ref gainBot);

                    // Đáp trả tốt nhất của người
                    int bestHumanGain = 0;
                    for (int hstart = 7; hstart <= 11; hstart++)
                    {
                        if (boardAfter[hstart] == 0) continue;
                        foreach (int hdir in new int[] { -1, +1 })
                        {
                            int tmp = 0;
                            SimulateMove(boardAfter, hstart, hdir, true, ref tmp);
                            if (tmp > bestHumanGain) bestHumanGain = tmp;
                        }
                    }

                    int value = gainBot - bestHumanGain;

                    if (value > bestValue)
                    {
                        bestValue = value;
                        bestStart = start;
                        bestDir = dir;
                    }
                }
            }

            if (!coQuanDeDi)
            {
                // Không có nước, để DoiLuot xử lý tiếp (vay hoặc kết thúc)
                DoiLuot();
                return;
            }

            // Thực hiện nước đi thật của bot
            await ThucHienNuocDi(bestStart, bestDir, false);
            CapNhatGiaoDien();

            // Đổi lượt về người
            DoiLuot();
        }

        // ====== F1. KIỂM TRA & VAY QUÂN ======
        private bool KiemTraVaVayQuan(bool laNguoiChoi1Turn)
        {
            if (gameOver) return false;

            if (laNguoiChoi1Turn)
            {
                bool allEmpty = true;
                for (int i = 7; i <= 11; i++) if (banCo[i] > 0) { allEmpty = false; break; }
                if (allEmpty)
                {
                    // vay 5, mỗi ô +1
                    int can = 5;
                    int trum = Math.Min(can, diemNguoi1);
                    diemNguoi1 -= trum; can -= trum;
                    if (can > 0)
                    {
                        int boSung = Math.Min(can, diemNguoi2);
                        diemNguoi2 -= boSung; can -= boSung;
                    }
                    for (int i = 7; i <= 11; i++) banCo[i] += 1;

                    lblThongBao.Text = "Người chơi vay 5 quân để rải lại ô dân.";
                    CapNhatGiaoDien();

                    // >>> POP-UP vay quân cho Người chơi <<<
                    MessageBox.Show(
                        "Bạn đã hết quân ở 5 ô dân! Bạn phải vay 5 quân (mỗi ô 1 quân) từ điểm của mình.",
                        "Vay quân!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return true;
                }
            }
            else
            {
                bool allEmpty = true;
                for (int i = 1; i <= 5; i++) if (banCo[i] > 0) { allEmpty = false; break; }
                if (allEmpty)
                {
                    int can = 5;
                    int trum = Math.Min(can, diemNguoi2);
                    diemNguoi2 -= trum; can -= trum;
                    if (can > 0)
                    {
                        int boSung = Math.Min(can, diemNguoi1);
                        diemNguoi1 -= boSung; can -= boSung;
                    }
                    for (int i = 1; i <= 5; i++) banCo[i] += 1;

                    lblThongBao.Text = "Bot vay 5 quân để rải lại ô dân.";
                    CapNhatGiaoDien();

                    // >>> POP-UP vay quân cho Bot <<<
                    MessageBox.Show(
                        "Bot đã hết quân ở 5 ô dân! Bot phải vay 5 quân (mỗi ô 1 quân) từ điểm.",
                        "Vay quân!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return true;
                }
            }
            return false;
        }


        // ====== F2. KẾT THÚC GAME (2 Quan trống -> lùa quân, tính điểm) ======
        private void KiemTraKetThucGame()
        {
            if (gameOver) return;

            if (banCo[0] == 0 && banCo[6] == 0)
            {
                // Lùa quân: gom các ô dân về điểm
                int nguoiThu = 0;
                for (int i = 7; i <= 11; i++) { nguoiThu += banCo[i]; banCo[i] = 0; }
                int botThu = 0;
                for (int i = 1; i <= 5; i++) { botThu += banCo[i]; banCo[i] = 0; }

                diemNguoi1 += nguoiThu;
                diemNguoi2 += botThu;

                gameOver = true;

                string ketQua = (diemNguoi1 > diemNguoi2) ? "Người chơi thắng!"
                                : (diemNguoi1 < diemNguoi2) ? "Bot thắng!"
                                : "Hòa!";
                lblThongBao.Text = $"Kết thúc game. {ketQua}";
                CapNhatGiaoDien();

                // MỞ FORM KẾT QUẢ 
                ResultForm resultForm = new ResultForm(diemNguoi1, diemNguoi2);

                // dạng dialog, sẽ khóa form game lại
                resultForm.ShowDialog();
                this.Close();
                

                // Khóa thao tác
                for (int i = 0; i < 12; i++) if (oVuong[i] != null) oVuong[i].Enabled = false;
                btnTrai.Visible = btnPhai.Visible = false;
            }
        }

        // ====== G. ĐỔI LƯỢT ======
        private async void DoiLuot()
        {
            if (gameOver) return;

            laLuotNguoiChoi = !laLuotNguoiChoi;
            CapNhatGiaoDien();

            // Kiểm tra kết thúc sớm
            KiemTraKetThucGame();
            if (gameOver) return;

            // Vay quân nếu lượt mới và 5 ô dân trống
            bool coVay = KiemTraVaVayQuan(laLuotNguoiChoi);
            if (coVay)
            {
                CapNhatGiaoDien();
                await Task.Delay(600);
            }

            // Lại kiểm tra kết thúc (phòng trường hợp lạ)
            KiemTraKetThucGame();
            if (gameOver) return;

            // Nếu đến lượt bot, bot đi
            if (!laLuotNguoiChoi)
            {
                await BotTuDi();
            }
            else
            {
                CapNhatGiaoDien();
            }
        }

        // ====== Helper: tính next theo ring và hướng ======
        private int NextIndex(int from, int dir) // dir = +1 (cw) | -1 (ccw)
        {
            int pos = ringIndexOf[from];
            int npos = (pos + (dir > 0 ? 1 : -1) + ring.Length) % ring.Length;
            return ring[npos];
        }

        private bool IsQuan(int i) => (i == 0 || i == 6);

        // ====== THỰC THI NƯỚC ĐI THẬT (có animation nhỏ) ======
        private async Task ThucHienNuocDi(int start, int dir, bool laNguoi)
        {
            if (banCo[start] <= 0) return;

            dangDiChuyen = true;

            int stones = banCo[start];
            banCo[start] = 0;
            CapNhatGiaoDien();
            await Task.Delay(320);

            int pos = start;
            // Rải hết số quân đang cầm
            while (stones > 0)
            {
                pos = NextIndex(pos, dir);
                banCo[pos]++;
                CapNhatGiaoDien();
                await Task.Delay(320);
                stones--;
            }

            // Sau khi rải xong, xử lý ba trường hợp
            while (true)
            {
                int next = NextIndex(pos, dir);

                // 1) vào quan => dừng
                if (IsQuan(next))
                {
                    break;
                }

                if (banCo[next] > 0)
                {
                    // 2) Có quân đi tiếp
                    stones = banCo[next];
                    banCo[next] = 0;
                    pos = next;
                    CapNhatGiaoDien();
                    await Task.Delay(320);

                    while (stones > 0)
                    {
                        pos = NextIndex(pos, dir);
                        banCo[pos]++;
                        CapNhatGiaoDien();
                        await Task.Delay(320);
                        stones--;
                    }
                    continue;
                }
                else
                {
                    // 3) Ô kế TRỐNG
                    int afterEmpty = NextIndex(next, dir);
                    if (banCo[afterEmpty] > 0)
                    {
                        // >>> BẮT ĐẦU ĂN QUÂN <<<
                        lblThongBao.Text = "Ăn quân!";
                        lblThongBao.Refresh();

                        int captured = banCo[afterEmpty];
                        banCo[afterEmpty] = 0;
                        if (laNguoi) diemNguoi1 += captured; else diemNguoi2 += captured;
                        CapNhatGiaoDien();
                        await Task.Delay(420);

                        // ĂN CHUỖI
                        int cur = afterEmpty;
                        while (true)
                        {
                            int emptyPos = NextIndex(cur, dir);
                            int target = NextIndex(emptyPos, dir);

                            if (banCo[emptyPos] == 0 && banCo[target] > 0)
                            {
                                // >>> BẮT ĐẦU ĂN CHUỖI <<<
                                lblThongBao.Text = "Ăn chuỗi!";
                                lblThongBao.Refresh();

                                int cap2 = banCo[target];
                                banCo[target] = 0;
                                if (laNguoi) diemNguoi1 += cap2; else diemNguoi2 += cap2;
                                CapNhatGiaoDien();
                                await Task.Delay(420);
                                cur = target;
                                continue;
                            }
                            break;
                        }
                    }
                    // kết thúc nước đi
                    break;
                }
            }

            dangDiChuyen = false;
            CapNhatGiaoDien();
        }

        // ====== MÔ PHỎNG NƯỚC ĐI (không UI, dùng cho bot/minimax) ======
        private int[] SimulateMove(int[] boardSrc, int start, int dir, bool laNguoi, ref int scoreGain)
        {
            int[] b = (int[])boardSrc.Clone();
            scoreGain = 0;
            if (b[start] <= 0) return b;

            int stones = b[start];
            b[start] = 0;

            int pos = start;
            // Rải
            while (stones > 0)
            {
                pos = SimNextIndex(pos, dir);
                b[pos]++;
                stones--;
            }

            while (true)
            {
                int next = SimNextIndex(pos, dir);
                if (IsQuan(next))
                {
                    break; // vào quan
                }

                if (b[next] > 0)
                {
                    // bốc đi tiếp
                    stones = b[next];
                    b[next] = 0;
                    pos = next;
                    while (stones > 0)
                    {
                        pos = SimNextIndex(pos, dir);
                        b[pos]++;
                        stones--;
                    }
                    continue;
                }
                else
                {
                    int afterEmpty = SimNextIndex(next, dir);
                    if (b[afterEmpty] > 0)
                    {
                        scoreGain += b[afterEmpty];
                        b[afterEmpty] = 0;

                        int cur = afterEmpty;
                        while (true)
                        {
                            int emptyPos = SimNextIndex(cur, dir);
                            int target = SimNextIndex(emptyPos, dir);

                            if (b[emptyPos] == 0 && b[target] > 0)
                            {
                                scoreGain += b[target];
                                b[target] = 0;
                                cur = target;
                                continue;
                            }
                            break;
                        }
                    }
                    break; // kết thúc
                }
            }
            return b;

            int SimNextIndex(int from, int d)
            {
                int posIn = ringIndexOf[from];
                int npos = (posIn + (d > 0 ? 1 : -1) + ring.Length) % ring.Length;
                return ring[npos];
            }
        }

    } 
}