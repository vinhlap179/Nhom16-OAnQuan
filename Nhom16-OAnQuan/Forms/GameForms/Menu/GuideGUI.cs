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
                "Assets/Images/Guides/pic1.png",
                "Assets/Images/Guides/pic2.png",
                "Assets/Images/Guides/pic3.png",
                "Assets/Images/Guides/pic4.png"
            };

            picGuide.SizeMode = PictureBoxSizeMode.Zoom;
            ShowCurrentImage();
        }

        private void ShowCurrentImage()
        {
            string imagePath = guideImages[currentIndex];

            if (File.Exists(imagePath))
            {
                // Note: Dùng 'new Bitmap(path)' thay vì 'Image.FromFile(path)'
                // để tránh lỗi file locking (file đang bị chiếm dụng).
                picGuide.Image = new Bitmap(imagePath);
            }
            else
            {
                MessageBox.Show($"Lỗi: Không tìm thấy file ảnh tại: {imagePath}", "Lỗi tải ảnh", MessageBoxButtons.OK, MessageBoxIcon.Error);
                picGuide.Image = null;
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