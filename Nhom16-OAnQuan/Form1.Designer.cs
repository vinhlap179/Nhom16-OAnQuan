namespace Nhom16_OAnQuan
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            buttonLogin = new Button();
            buttonSignUp = new Button();
            buttonRecover = new Button();
            SuspendLayout();
            // 
            // buttonLogin
            // 
            buttonLogin.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonLogin.Location = new Point(258, 190);
            buttonLogin.Name = "buttonLogin";
            buttonLogin.Size = new Size(233, 60);
            buttonLogin.TabIndex = 0;
            buttonLogin.Text = "Đăng nhập ";
            buttonLogin.UseVisualStyleBackColor = true;
            buttonLogin.Click += buttonLogin_Click;
            // 
            // buttonSignUp
            // 
            buttonSignUp.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonSignUp.Location = new Point(258, 106);
            buttonSignUp.Name = "buttonSignUp";
            buttonSignUp.Size = new Size(233, 60);
            buttonSignUp.TabIndex = 1;
            buttonSignUp.Text = "Đăng ký  ";
            buttonSignUp.UseVisualStyleBackColor = true;
            buttonSignUp.Click += buttonSignUp_Click;
            // 
            // buttonRecover
            // 
            buttonRecover.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonRecover.Location = new Point(258, 283);
            buttonRecover.Name = "buttonRecover";
            buttonRecover.Size = new Size(233, 60);
            buttonRecover.TabIndex = 2;
            buttonRecover.Text = "Quên mật khẩu";
            buttonRecover.UseVisualStyleBackColor = true;
            buttonRecover.Click += buttonRecover_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonRecover);
            Controls.Add(buttonSignUp);
            Controls.Add(buttonLogin);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button buttonLogin;
        private Button buttonSignUp;
        private Button buttonRecover;
    }
}
