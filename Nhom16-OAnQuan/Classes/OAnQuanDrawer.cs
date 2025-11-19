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
        private Random _rand = new Random();
        private PointF[][] _stonePositions; // Lưu vị trí sỏi để không bị "nhảy" lung tung

        public OAnQuanDrawer()
        {
            // 1. Load ảnh gỗ từ folder
            try
            {
                string path = Path.Combine(Application.StartupPath, "Images", "GameBoard", "cool.jpg");
                if (File.Exists(path)) _imgWood = Image.FromFile(path);
                else _imgWood = new Bitmap(1, 1);
            }
            catch { _imgWood = new Bitmap(1, 1); }

            // 2. Tạo vị trí sỏi ngẫu nhiên
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
                    // Random vị trí từ 10% đến 90% diện tích ô
                    _stonePositions[i][j] = new PointF((float)_rand.NextDouble() * 0.8f + 0.1f, (float)_rand.NextDouble() * 0.8f + 0.1f);
                }
            }
        }

        // --- HÀM VẼ CHÍNH ---
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
                    int radius = (index == 0 || index == 6) ? 60 : 20; // Quan bo tròn hơn

                    using (GraphicsPath path = GetRoundedRect(rect, radius))
                    {
                        g.FillPath(brush, path);
                        using (Pen p = new Pen(Color.FromArgb(60, 40, 20), 3)) g.DrawPath(p, path);
                        // Highlight viền ĐỎ khi chọn
                        if (isSelected)
                        {
                            // Màu Red, độ dày 5 cho rõ
                            using (Pen pHi = new Pen(Color.Red, 5))
                            {
                                // Vẽ đè lên để thấy rõ viền
                                g.DrawPath(pHi, path);
                            }
                        }
                    }
                }
            }
            else
            {
                g.FillRectangle(Brushes.SandyBrown, rect); // Màu dự phòng
            }

            // B. Vẽ Số
            using (Font f = new Font("Arial", 12, FontStyle.Bold))
            using (Brush b = new SolidBrush(Color.FromArgb(180, 255, 255, 255)))
            {
                g.DrawString(soQuan.ToString(), f, b, 5, 5);
            }

            // C. Vẽ Sỏi & Quan (Logic: 1 Quan = 10 Sỏi)
            bool coQuanLon = false;
            int soSoiNho = soQuan;

            if ((index == 0 || index == 6) && soQuan >= 10)
            {
                coQuanLon = true;
                soSoiNho = soQuan - 10;
            }

            // Vẽ Quan lớn
            if (coQuanLon)
            {
                float size = Math.Min(rect.Width, rect.Height) * 0.5f;
                float x = rect.X + (rect.Width - size) / 2;
                float y = rect.Y + (rect.Height - size) / 2;

                g.FillEllipse(new SolidBrush(Color.FromArgb(50, 0, 0, 0)), x + 3, y + 3, size, size); // Bóng
                g.FillEllipse(Brushes.Gold, x, y, size, size);
                g.DrawEllipse(Pens.Goldenrod, x, y, size, size);
            }

            int maxShow = 30;
            int drawCount = Math.Min(soSoiNho, maxShow);
            float sSize = 18; // Tăng kích thước sỏi từ 14 lên 18

            for (int k = 0; k < drawCount; k++)
            {
                float x = rect.X + _stonePositions[index][k].X * (rect.Width - sSize * 2);
                float y = rect.Y + _stonePositions[index][k].Y * (rect.Height - sSize * 2);

                // Màu sỏi đậm hơn: DarkGray và Gray
                Brush b = (k % 2 == 0) ? Brushes.DarkGray : Brushes.Gray;

                g.FillEllipse(b, x, y, sSize, sSize);
                g.DrawEllipse(Pens.Black, x, y, sSize, sSize); // Viền đen cho rõ
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