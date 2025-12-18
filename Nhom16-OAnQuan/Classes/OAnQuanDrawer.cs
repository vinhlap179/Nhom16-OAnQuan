using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace Nhom16_OAnQuan.Classes
{
    public class OAnQuanDrawer
    {
        private Image _imgWood;
        private Image _imgSoiNho;
        private Image _imgQuanLon; // [MỚI] Biến chứa ảnh Quan

        private Random _rand = new Random();
        private PointF[][] _stonePositions;

        public OAnQuanDrawer()
        {
            string startupPath = Application.StartupPath;

            // 1. Load ảnh gỗ nền
            try
            {
                string path = Path.Combine(startupPath, "Images", "GameBoard", "cool.png");
                if (File.Exists(path)) _imgWood = Image.FromFile(path);
                else _imgWood = new Bitmap(1, 1);
            }
            catch { _imgWood = new Bitmap(1, 1); }

            // 2. Load ảnh Sỏi Nhỏ
            try
            {
                string pathSoi = Path.Combine(startupPath, "Images", "Stones", "SoiNho.png");
                if (File.Exists(pathSoi)) _imgSoiNho = Image.FromFile(pathSoi);
                else _imgSoiNho = null;
            }
            catch { _imgSoiNho = null; }

            // --- [MỚI] 3. Load ảnh Quan Lớn ---
            try
            {
                // Bạn nhớ đổi tên file kim cương thành QuanLon.png và bỏ vào thư mục Stones nhé
                string pathQuan = Path.Combine(startupPath, "Images", "Stones", "QuanLon.png");

                if (File.Exists(pathQuan))
                    _imgQuanLon = Image.FromFile(pathQuan);
                else
                    _imgQuanLon = null;
            }
            catch { _imgQuanLon = null; }
            // ----------------------------------

            // 4. Tạo vị trí sỏi ngẫu nhiên
            InitStonePositions();
        }

        private void InitStonePositions()
        {
            _stonePositions = new PointF[12][];
            for (int i = 0; i < 12; i++)
            {
                _stonePositions[i] = new PointF[100];
                for (int j = 0; j < 100; j++)
                {
                    _stonePositions[i][j] = new PointF((float)_rand.NextDouble() * 0.8f + 0.1f, (float)_rand.NextDouble() * 0.8f + 0.1f);
                }
            }
        }

        public void DrawCell(Graphics g, Rectangle rect, int index, int soQuan, bool isSelected)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // A. Vẽ Nền Gỗ
            if (_imgWood != null && _imgWood.Width > 1)
            {
                using (TextureBrush brush = new TextureBrush(_imgWood, WrapMode.TileFlipXY))
                {
                    brush.ScaleTransform((float)rect.Width / _imgWood.Width, (float)rect.Height / _imgWood.Height);
                    int radius = (index == 0 || index == 6) ? 60 : 20;

                    using (GraphicsPath path = GetRoundedRect(rect, radius))
                    {
                        g.FillPath(brush, path);
                        using (Pen p = new Pen(Color.FromArgb(60, 40, 20), 3)) g.DrawPath(p, path);
                        if (isSelected)
                        {
                            using (Pen pHi = new Pen(Color.Red, 5)) g.DrawPath(pHi, path);
                        }
                    }
                }
            }
            else
            {
                g.FillRectangle(Brushes.SandyBrown, rect);
            }

            // B. Vẽ Số
            using (Font f = new Font("Arial", 12, FontStyle.Bold))
            using (Brush b = new SolidBrush(Color.FromArgb(180, 255, 255, 255)))
            {
                g.DrawString(soQuan.ToString(), f, b, 5, 5);
            }

            // C. Vẽ Sỏi & Quan
            bool coQuanLon = false;
            int soSoiNho = soQuan;

            if ((index == 0 || index == 6) && soQuan >= 10)
            {
                coQuanLon = true;
                soSoiNho = soQuan - 10;
            }

            // --- [MỚI] VẼ QUAN LỚN BẰNG ẢNH ---
            if (coQuanLon)
            {
                float size = Math.Min(rect.Width, rect.Height) * 0.7f; // Kích thước Quan (60% ô cờ)
                float x = rect.X + (rect.Width - size) / 2;
                float y = rect.Y + (rect.Height - size) / 2;

                if (_imgQuanLon != null)
                {
                    // Vẽ bóng mờ dưới quan (tạo chiều sâu)
                    //g.FillEllipse(new SolidBrush(Color.FromArgb(60, 0, 0, 0)), x + 5, y + 5, size, size);
                    // Vẽ ảnh Quan
                    g.DrawImage(_imgQuanLon, x, y, size, size);
                }
                else
                {
                    // Dự phòng nếu chưa có ảnh: Vẽ hình tròn vàng
                    g.FillEllipse(new SolidBrush(Color.FromArgb(50, 0, 0, 0)), x + 3, y + 3, size, size);
                    g.FillEllipse(Brushes.Gold, x, y, size, size);
                    g.DrawEllipse(Pens.Goldenrod, x, y, size, size);
                }
            }
            // ----------------------------------

            int maxShow = 30;
            int drawCount = Math.Min(soSoiNho, maxShow);

            // Giữ nguyên size sỏi = 36 như bạn yêu cầu
            float sSize = 36;

            for (int k = 0; k < drawCount; k++)
            {
                // Logic tính vị trí để sỏi không tràn ra ngoài
                float rangeX = Math.Max(0, rect.Width - sSize * 1.5f);
                float rangeY = Math.Max(0, rect.Height - sSize * 1.5f);

                float x = rect.X + _stonePositions[index][k].X * rangeX;
                float y = rect.Y + _stonePositions[index][k].Y * rangeY;

                // Vẽ Sỏi Nhỏ
                if (_imgSoiNho != null)
                {
                    g.DrawImage(_imgSoiNho, x, y, sSize, sSize);
                }
                else
                {
                    Brush b = (k % 2 == 0) ? Brushes.DarkGray : Brushes.Gray;
                    g.FillEllipse(b, x, y, sSize, sSize);
                    g.DrawEllipse(Pens.Black, x, y, sSize, sSize);
                }
            }
        }

        private GraphicsPath GetRoundedRect(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, radius, radius, 180, 90);
            path.AddArc(bounds.X + bounds.Width - radius, bounds.Y, radius, radius, 270, 90);
            path.AddArc(bounds.X + bounds.Width - radius, bounds.Y + bounds.Height - radius, radius, radius, 0, 90);
            path.AddArc(bounds.X, bounds.Y + bounds.Height - radius, radius, radius, 90, 90);
            path.CloseAllFigures();
            return path;
        }
    }
}