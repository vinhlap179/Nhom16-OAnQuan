using Nhom16_OAnQuan.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using Google.Cloud.Firestore;

namespace Nhom16_OAnQuan.Forms.GameForms
{
    public partial class StartingGUI : Form
    {
        private SoundPlayer backgroundMusicPlayer;
        public StartingGUI()
        {
            InitializeComponent();
            backgroundMusicPlayer = new SoundPlayer();
            pictureBox1.BackColor = System.Drawing.Color.Transparent;
            CheckLoginToken();
        }



        private void CheckLoginToken()
        {
            if (string.IsNullOrEmpty(GlobalUserSession.CurrentToken))
            {
                // Nếu không có Token, phiên không hợp lệ
                MessageBox.Show("Phiên đăng nhập đã hết hạn hoặc không hợp lệ.", "Cần đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                LogInForm loginForm = new LogInForm();
                this.Hide();
                loginForm.ShowDialog();
                Application.Exit();
                return;
            }
            else
            {
                this.Text = $"OAnQuan - Đăng nhập với: {GlobalUserSession.CurrentUsername}";
            }
        }

        private void SignOutBtn_Click(object sender, EventArgs e)
        {
            GlobalUserSession.ClearSession();

            MessageBox.Show("Bạn đã đăng xuất thành công.", "Đăng xuất");
            this.Close();
            Application.Restart();
        }

        private void GuideBtn_Click(object sender, EventArgs e)
        {
            new GuideGUI().ShowDialog();
        }

        private void PlayerInformationBtn_Click(object sender, EventArgs e)
        {
            new PlayerInfoForm().ShowDialog();
        }

        private void PlayBtn_Click(object sender, EventArgs e)
        {
            new ChooseMode().ShowDialog();
        }

        private async void StartingGUI_Load(object sender, EventArgs e)
        {
            try
            {
                // Gán file nhạc từ Resources (đảm bảo file 'seabed' là file .wav)
                backgroundMusicPlayer.Stream = Properties.Resources.seabed;
                // Phát lặp lại
                backgroundMusicPlayer.PlayLooping();
            }
            catch (Exception ex)
            {
                // Phòng trường hợp lỗi file nhạc thì không bị crash game
                Console.WriteLine("Lỗi phát nhạc: " + ex.Message);
            }

            // Gọi hàm tải bảng xếp hạng ngay tại đây
            await LoadBangXepHang();
        }

        
            

        private void StartingGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (backgroundMusicPlayer != null)
            {
                backgroundMusicPlayer.Stop();
                backgroundMusicPlayer.Dispose(); // Giải phóng tài nguyên
            }
        }


        // Hàm cấu hình và tải bảng xếp hạng
        private async Task LoadBangXepHang()
        {
            try
            {
                //Lấy dữ liệu từ Firestore xong sắp xếp
                var db = FirestoreHelper.Database;
                Query query = db.Collection("UserData").OrderByDescending("Wins").Limit(10);
                QuerySnapshot snap = await query.GetSnapshotAsync();

                //Cho dữ liệu vào bảng
                dgvBXH.Rows.Clear();
                int rank = 1;

                foreach (DocumentSnapshot doc in snap.Documents)
                {
                    if (doc.Exists)
                    {
                        UserData user = doc.ConvertTo<UserData>();
                        dgvBXH.Rows.Add(rank, user.Username, user.Wins);
                        rank++;
                    }
                }

  
                dgvBXH.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi BXH: " + ex.Message);
            }
        }
        private void StartingGUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            GlobalUserSession.ClearSession();
            MessageBox.Show("Bạn đã đăng xuất thành công.", "Đăng xuất");
            Application.Restart();
        }


    }
}