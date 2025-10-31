using Google.Cloud.Firestore;
using Nhom16_OAnQuan.Classes;
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
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void SignUpBtn_Click(object sender, EventArgs e)
        {

        }

        private void BackToLoginBtn_Click_1(object sender, EventArgs e)
        {

        }

        private void BackToLoginBtn_Click(object sender, EventArgs e)
        {
            Hide();
            LogInForm form = new LogInForm();
            form.ShowDialog();
            Close();
        }

        private void SignUpBtn_Click_1(object sender, EventArgs e)
        {
            var db = FirestoreHelper.Database;
            if (CheckIfUserAlreadyExist())
            {
                MessageBox.Show("Người dùng đã tồn tại");
                return;
            }
            var data = GetWriteData();
            DocumentReference docRef = db.Collection("UserData").Document(data.Username);
            docRef.SetAsync(data);
            MessageBox.Show("Sucess");
        }

        private UserData GetWriteData()
        {
            string username = UserBox.Text.Trim();
            string password =  Security.Encrypt(PassBox.Text);
            int zip = Convert.ToInt32(ZipBox.Text);

            return new UserData()
            {
                Username = username,
                Password = password,
                ZipCode = zip,
            };
        }

        private bool CheckIfUserAlreadyExist()
        {
            string username = UserBox.Text.Trim();
            string password = PassBox.Text;

            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("UserData").Document(username);
            UserData data = docRef.GetSnapshotAsync().Result.ConvertTo<UserData>();

            if (data != null)
            {
                return true; 
            }
            else
            {
                return false;
            }
        }
    }
}
