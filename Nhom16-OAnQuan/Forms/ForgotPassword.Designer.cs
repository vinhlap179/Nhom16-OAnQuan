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
            UserBox.BackColor = SystemColors.ControlDark;
            UserBox.Font = new Font("Press Start 2P", 9F);
            UserBox.ForeColor = SystemColors.ButtonHighlight;
            UserBox.Location = new Point(203, 90);
            UserBox.Name = "UserBox";
            UserBox.Size = new Size(306, 22);
            UserBox.TabIndex = 7;
            UserBox.TextChanged += UserBox_TextChanged;
            // 
            // PassBox
            // 
            PassBox.BackColor = SystemColors.ControlDark;
            PassBox.Font = new Font("Press Start 2P", 9F);
            PassBox.ForeColor = SystemColors.ButtonHighlight;
            PassBox.Location = new Point(203, 154);
            PassBox.Name = "PassBox";
            PassBox.PasswordChar = '*';
            PassBox.Size = new Size(306, 22);
            PassBox.TabIndex = 8;
            PassBox.UseSystemPasswordChar = true;
            PassBox.TextChanged += PassBox_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Press Start 2P", 9F);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(65, 93);
            label1.Name = "label1";
            label1.Size = new Size(130, 21);
            label1.TabIndex = 9;
            label1.Text = "Username";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Press Start 2P", 9F);
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(65, 154);
            label2.Name = "label2";
            label2.Size = new Size(130, 21);
            label2.TabIndex = 13;
            label2.Text = "Password";
            // 
            // ChangePassBtn
            // 
            ChangePassBtn.BackColor = Color.DimGray;
            ChangePassBtn.Font = new Font("Press Start 2P", 9F);
            ChangePassBtn.ForeColor = SystemColors.ButtonHighlight;
            ChangePassBtn.Location = new Point(223, 224);
            ChangePassBtn.Name = "ChangePassBtn";
            ChangePassBtn.Size = new Size(204, 51);
            ChangePassBtn.TabIndex = 14;
            ChangePassBtn.Text = "Change Password";
            ChangePassBtn.UseVisualStyleBackColor = false;
            ChangePassBtn.Click += ChangePassBtn_Click;
            // 
            // ForgotPassword
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(659, 351);
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