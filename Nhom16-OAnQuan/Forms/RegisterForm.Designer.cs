namespace Nhom16_OAnQuan.Forms
{
    partial class RegisterForm
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
            label1 = new Label();
            label2 = new Label();
            UserBox = new TextBox();
            PassBox = new TextBox();
            BackToLoginBtn = new Button();
            SignUpBtn = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(141, 109);
            label1.Name = "label1";
            label1.Size = new Size(94, 28);
            label1.TabIndex = 2;
            label1.Text = "Tài khoản";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(141, 151);
            label2.Name = "label2";
            label2.Size = new Size(94, 28);
            label2.TabIndex = 3;
            label2.Text = "Mật khẩu";
            // 
            // UserBox
            // 
            UserBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            UserBox.Location = new Point(265, 109);
            UserBox.Name = "UserBox";
            UserBox.Size = new Size(306, 34);
            UserBox.TabIndex = 6;
            // 
            // PassBox
            // 
            PassBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            PassBox.Location = new Point(265, 151);
            PassBox.Name = "PassBox";
            PassBox.PasswordChar = '*';
            PassBox.Size = new Size(306, 34);
            PassBox.TabIndex = 7;
            PassBox.UseSystemPasswordChar = true;
            // 
            // BackToLoginBtn
            // 
            BackToLoginBtn.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BackToLoginBtn.Location = new Point(70, 211);
            BackToLoginBtn.Name = "BackToLoginBtn";
            BackToLoginBtn.Size = new Size(204, 51);
            BackToLoginBtn.TabIndex = 9;
            BackToLoginBtn.Text = "Trở về đăng nhập ";
            BackToLoginBtn.UseVisualStyleBackColor = true;
            BackToLoginBtn.Click += BackToLoginBtn_Click;
            // 
            // SignUpBtn
            // 
            SignUpBtn.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            SignUpBtn.Location = new Point(315, 211);
            SignUpBtn.Name = "SignUpBtn";
            SignUpBtn.Size = new Size(204, 51);
            SignUpBtn.TabIndex = 10;
            SignUpBtn.Text = "Đăng kí ";
            SignUpBtn.UseVisualStyleBackColor = true;
            SignUpBtn.Click += SignUpBtn_Click_1;
            // 
            // RegisterForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(SignUpBtn);
            Controls.Add(BackToLoginBtn);
            Controls.Add(PassBox);
            Controls.Add(UserBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "RegisterForm";
            Text = "RegisterForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox UserBox;
        private TextBox PassBox;
        private Button BackToLoginBtn;
        private Button SignUpBtn;
    }
}