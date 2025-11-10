using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Cloud.Firestore;
using Nhom16_OAnQuan.Classes;


namespace Nhom16_OAnQuan.Forms.GameForms
{
    public partial class PlayerInfoForm : Form
    {
        public PlayerInfoForm()
        {
            InitializeComponent();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void PlayerInfoForm_Load(object sender, EventArgs e)
        {
            string username = GlobalUserSession.CurrentUsername;
            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("UserData").Document(username);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if (!snapshot.Exists)
            {
                MessageBox.Show("Không tìm thấy thông tin người chơi.");
                return;
            }

            UserData data = snapshot.ConvertTo<UserData>();

            lbUsername.Text = $"👤 Tài khoản: {data.Username}";
            lbWin.Text = $"🏆 Số trận thắng: {data.Wins}";
            lbLoss.Text = $"💀 Số trận thua: {data.Losses}";
            lbTotal.Text = $"🎮 Tổng số trận: {data.TotalGames}";
        }
    }
}
