namespace Nhom16_OAnQuan
{
    partial class DangKy
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
            txt_user = new TextBox();
            txt_pwd = new TextBox();
            txt_confirm = new TextBox();
            btnRegister = new Button();
            fileSystemWatcher1 = new FileSystemWatcher();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).BeginInit();
            SuspendLayout();
            // 
            // txt_user
            // 
            txt_user.Font = new Font("Segoe UI", 13F);
            txt_user.Location = new Point(414, 47);
            txt_user.Name = "txt_user";
            txt_user.Size = new Size(277, 36);
            txt_user.TabIndex = 0;
            // 
            // txt_pwd
            // 
            txt_pwd.Font = new Font("Segoe UI", 12F);
            txt_pwd.Location = new Point(414, 117);
            txt_pwd.Name = "txt_pwd";
            txt_pwd.Size = new Size(277, 34);
            txt_pwd.TabIndex = 1;
            // 
            // txt_confirm
            // 
            txt_confirm.Font = new Font("Segoe UI", 12F);
            txt_confirm.Location = new Point(414, 201);
            txt_confirm.Name = "txt_confirm";
            txt_confirm.Size = new Size(277, 34);
            txt_confirm.TabIndex = 2;
            // 
            // btnRegister
            // 
            btnRegister.Font = new Font("Segoe UI", 14F);
            btnRegister.Location = new Point(246, 286);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(185, 68);
            btnRegister.TabIndex = 3;
            btnRegister.Text = "Đăng Ký";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // fileSystemWatcher1
            // 
            fileSystemWatcher1.EnableRaisingEvents = true;
            fileSystemWatcher1.SynchronizingObject = this;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F);
            label1.Location = new Point(54, 47);
            label1.Name = "label1";
            label1.Size = new Size(158, 32);
            label1.TabIndex = 4;
            label1.Text = "Tên tài khoản";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14F);
            label2.Location = new Point(54, 117);
            label2.Name = "label2";
            label2.Size = new Size(115, 32);
            label2.TabIndex = 5;
            label2.Text = "Mật khẩu";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14F);
            label3.Location = new Point(54, 201);
            label3.Name = "label3";
            label3.Size = new Size(219, 32);
            label3.TabIndex = 6;
            label3.Text = "Xác nhận mật khẩu";
            // 
            // DangKy
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnRegister);
            Controls.Add(txt_confirm);
            Controls.Add(txt_pwd);
            Controls.Add(txt_user);
            Name = "DangKy";
            Text = "DangKy";
            ((System.ComponentModel.ISupportInitialize)fileSystemWatcher1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txt_user;
        private TextBox txt_pwd;
        private TextBox txt_confirm;
        private Button btnRegister;
        private FileSystemWatcher fileSystemWatcher1;
        private Label label3;
        private Label label2;
        private Label label1;
    }
}