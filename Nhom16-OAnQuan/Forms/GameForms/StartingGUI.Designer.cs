namespace Nhom16_OAnQuan.Forms.GameForms
{
    partial class StartingGUI
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
            SignOutBtn = new Button();
            GuideBtn = new Button();
            PlayerInformationBtn = new Button();
            PlayBtn = new Button();
            btnLobby = new Button();
            SuspendLayout();
            // 
            // SignOutBtn
            // 
            SignOutBtn.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            SignOutBtn.Location = new Point(782, 506);
            SignOutBtn.Name = "SignOutBtn";
            SignOutBtn.Size = new Size(204, 51);
            SignOutBtn.TabIndex = 19;
            SignOutBtn.Text = "Đăng xuất";
            SignOutBtn.UseVisualStyleBackColor = true;
            SignOutBtn.Click += SignOutBtn_Click;
            // 
            // GuideBtn
            // 
            GuideBtn.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            GuideBtn.Location = new Point(782, 433);
            GuideBtn.Name = "GuideBtn";
            GuideBtn.Size = new Size(204, 51);
            GuideBtn.TabIndex = 20;
            GuideBtn.Text = "Hướng Dẫn";
            GuideBtn.UseVisualStyleBackColor = true;
            GuideBtn.Click += GuideBtn_Click;
            // 
            // PlayerInformationBtn
            // 
            PlayerInformationBtn.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            PlayerInformationBtn.Location = new Point(782, 355);
            PlayerInformationBtn.Name = "PlayerInformationBtn";
            PlayerInformationBtn.Size = new Size(204, 51);
            PlayerInformationBtn.TabIndex = 21;
            PlayerInformationBtn.Text = "Thông tin người chơi";
            PlayerInformationBtn.UseVisualStyleBackColor = true;
            PlayerInformationBtn.Click += PlayerInformationBtn_Click;
            // 
            // PlayBtn
            // 
            PlayBtn.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            PlayBtn.Location = new Point(782, 279);
            PlayBtn.Name = "PlayBtn";
            PlayBtn.Size = new Size(204, 51);
            PlayBtn.TabIndex = 22;
            PlayBtn.Text = "Chơi Offline";
            PlayBtn.UseVisualStyleBackColor = true;
            PlayBtn.Click += PlayBtn_Click;
            // 
            // btnLobby
            // 
            btnLobby.Font = new Font("Segoe UI", 14F);
            btnLobby.Location = new Point(782, 171);
            btnLobby.Name = "btnLobby";
            btnLobby.Size = new Size(204, 61);
            btnLobby.TabIndex = 23;
            btnLobby.Text = "Chơi Online";
            btnLobby.UseVisualStyleBackColor = true;
            btnLobby.Click += btnLobby_Click;
            // 
            // StartingGUI
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1045, 581);
            Controls.Add(btnLobby);
            Controls.Add(PlayBtn);
            Controls.Add(PlayerInformationBtn);
            Controls.Add(GuideBtn);
            Controls.Add(SignOutBtn);
            Name = "StartingGUI";
            Text = "StartingGUI";
            Load += StartingGUI_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button SignOutBtn;
        private Button GuideBtn;
        private Button PlayerInformationBtn;
        private Button PlayBtn;
        private Button btnLobby;
    }
}