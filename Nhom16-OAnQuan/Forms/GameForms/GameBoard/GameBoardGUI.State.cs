using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nhom16_OAnQuan.Forms.GameForms
{
    public partial class GameBoardGUI : Form
    {
        // ====== STATE ======
        private int[] banCo = new int[12];        // 0..11 (0,6 = Quan)
        private Label[] oVuong = new Label[12];   // 12 Label UI mapping 12 ô
        private int diemNguoi1;                   // Người (ô 7..11)
        private int diemNguoi2;                   // Bot (ô 1..5)
        private bool laLuotNguoiChoi = true;
        private int oDaChon = -1;
        private bool dangDiChuyen = false;
        private bool gameOver = false;

        // Tốc độ/nhịp (ms)
        private const int DELAY_CHUYEN = 320;
        private const int DELAY_RAI = 320;
        private const int DELAY_AN = 420;
        private const int DELAY_VAY = 600;
        private const int DELAY_BOT_THINK = 350;

        // Vòng đi thuận chiều kim đồng hồ dọc theo chu vi
        private readonly int[] ring = new int[] { 7, 8, 9, 10, 11, 6, 5, 4, 3, 2, 1, 0 };
        private int[] ringIndexOf = new int[12];

        // Helpers
        private bool IsQuan(int i) => (i == 0 || i == 6);

        private int NextIndex(int from, int dir) // dir = +1 (cw) | -1 (ccw)
        {
            int pos = ringIndexOf[from];
            int npos = (pos + (dir > 0 ? 1 : -1) + ring.Length) % ring.Length;
            return ring[npos];
        }
    }
}
