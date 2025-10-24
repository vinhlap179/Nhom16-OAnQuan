namespace Nhom16_OAnQuan
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Vinh Lap da o day 
        }

        private void buttonSignUp_Click(object sender, EventArgs e)
        {
            var Newform = new DangKy();
            Newform.ShowDialog();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            var Newform = new DangNhap();
            Newform.ShowDialog();
        }

        private void buttonRecover_Click(object sender, EventArgs e)
        {
            var Newform = new QuenMatKhau();
            Newform.ShowDialog();
        }
    }
}
