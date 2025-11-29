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

namespace Nhom16_OAnQuan.Forms.GameForms
{
    public partial class StartingGUI : Form
    {
        public StartingGUI()
        {
            InitializeComponent();
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

        }

        private void btnLobby_Click(object sender, EventArgs e)
        {
            // 1. Lấy tên người dùng hiện tại từ Session (thay vì điền cứng "diecchituong")
            string username = GlobalUserSession.CurrentUsername;

            // 2. Kiểm tra cho chắc chắn (phòng hờ)
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Chưa xác định được người dùng, vui lòng đăng nhập lại.");
                return;
            }

            // 3. Khởi tạo LobbyForm
            LobbyForm lobby = new LobbyForm(username);

            // 4. Ẩn form StartingGUI đi để chuyển sang Lobby
            this.Hide();

            // 5. Dùng ShowDialog() để code dừng tại đây cho đến khi LobbyForm đóng lại
            lobby.ShowDialog();

            // 6. Sau khi LobbyForm đóng, hiện lại StartingGUI
            this.Show();

        }
    }
}