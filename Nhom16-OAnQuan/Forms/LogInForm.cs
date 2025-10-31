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
    public partial class LogInForm : Form
    {
        public LogInForm()
        {
            InitializeComponent();
        }

        private void SignUpBtn_Click(object sender, EventArgs e)
        {
            Hide();
            RegisterForm form = new RegisterForm();
            form.ShowDialog();
            Close();
        }

        private void LogInBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string username = UserBox.Text.Trim();
                string password = PassBox.Text;

                var db = FirestoreHelper.Database;
                DocumentReference docRef = db.Collection("UserData").Document(username);
                UserData data = docRef.GetSnapshotAsync().Result.ConvertTo<UserData>();

                if (data != null)
                {
                    if (password == Security.Decrypt(data.Password))
                    {
                        MessageBox.Show("Success");
                    }
                    else
                    {
                        MessageBox.Show("Login Failed");
                    }
                }
                else
                {
                    MessageBox.Show("Login Failed");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("error");
            }

        }
    }
}
