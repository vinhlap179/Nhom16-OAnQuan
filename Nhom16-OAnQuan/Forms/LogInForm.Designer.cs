namespace Nhom16_OAnQuan.Forms
{
    partial class LogInForm
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
            LogInBtn = new Button();
            SignUpBtn = new Button();
            PassBox = new TextBox();
            UserBox = new TextBox();
            label2 = new Label();
            label1 = new Label();
            forgotPassBtn = new Button();
            SuspendLayout();
            // 
            // LogInBtn
            // 
            LogInBtn.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            LogInBtn.Location = new Point(374, 237);
            LogInBtn.Name = "LogInBtn";
            LogInBtn.Size = new Size(204, 51);
            LogInBtn.TabIndex = 18;
            LogInBtn.Text = "Đăng nhập";
            LogInBtn.UseVisualStyleBackColor = true;
            LogInBtn.Click += LogInBtn_Click;
            // 
            // SignUpBtn
            // 
            SignUpBtn.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            SignUpBtn.Location = new Point(128, 237);
            SignUpBtn.Name = "SignUpBtn";
            SignUpBtn.Size = new Size(204, 51);
            SignUpBtn.TabIndex = 17;
            SignUpBtn.Text = "Đăng kí ";
            SignUpBtn.UseVisualStyleBackColor = true;
            SignUpBtn.Click += SignUpBtn_Click;
            // 
            // PassBox
            // 
            PassBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            PassBox.Location = new Point(302, 161);
            PassBox.Name = "PassBox";
            PassBox.PasswordChar = '*';
            PassBox.Size = new Size(306, 34);
            PassBox.TabIndex = 15;
            PassBox.UseSystemPasswordChar = true;
            // 
            // UserBox
            // 
            UserBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            UserBox.Location = new Point(302, 119);
            UserBox.Name = "UserBox";
            UserBox.Size = new Size(306, 34);
            UserBox.TabIndex = 14;
            UserBox.TextChanged += UserBox_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(178, 161);
            label2.Name = "label2";
            label2.Size = new Size(94, 28);
            label2.TabIndex = 12;
            label2.Text = "Mật khẩu";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(178, 119);
            label1.Name = "label1";
            label1.Size = new Size(94, 28);
            label1.TabIndex = 11;
            label1.Text = "Tài khoản";
            label1.Click += label1_Click;
            // 
            // forgotPassBtn
            // 
            forgotPassBtn.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            forgotPassBtn.Location = new Point(128, 318);
            forgotPassBtn.Name = "forgotPassBtn";
            forgotPassBtn.Size = new Size(450, 51);
            forgotPassBtn.TabIndex = 19;
            forgotPassBtn.Text = "Quên mật khẩu";
            forgotPassBtn.UseVisualStyleBackColor = true;
            forgotPassBtn.Click += forgotPassBtn_Click;
            // 
            // LogInForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(forgotPassBtn);
            Controls.Add(LogInBtn);
            Controls.Add(SignUpBtn);
            Controls.Add(PassBox);
            Controls.Add(UserBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "LogInForm";
            Text = "LogInForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button LogInBtn;
        private Button SignUpBtn;
        private TextBox PassBox;
        private TextBox UserBox;
        private Label label2;
        private Label label1;
        private Button forgotPassBtn;
    }
}