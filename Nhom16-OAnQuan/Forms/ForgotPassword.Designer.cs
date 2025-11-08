namespace Nhom16_OAnQuan.Forms
{
    partial class ForgotPassword
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
            UserBox = new TextBox();
            PassBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            ChangePassBtn = new Button();
            SuspendLayout();
            // 
            // UserBox
            // 
            UserBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            UserBox.Location = new Point(207, 103);
            UserBox.Name = "UserBox";
            UserBox.Size = new Size(306, 34);
            UserBox.TabIndex = 7;
            UserBox.TextChanged += UserBox_TextChanged;
            // 
            // PassBox
            // 
            PassBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            PassBox.Location = new Point(207, 167);
            PassBox.Name = "PassBox";
            PassBox.PasswordChar = '*';
            PassBox.Size = new Size(306, 34);
            PassBox.TabIndex = 8;
            PassBox.UseSystemPasswordChar = true;
            PassBox.TextChanged += PassBox_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(83, 109);
            label1.Name = "label1";
            label1.Size = new Size(94, 28);
            label1.TabIndex = 9;
            label1.Text = "Tài khoản";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(83, 167);
            label2.Name = "label2";
            label2.Size = new Size(94, 28);
            label2.TabIndex = 13;
            label2.Text = "Mật khẩu";
            // 
            // ChangePassBtn
            // 
            ChangePassBtn.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ChangePassBtn.Location = new Point(227, 237);
            ChangePassBtn.Name = "ChangePassBtn";
            ChangePassBtn.Size = new Size(204, 51);
            ChangePassBtn.TabIndex = 14;
            ChangePassBtn.Text = "Đổi mật khẩu ";
            ChangePassBtn.UseVisualStyleBackColor = true;
            ChangePassBtn.Click += ChangePassBtn_Click;
            // 
            // ForgotPassword
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ChangePassBtn);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(PassBox);
            Controls.Add(UserBox);
            Name = "ForgotPassword";
            Text = "ForgotPassword";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox UserBox;
        private TextBox PassBox;
        private Label label1;
        private Label label2;
        private Button ChangePassBtn;
    }
}