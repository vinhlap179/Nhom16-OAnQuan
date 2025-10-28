namespace Nhom16_OAnQuan
{
    partial class DangNhap
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnLogin = new Button();
            txt_user = new TextBox();
            txt_pwd = new TextBox();
            SuspendLayout();
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(252, 248);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(137, 53);
            btnLogin.TabIndex = 0;
            btnLogin.Text = "button1";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // txt_user
            // 
            txt_user.Location = new Point(192, 60);
            txt_user.Name = "txt_user";
            txt_user.Size = new Size(345, 27);
            txt_user.TabIndex = 1;
            txt_user.TextChanged += txt_user_TextChanged;
            // 
            // txt_pwd
            // 
            txt_pwd.Location = new Point(198, 141);
            txt_pwd.Name = "txt_pwd";
            txt_pwd.Size = new Size(339, 27);
            txt_pwd.TabIndex = 2;
            txt_pwd.UseSystemPasswordChar = true;
            // 
            // DangNhap
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txt_pwd);
            Controls.Add(txt_user);
            Controls.Add(btnLogin);
            Name = "DangNhap";
            Text = "DangNhap";
            Load += DangNhap_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnLogin;
        private TextBox txt_user;
        private TextBox txt_pwd;
    }
}