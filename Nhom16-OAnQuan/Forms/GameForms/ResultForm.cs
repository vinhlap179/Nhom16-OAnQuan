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
        // Trong ResultForm.cs

        // Thêm biến để biết là chơi với Bot hay Người
        private bool isOnlineMatch = false;

        // Sửa Constructor để nhận thêm cờ isOnline (mặc định false nếu không truyền)
        public ResultForm(int playerScore, int otherScore, bool isOnline = false)
        {
            InitializeComponent();
            this.playerScore = playerScore;
            this.botScore = otherScore; // Tạm gọi là botScore nhưng hiểu là điểm đối thủ
            this.isOnlineMatch = isOnline;
        }
        private async void ResultForm_Load(object sender, EventArgs e)
        {
            lbScorePlayer.Text = $"Điểm của bạn: {playerScore}";
            string otherLabel = isOnlineMatch ? "Điểm đối thủ" : "Điểm của máy";
            lbScoreBot.Text = $"{otherLabel}: {botScore}";
            // Nếu onl á thì cập nhật điểm cho cả hai người chơi, còn off thì chỉ cập nhật cho người chơi
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
