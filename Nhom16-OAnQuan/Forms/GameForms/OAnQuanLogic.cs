using System;
using System.Linq;

namespace Nhom16_OAnQuan.Classes
{
    public class OAnQuanLogic
    {
        // --- 1. DỮ LIỆU (DATA) ---
        public int[] BanCo { get; private set; } = new int[12];
        public int DiemNguoi1 { get; set; }
        public int DiemNguoi2 { get; set; }
        public bool LaLuotNguoiChoi { get; set; } = true;
        public bool GameOver { get; set; } = false;

        // Định nghĩa vòng đi (Ring)
        private readonly int[] ring = new int[] { 7, 8, 9, 10, 11, 6, 5, 4, 3, 2, 1, 0 };
        private int[] ringIndexOf = new int[12];

        public OAnQuanLogic()
        {
            // Map index để tính toán nhanh
            for (int i = 0; i < ring.Length; i++) ringIndexOf[ring[i]] = i;
            KhoiTaoGame();
        }

        // --- 2. CÁC HÀM CƠ BẢN ---
        public void KhoiTaoGame()
        {
            for (int i = 0; i < 12; i++)
            {
                if (i == 0 || i == 6) BanCo[i] = 10; // Quan
                else BanCo[i] = 5;                   // Dân
            }
            DiemNguoi1 = 0;
            DiemNguoi2 = 0;
            LaLuotNguoiChoi = true;
            GameOver = false;
        }

        public int NextIndex(int from, int dir) // dir: -1 (Trái), 1 (Phải)
        {
            int pos = ringIndexOf[from];
            int npos = (pos + (dir > 0 ? 1 : -1) + ring.Length) % ring.Length;
            return ring[npos];
        }

        public bool IsQuan(int i) => (i == 0 || i == 6);

        // --- 3. KIỂM TRA KẾT THÚC & VAY QUÂN ---
        public void CheckEndGame()
        {
            if (BanCo[0] == 0 && BanCo[6] == 0)
            {
                // Lùa quân còn lại vào điểm
                for (int i = 7; i <= 11; i++) { DiemNguoi1 += BanCo[i]; BanCo[i] = 0; }
                for (int i = 1; i <= 5; i++) { DiemNguoi2 += BanCo[i]; BanCo[i] = 0; }
                GameOver = true;
            }
        }

        public bool CheckAndBorrowStones()
        {
            if (LaLuotNguoiChoi) // Người chơi
            {
                bool allEmpty = true;
                for (int i = 7; i <= 11; i++) if (BanCo[i] > 0) { allEmpty = false; break; }
                if (allEmpty)
                {
                    int can = 5;
                    int trum = Math.Min(can, DiemNguoi1);
                    DiemNguoi1 -= trum; can -= trum;
                    if (can > 0) { DiemNguoi2 -= Math.Min(can, DiemNguoi2); }
                    for (int i = 7; i <= 11; i++) BanCo[i] += 1;
                    return true;
                }
            }
            else // Bot
            {
                bool allEmpty = true;
                for (int i = 1; i <= 5; i++) if (BanCo[i] > 0) { allEmpty = false; break; }
                if (allEmpty)
                {
                    int can = 5;
                    int trum = Math.Min(can, DiemNguoi2);
                    DiemNguoi2 -= trum; can -= trum;
                    if (can > 0) { DiemNguoi1 -= Math.Min(can, DiemNguoi1); }
                    for (int i = 1; i <= 5; i++) BanCo[i] += 1;
                    return true;
                }
            }
            return false;
        }

        // --- 4. LOGIC BOT (AI) ---
        public (int start, int dir) TimNuocDiChoBot()
        {
            int bestStart = -1;
            int bestDir = 1;
            int bestValue = int.MinValue;
            bool coQuanDeDi = false;

            for (int start = 1; start <= 5; start++)
            {
                if (BanCo[start] == 0) continue;
                coQuanDeDi = true;

                foreach (int dir in new int[] { -1, 1 })
                {
                    int gainBot = 0;
                    int[] boardAfter = SimulateMove(BanCo, start, dir, false, ref gainBot);

                    int bestHumanGain = 0;
                    // Giả lập người chơi đáp trả
                    for (int hstart = 7; hstart <= 11; hstart++)
                    {
                        if (boardAfter[hstart] == 0) continue;
                        foreach (int hdir in new int[] { -1, 1 })
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

            if (!coQuanDeDi) return (-1, 1);
            return (bestStart, bestDir);
        }

        // Hàm mô phỏng (Private) - Dùng riêng cho AI
        private int[] SimulateMove(int[] boardSrc, int start, int dir, bool laNguoi, ref int scoreGain)
        {
            int[] b = (int[])boardSrc.Clone();
            scoreGain = 0;
            if (b[start] <= 0) return b;

            int stones = b[start];
            b[start] = 0;
            int pos = start;

            while (stones > 0) // Rải
            {
                pos = NextIndex(pos, dir);
                b[pos]++;
                stones--;
            }

            while (true)
            {
                int next = NextIndex(pos, dir);
                if (IsQuan(next)) break; // Vào quan -> dừng
                if (b[next] > 0) // Có quân -> đi tiếp
                {
                    stones = b[next]; b[next] = 0; pos = next;
                    while (stones > 0) { pos = NextIndex(pos, dir); b[pos]++; stones--; }
                }
                else // Trống -> Ăn
                {
                    int afterEmpty = NextIndex(next, dir);
                    if (b[afterEmpty] > 0)
                    {
                        scoreGain += b[afterEmpty]; b[afterEmpty] = 0;
                        int cur = afterEmpty;
                        // Ăn chuỗi
                        while (true)
                        {
                            int emptyPos = NextIndex(cur, dir);
                            int target = NextIndex(emptyPos, dir);
                            if (b[emptyPos] == 0 && b[target] > 0)
                            {
                                scoreGain += b[target]; b[target] = 0; cur = target; continue;
                            }
                            break;
                        }
                    }
                    break;
                }
            }
            return b;
        }
    }
}