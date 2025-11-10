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
    public partial class ResultForm : Form
    {
        private int playerScore;
        private int botScore;
        public ResultForm(int playerScore, int botScore)
        {
            InitializeComponent();
            this.playerScore = playerScore;
            this.botScore = botScore;
        }

        private async void ResultForm_Load(object sender, EventArgs e)
        {
            lbScorePlayer.Text = $"Điểm của bạn: {playerScore}";
            lbScoreBot.Text = $"Điểm của máy: {botScore}";

            string resultMessage;

            if (playerScore > botScore)
            {
                resultMessage = "🎉 Bạn đã thắng!";
                await UpdatePlayerStats(isWinner: true);
            }
            else if (playerScore < botScore)
            {
                resultMessage = "😢 Bạn đã thua!";
                await UpdatePlayerStats(isWinner: false);
            }
            else
            {
                resultMessage = "🤝 Hòa!";
                await UpdatePlayerStats(isWinner: null);
            }

            lbKq.Text = resultMessage;
        }

        private async Task UpdatePlayerStats(bool? isWinner)
        {
            try
            {
                var db = FirestoreHelper.Database;
                string username = GlobalUserSession.CurrentUsername;

                DocumentReference docRef = db.Collection("UserData").Document(username);
                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

                if (!snapshot.Exists) return;

                UserData data = snapshot.ConvertTo<UserData>();

                data.TotalGames++;

                if (isWinner == true)
                    data.Wins++;
                else if (isWinner == false)
                    data.Losses++;

                await docRef.SetAsync(data, SetOptions.Overwrite);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi cập nhật thống kê: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
