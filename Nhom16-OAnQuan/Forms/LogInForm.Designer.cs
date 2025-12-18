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
            LogInBtn.BackColor = Color.DimGray;
            LogInBtn.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            LogInBtn.ForeColor = SystemColors.ButtonHighlight;
            LogInBtn.Location = new Point(404, 215);
            LogInBtn.Name = "LogInBtn";
            LogInBtn.Size = new Size(204, 51);
            LogInBtn.TabIndex = 18;
            LogInBtn.Text = "Login";
            LogInBtn.UseVisualStyleBackColor = false;
            LogInBtn.Click += LogInBtn_Click;
            // 
            // SignUpBtn
            // 
            SignUpBtn.BackColor = Color.DimGray;
            SignUpBtn.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            SignUpBtn.ForeColor = SystemColors.ButtonHighlight;
            SignUpBtn.Location = new Point(152, 215);
            SignUpBtn.Name = "SignUpBtn";
            SignUpBtn.Size = new Size(204, 51);
            SignUpBtn.TabIndex = 17;
            SignUpBtn.Text = "Register";
            SignUpBtn.UseVisualStyleBackColor = false;
            SignUpBtn.Click += SignUpBtn_Click;
            // 
            // PassBox
            // 
            PassBox.BackColor = SystemColors.ControlDark;
            PassBox.Font = new Font("Microsoft Sans Serif", 7.8F);
            PassBox.ForeColor = SystemColors.Control;
            PassBox.Location = new Point(302, 165);
            PassBox.Name = "PassBox";
            PassBox.PasswordChar = '*';
            PassBox.Size = new Size(306, 22);
            PassBox.TabIndex = 15;
            PassBox.UseSystemPasswordChar = true;
            // 
            // UserBox
            // 
            UserBox.BackColor = SystemColors.ControlDark;
            UserBox.Font = new Font("Microsoft Sans Serif", 7.8F);
            UserBox.ForeColor = SystemColors.Control;
            UserBox.Location = new Point(302, 103);
            UserBox.Name = "UserBox";
            UserBox.Size = new Size(306, 22);
            UserBox.TabIndex = 14;
            UserBox.TextChanged += UserBox_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(64, 64, 64);
            label2.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(152, 161);
            label2.Name = "label2";
            label2.Size = new Size(98, 25);
            label2.TabIndex = 12;
            label2.Text = "Password";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(64, 64, 64);
            label1.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(152, 99);
            label1.Name = "label1";
            label1.Size = new Size(102, 25);
            label1.TabIndex = 11;
            label1.Text = "Username";
            label1.Click += label1_Click;
            // 
            // forgotPassBtn
            // 
            forgotPassBtn.BackColor = Color.DimGray;
            forgotPassBtn.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            forgotPassBtn.ForeColor = SystemColors.ButtonHighlight;
            forgotPassBtn.Location = new Point(152, 292);
            forgotPassBtn.Name = "forgotPassBtn";
            forgotPassBtn.Size = new Size(450, 51);
            forgotPassBtn.TabIndex = 19;
            forgotPassBtn.Text = "Forgot password";
            forgotPassBtn.UseVisualStyleBackColor = false;
            forgotPassBtn.Click += forgotPassBtn_Click;
            // 
            // LogInForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(787, 438);
            Controls.Add(forgotPassBtn);
            Controls.Add(LogInBtn);
            Controls.Add(SignUpBtn);
            Controls.Add(PassBox);
            Controls.Add(UserBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "LogInForm";
            Text = "LogInForm";
            Load += LogInForm_Load;
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