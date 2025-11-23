using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhom16_OAnQuan.Classes
{
    public class CustomBorderButton : Button
    {
        // Khai báo các thuộc tính mới để dễ dàng tùy chỉnh trong cửa sổ Properties

        // Thuộc tính màu viền
        public Color BorderColor { get; set; } = Color.DarkRed; // Màu mặc định cho viền

        // Thuộc tính độ dày viền
        public int BorderSize { get; set; } = 2; // Độ dày mặc định (ví dụ: 2 pixel)

        public CustomBorderButton()
        {
            // 1. Bắt buộc: Đặt FlatStyle là Flat để tắt hiệu ứng 3D mặc định
            this.FlatStyle = FlatStyle.Flat;

            // 2. Bắt buộc: Đặt BorderSize của FlatAppearance về 0 để tắt viền của FlatStyle
            this.FlatAppearance.BorderSize = 0;

            // Thiết lập các thuộc tính khác (ví dụ: màu chữ, kích thước)
            this.ForeColor = Color.White;
        }

        // Ghi đè phương thức OnPaint để vẽ lại nút
        protected override void OnPaint(PaintEventArgs pevent)
        {
            // Gọi phương thức OnPaint gốc để vẽ chữ và nền (BackColor)
            base.OnPaint(pevent);

            // Tạo đối tượng Pen (Bút vẽ) với màu và độ dày viền đã định nghĩa
            using (Pen pen = new Pen(this.BorderColor, this.BorderSize))
            {
                // Vẽ hình chữ nhật (viền) xung quanh nút
                // Vị trí (0, 0) là góc trên bên trái
                // Kích thước (Width - 1, Height - 1) để đảm bảo viền nằm hoàn toàn trong control
                pevent.Graphics.DrawRectangle(
                    pen,
                    0,
                    0,
                    this.Width - 1,
                    this.Height - 1
                );
            }
        }
    }
}
