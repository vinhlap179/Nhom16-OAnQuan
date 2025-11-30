using Google.Cloud.Firestore;
using Nhom16_OAnQuan.Classes;
using Nhom16_OAnQuan.Forms.GameForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nhom16_OAnQuan.Forms
{
    public partial class LogInForm : Form
    {
        public LogInForm()
        {
            InitializeComponent();
        }

        private void SignUpBtn_Click(object sender, EventArgs e)
        {
            RegisterForm form = new RegisterForm();
            this.Hide();
            form.ShowDialog();
            this.Show();
        }

        private void LogInBtn_Click(object sender, EventArgs e)
        {
            string username = UserBox.Text.Trim();
            string password = PassBox.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tên tài khoản và Mật khẩu.", "Lỗi Đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var db = FirestoreHelper.Database;

                DocumentReference docRef = db.Collection("UserData").Document(username);

                UserData data = docRef.GetSnapshotAsync().Result.ConvertTo<UserData>();

                if (data != null)
                {
                    if (password == Security.Decrypt(data.Password))
                    {
                        MessageBox.Show("Success");
                        GlobalUserSession.CurrentUsername = username;
                        GlobalUserSession.CurrentToken = Guid.NewGuid().ToString();

                        StartingGUI startingForm = new StartingGUI();


                        startingForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Đăng nhập thất bại. Vui lòng kiểm tra lại tài khoản và mật khẩu.");
                    }
                }
                else
                {
                    MessageBox.Show("Đăng nhập thất bại. Vui lòng kiểm tra lại tài khoản và mật khẩu.");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("error");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi hệ thống: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void forgotPassBtn_Click(object sender, EventArgs e)
        {
            ForgotPassword form = new ForgotPassword();
            this.Hide();
            form.ShowDialog();
            this.Show();
        }

        private void UserBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
