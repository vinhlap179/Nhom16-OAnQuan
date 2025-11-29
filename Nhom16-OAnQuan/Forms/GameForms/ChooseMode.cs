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
    public partial class ChooseMode : Form
    {
        public ChooseMode()
        {
            InitializeComponent();
        }

        private void PlayOnlineBtn_Click(object sender, EventArgs e)
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

        private void PlayOfflineBtn_Click(object sender, EventArgs e)
        {
            GameBoardGUI Gameboard = new GameBoardGUI();
            this.Hide();
            Gameboard.ShowDialog();
            this.Show();
        }
    }
}
