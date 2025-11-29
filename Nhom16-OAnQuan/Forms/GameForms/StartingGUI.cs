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
            new GameBoardGUI().ShowDialog();
        }

        private void StartingGUI_Load(object sender, EventArgs e)
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
        }

        private void btnLobby_Click(object sender, EventArgs e)
        {
            string username = GlobalUserSession.CurrentUsername;

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Chưa xác định được người dùng, vui lòng đăng nhập lại.");
                return;
            }

            LobbyForm lobby = new LobbyForm(username);
            this.Hide();
            lobby.ShowDialog();
            this.Show();

        }
            

        private void StartingGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (backgroundMusicPlayer != null)
            {
                backgroundMusicPlayer.Stop();
                backgroundMusicPlayer.Dispose(); // Giải phóng tài nguyên
            }
        }


    }
}