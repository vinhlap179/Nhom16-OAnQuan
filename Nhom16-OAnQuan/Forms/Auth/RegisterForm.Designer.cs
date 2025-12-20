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
            label1.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(121, 120);
            label1.Name = "label1";
            label1.Size = new Size(77, 18);
            label1.TabIndex = 2;
            label1.Text = "Username";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(64, 64, 64);
            label2.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.Control;
            label2.Location = new Point(121, 151);
            label2.Name = "label2";
            label2.Size = new Size(75, 18);
            label2.TabIndex = 3;
            label2.Text = "Password";
            // 
            // UserBox
            // 
            UserBox.BackColor = SystemColors.ControlDark;
            UserBox.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            UserBox.ForeColor = SystemColors.Window;
            UserBox.Location = new Point(265, 109);
            UserBox.Name = "UserBox";
            UserBox.Size = new Size(306, 30);
            UserBox.TabIndex = 6;
            UserBox.TextChanged += UserBox_TextChanged;
            // 
            // PassBox
            // 
            PassBox.BackColor = SystemColors.ControlDark;
            PassBox.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            PassBox.ForeColor = SystemColors.Window;
            PassBox.Location = new Point(265, 151);
            PassBox.Name = "PassBox";
            PassBox.PasswordChar = '*';
            PassBox.Size = new Size(306, 30);
            PassBox.TabIndex = 7;
            PassBox.UseSystemPasswordChar = true;
            PassBox.TextChanged += PassBox_TextChanged;
            // 
            // BackToLoginBtn
            // 
            BackToLoginBtn.BackColor = Color.DimGray;
            BackToLoginBtn.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BackToLoginBtn.ForeColor = SystemColors.ButtonHighlight;
            BackToLoginBtn.Location = new Point(121, 205);
            BackToLoginBtn.Name = "BackToLoginBtn";
            BackToLoginBtn.Size = new Size(203, 62);
            BackToLoginBtn.TabIndex = 9;
            BackToLoginBtn.Text = "Back to login";
            BackToLoginBtn.UseVisualStyleBackColor = false;
            BackToLoginBtn.Click += BackToLoginBtn_Click;
            // 
            // SignUpBtn
            // 
            SignUpBtn.BackColor = Color.DimGray;
            SignUpBtn.BackgroundImageLayout = ImageLayout.Center;
            SignUpBtn.FlatAppearance.BorderSize = 0;
            SignUpBtn.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            SignUpBtn.ForeColor = SystemColors.ButtonHighlight;
            SignUpBtn.Location = new Point(371, 205);
            SignUpBtn.Name = "SignUpBtn";
            SignUpBtn.Size = new Size(200, 62);
            SignUpBtn.TabIndex = 10;
            SignUpBtn.Text = "Signup ";
            SignUpBtn.UseVisualStyleBackColor = false;
            SignUpBtn.Click += SignUpBtn_Click_1;
            // 
            // RegisterForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(744, 376);
            Controls.Add(SignUpBtn);
            Controls.Add(BackToLoginBtn);
            Controls.Add(PassBox);
            Controls.Add(UserBox);
            Controls.Add(label2);
            Controls.Add(label1);
            DoubleBuffered = true;
            Name = "RegisterForm";
            Text = "RegisterForm";
            Load += RegisterForm_Load;
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