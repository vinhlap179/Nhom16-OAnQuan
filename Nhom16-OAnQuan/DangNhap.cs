// Các thư viện cần thiết
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.IO;
using System.Net;
using System.Security.Cryptography; // Cần cho băm mật khẩu
using System.Text;                   // Cần cho băm mật khẩu
using System.Windows.Forms;

namespace Nhom16_OAnQuan
{
    // LỚP 1: ĐỊNH NGHĨA DỮ LIỆU NGƯỜI DÙNG (BẮT BUỘC CÓ)
    // (Khuôn mẫu để đọc dữ liệu user từ Firebase)
    public class UserData
    {
        public string UserName { get; set; }
        public string Password { get; set; } // Mật khẩu đã băm
    }

    // LỚP 2: BĂM MẬT KHẨU (BẮT BUỘC CÓ)
    // (Phải dùng CÙNG MỘT cách băm khi đăng ký và đăng nhập)
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

    // LỚP 3: FORM ĐĂNG NHẬP CỦA BẠN
    public partial class DangNhap : Form
    {
        // --- Cấu hình FireSharp ---
        IFirebaseConfig ifc = new FirebaseConfig()
        {
            AuthSecret = "ZbGtGNeAorzKTN042TXKZv3jU70Fn3Yf47YK10cYQT",
            BasePath = "https://test-b658c-default-rtdb.asia-southeast1.firebaseio.com/"
        };

        // Client để kết nối
        IFirebaseClient client;
        // -------------------------

        public DangNhap()
        {
            InitializeComponent();
        }

        // === CẬP NHẬT 1: KIỂM TRA KẾT NỐI KHI LOAD ===
        private async void DangNhap_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. Khởi tạo client
                client = new FireSharp.FirebaseClient(ifc);

                // 2. Thử kết nối bằng cách đọc dữ liệu ở gốc ("/")
                FirebaseResponse response = await client.GetAsync("/");

                // 3. Thông báo thành công
                MessageBox.Show("Kết nối Firebase Realtime Database thành công!");
            }
            catch (Exception ex)
            {
                // Bất kỳ lỗi nào (sai AuthSecret, mất mạng) sẽ bị bắt ở đây
                MessageBox.Show($"Lỗi kết nối Firebase: {ex.Message}");
            }
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txt_user.Text;
            string password = txt_pwd.Text;

            // === CẬP NHẬT 2: KIỂM TRA TEXTBOX RỖNG (ĐÃ CÓ) ===
            // Đây chính là code kiểm tra 2 ô text có chữ hay không
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return; // Dừng lại, không chạy code bên dưới
            }

            // --- Các bước đăng nhập ---
            try
            {
                // 1. Lấy dữ liệu user từ Firebase theo Tên đăng nhập
                FirebaseResponse response = await client.GetAsync("Users/" + username);

                // 2. Kiểm tra user có tồn tại không
                if (response.Body == "null")
                {
                    MessageBox.Show("Tài khoản không tồn tại!");
                    return;
                }

                // 3. Chuyển dữ liệu JSON về object UserData
                UserData userData = response.ResultAs<UserData>();

                // 4. Băm mật khẩu mà người dùng vừa nhập
                string hashedInputPassword = EncryptSHA.GetData(password);

                // 5. So sánh mật khẩu vừa băm với mật khẩu đã lưu trên Firebase
                if (hashedInputPassword == userData.Password)
                {
                    MessageBox.Show($"Đăng nhập thành công! Chào mừng {userData.UserName}");

                    // TODO: Mở form chính (main form) của bạn tại đây
                    // Ví dụ:
                    // FormMainMenu mainMenu = new FormMainMenu();
                    // mainMenu.Show();
                    // this.Hide();
                }
                else
                {
                    MessageBox.Show("Sai mật khẩu!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }
    }
}