using Google.Cloud.Firestore;
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

namespace Nhom16_OAnQuan.Forms
{
    public partial class ForgotPassword : Form
    {
        public ForgotPassword()
        {
            InitializeComponent();
        }

        private async void ChangePassBtn_Click(object sender, EventArgs e)
        {
            string username = UserBox.Text.Trim();
            string newPassword = PassBox.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(newPassword))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên người dùng và mật khẩu mới.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string hashedPassword = Security.Encrypt(newPassword);

            try
            {
                var db = FirestoreHelper.Database;
                if (db == null)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DocumentReference docRef = db.Collection("UserData").Document(username);

                DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

                if (!snapshot.Exists)
                {
                    MessageBox.Show("Tên người dùng không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Dictionary<string, object> updates = new Dictionary<string, object>
                {
                    { nameof(UserData.Password), hashedPassword }
                };

                await docRef.UpdateAsync(updates);

                MessageBox.Show("Đặt lại mật khẩu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UserBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void PassBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
