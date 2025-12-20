using Google.Cloud.Firestore;
using Nhom16_OAnQuan.Classes;
using System;
using System.Text.RegularExpressions; // Thêm thư viện này để dùng Regex
using System.Windows.Forms;

namespace Nhom16_OAnQuan.Forms
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }
        private bool ValidateInput()
        {
            string username = UserBox.Text.Trim();
            string password = PassBox.Text;

            // Kiểm tra bỏ trống
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tên đăng nhập và Mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Kiểm tra ký tự không phù hợp cho Username
            // Quy tắc: Chỉ cho phép chữ cái và số, độ dài từ 4-20 ký tự, không có khoảng trắng
            var usernameRegex = new Regex(@"^[a-zA-Z0-9]{4,20}$");
            if (!usernameRegex.IsMatch(username))
            {
                MessageBox.Show("Tên đăng nhập phải từ 4-20 ký tự, chỉ gồm chữ cái và số, không chứa ký tự đặc biệt!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Kiểm tra độ dài mật khẩu
            if (password.Length < 4)
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 4 ký tự!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void SignUpBtn_Click_1(object sender, EventArgs e)
        {
            //Kiểm tra nhập liệu 
            if (!ValidateInput())
            {
                return; 
            }

            var db = FirestoreHelper.Database;

            //Kiểm tra tồn tại
            if (CheckIfUserAlreadyExist())
            {
                MessageBox.Show("Người dùng đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                var data = GetWriteData();
                DocumentReference docRef = db.Collection("UserData").Document(data.Username);
                docRef.SetAsync(data);
                MessageBox.Show("Đăng ký tài khoản thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Sau khi đăng ký thành công có thể chuyển về form đăng nhập
                BackToLoginBtn_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kết nối cơ sở dữ liệu: " + ex.Message);
            }
        }
        private UserData GetWriteData()
        {
            string username = UserBox.Text.Trim();
            // Mật khẩu đã được mã hóa trước khi gửi lên Firestore
            string password = Security.Encrypt(PassBox.Text);

            return new UserData()
            {
                Username = username,
                Password = password,
            };
        }
        private bool CheckIfUserAlreadyExist()
        {
            string username = UserBox.Text.Trim();
            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("UserData").Document(username);
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
    }
}