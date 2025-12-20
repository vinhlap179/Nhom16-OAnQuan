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
        public partial class ChangePassword : Form
        {
            public ChangePassword()
            {
                InitializeComponent();
            }

            private async void ChangePassBtn_Click(object sender, EventArgs e)
            {
                string username = UserBox.Text.Trim();
                string oldPassword = PassBox.Text; // Mật khẩu cũ người dùng nhập
                string newPassword = newPassBox.Text; // Mật khẩu mới người dùng nhập

                // 1. Kiểm tra đầu vào trống
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ tên tài khoản, mật khẩu cũ và mật khẩu mới.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

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

                    // 2. Kiểm tra tài khoản tồn tại
                    if (!snapshot.Exists)
                    {
                        MessageBox.Show("Tên người dùng không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    UserData data = snapshot.ConvertTo<UserData>();

                    // 3. Giải mã mật khẩu cũ từ DB và so sánh với mật khẩu cũ người dùng vừa nhập
                    string decryptedDbPassword = Security.Decrypt(data.Password);

                    if (oldPassword != decryptedDbPassword)
                    {
                        MessageBox.Show("Mật khẩu cũ không chính xác!", "Lỗi xác thực", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    // 4. Kiểm tra mật khẩu mới không trùng mật khẩu cũ (tùy chọn nhưng nên có)
                    if (oldPassword == newPassword)
                    {
                        MessageBox.Show("Mật khẩu mới không được trùng với mật khẩu cũ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // 5. Mã hóa mật khẩu mới và cập nhật lên Firestore
                    string hashedNewPassword = Security.Encrypt(newPassword);
                    Dictionary<string, object> updates = new Dictionary<string, object>
                    {
                        { nameof(UserData.Password), hashedNewPassword }
                    };

                    await docRef.UpdateAsync(updates);

                    MessageBox.Show("Đổi mật khẩu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Đã xảy ra lỗi hệ thống: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
