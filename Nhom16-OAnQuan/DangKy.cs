using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using static Nhom16_OAnQuan.DangNhap;
using System.Windows.Forms;
using System.Security.Cryptography;
using FireSharp.Response;

namespace Nhom16_OAnQuan
{
    public partial class DangKy : Form
    {
        public DangKy()
        {
            InitializeComponent();
        }
        
        
        private async void btnRegister_Click(object sender, EventArgs e)
        {
            //Nếu client chưa được khởi tạo do DangKyLoad chưa chạy thì form sẽ tự động chạy lại
            if (client == null)
            {
                try
                {
                    client = new FireSharp.FirebaseClient(ifc);
                    if (client == null)
                    {
                        MessageBox.Show("Không thể kết nối Firebase. Kiểm tra AuthSecret hoặc BasePath!");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tạo client Firebase: " + ex.Message);
                    return;
                }
            }

            //Lấy dữ liệu của người dùng
            string username = txt_user.Text.Trim();
            string password = txt_pwd.Text;
            string confirm = txt_confirm.Text;

            // Kiểm tra các ô trống
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirm))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            // Kiểm tra mật khẩu trùng khớp
            if (password != confirm)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp!");
                return;
            }

            try
            {
                // 1. Kiểm tra tài khoản đã tồn tại chưa
                FirebaseResponse checkUser = await client.GetAsync("Users/" + username);

                if (checkUser.Body != "null")
                {
                    MessageBox.Show("Tên tài khoản đã tồn tại! Vui lòng chọn tên khác.");
                    return;
                }

                // 2. Băm mật khẩu
                string hashedPassword = EncryptSHA.GetData(password);

                // 3. Tạo đối tượng lưu vào Firebase
                UserData newUser = new UserData()
                {
                    UserName = username,
                    Password = hashedPassword
                };

                // 4. Ghi dữ liệu vào Firebase
                SetResponse response = await client.SetAsync("Users/" + username, newUser);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show("Đăng ký thành công! Bạn có thể đăng nhập ngay.");
                    this.Hide();
                    
                }
                else
                {
                    MessageBox.Show("Đăng ký thất bại, vui lòng thử lại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }
        private void txt_user_TextChanged(object sender, EventArgs e)
        {

        }

        // --- Cấu hình Firebase ---
        IFirebaseConfig ifc = new FirebaseConfig()
        {
            AuthSecret = "DzJSzDp6vNcDuWruaQWkdXMK4JWHihF41mZLuSiZ",
            BasePath = "https://nhom16-oanquan-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };

        IFirebaseClient client;

        

        // Khi load form: kiểm tra kết nối
        private async void DangKy_Load(object sender, EventArgs e)
        {
            try
            {
                //Tạo kết nối tới Firebase
                client = new FireSharp.FirebaseClient(ifc);
                //Gửi request để kiểm tra kết nối có hợp lệ không
                FirebaseResponse response = await client.GetAsync("/");
                MessageBox.Show("Kết nối Firebase thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối Firebase: {ex.Message}");
            }
        }

        // Lớp dữ liệu user
        public class UserData
        {
            public string UserName { get; set; }
            public string Password { get; set; } // Mật khẩu đã băm
        }

        // Hàm băm của Tùng núi
        public static class EncryptSHA
        {
            public static string GetData(string data)
            {
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data));
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    return builder.ToString();
                }
            }
        }

    }
}
