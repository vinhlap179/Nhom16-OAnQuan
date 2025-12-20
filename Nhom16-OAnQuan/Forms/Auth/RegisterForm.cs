using Google.Cloud.Firestore;
using Nhom16_OAnQuan.Classes;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Nhom16_OAnQuan.Forms
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        // 1. Hàm kiểm tra tính hợp lệ (Validation)
        private bool ValidateInput()
        {
            string username = UserBox.Text.Trim();
            string password = PassBox.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tên đăng nhập và Mật khẩu!", "Thông báo");
                return false;
            }

            // Chỉ cho phép chữ và số, từ 4-20 ký tự
            var usernameRegex = new Regex(@"^[a-zA-Z0-9]{4,20}$");
            if (!usernameRegex.IsMatch(username))
            {
                MessageBox.Show("Tên đăng nhập không hợp lệ (4-20 ký tự, không chứa ký tự đặc biệt)!");
                return false;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự!");
                return false;
            }

            return true;
        }

        private void SignUpBtn_Click_1(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            var db = FirestoreHelper.Database;
            if (CheckIfUserAlreadyExist())
            {
                MessageBox.Show("Người dùng đã tồn tại");
                return;
            }

            try
            {
                var data = GetWriteData();
                DocumentReference docRef = db.Collection("UserData").Document(data.Username);
                docRef.SetAsync(data);
                MessageBox.Show("Đăng ký thành công!");
                BackToLoginBtn_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private UserData GetWriteData()
        {
            return new UserData()
            {
                Username = UserBox.Text.Trim(),
                Password = Security.Encrypt(PassBox.Text),
            };
        }

        private bool CheckIfUserAlreadyExist()
        {
            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("UserData").Document(UserBox.Text.Trim());
            var snapshot = docRef.GetSnapshotAsync().Result;
            return snapshot.Exists;
        }

        private void BackToLoginBtn_Click(object sender, EventArgs e)
        {
            Hide();
            LogInForm form = new LogInForm();
            form.ShowDialog();
            Close();
        }
        private void UserBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void PassBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
        }
    }
}