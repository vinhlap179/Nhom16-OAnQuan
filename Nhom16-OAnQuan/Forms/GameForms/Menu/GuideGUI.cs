using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Nhom16_OAnQuan.Forms.GameForms
{
    public partial class GuideGUI : Form
    {
        private List<string> guideImages;
        private int currentIndex = 0;

        public GuideGUI()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new StartingGUI().Show();
            this.Hide();
        }

        private void GuideGUI_Load(object sender, EventArgs e)
        {
            guideImages = new List<string>
            {
                "Assets/Images/GameBoard/Guides/pic1.png",
                "Assets/Images/GameBoard/Guides/pic2.png",
                "Assets/Images/GameBoard/Guides/pic3.png",
                "Assets/Images/GameBoard/Guides/pic4.png"
            };

            picGuide.SizeMode = PictureBoxSizeMode.Zoom;
            ShowCurrentImage();
        }

        private void ShowCurrentImage()
        {
            // Lấy đường dẫn thư mục chứa file .exe đang chạy
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            // Kết hợp với đường dẫn tương đối
            string relativePath = guideImages[currentIndex];
            string imagePath = Path.Combine(baseDirectory, relativePath);

            if (File.Exists(imagePath))
            {
                // Giải phóng ảnh cux
                if (picGuide.Image != null) picGuide.Image.Dispose();
                picGuide.Image = new Bitmap(imagePath);
            }
            else
            {
                MessageBox.Show($"Không tìm thấy ảnh tại: \n{imagePath}", "Lỗi");
            }

            btnBack.Enabled = (currentIndex > 0);
            btnNext.Enabled = (currentIndex < guideImages.Count - 1);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                ShowCurrentImage();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentIndex < guideImages.Count - 1)
            {
                currentIndex++;
                ShowCurrentImage();
            }
        }

        private void picGuide_Click(object sender, EventArgs e)
        {
        }
    }
}