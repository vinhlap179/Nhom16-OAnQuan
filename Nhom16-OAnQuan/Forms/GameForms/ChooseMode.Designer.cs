namespace Nhom16_OAnQuan.Forms.GameForms
{
    partial class ChooseMode
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
            PlayOfflineBtn = new Button();
            PlayOnlineBtn = new Button();
            SuspendLayout();
            // 
            // PlayOfflineBtn
            // 
            PlayOfflineBtn.BackColor = Color.Gray;
            PlayOfflineBtn.Font = new Font("Press Start 2P", 9F);
            PlayOfflineBtn.ForeColor = SystemColors.ButtonHighlight;
            PlayOfflineBtn.Location = new Point(190, 186);
            PlayOfflineBtn.Name = "PlayOfflineBtn";
            PlayOfflineBtn.Size = new Size(204, 56);
            PlayOfflineBtn.TabIndex = 23;
            PlayOfflineBtn.Text = "Play Offline";
            PlayOfflineBtn.UseVisualStyleBackColor = false;
            PlayOfflineBtn.Click += PlayOfflineBtn_Click;
            // 
            // PlayOnlineBtn
            // 
            PlayOnlineBtn.BackColor = Color.Gray;
            PlayOnlineBtn.Font = new Font("Press Start 2P", 9F);
            PlayOnlineBtn.ForeColor = SystemColors.ButtonHighlight;
            PlayOnlineBtn.Location = new Point(190, 80);
            PlayOnlineBtn.Name = "PlayOnlineBtn";
            PlayOnlineBtn.Size = new Size(204, 56);
            PlayOnlineBtn.TabIndex = 24;
            PlayOnlineBtn.Text = "Play Online";
            PlayOnlineBtn.UseVisualStyleBackColor = false;
            PlayOnlineBtn.Click += PlayOnlineBtn_Click;
            // 
            // ChooseMode
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(624, 339);
            Controls.Add(PlayOnlineBtn);
            Controls.Add(PlayOfflineBtn);
            Name = "ChooseMode";
            Text = "ChooseMode";
            ResumeLayout(false);
        }

        #endregion

        private Button PlayOfflineBtn;
        private Button PlayOnlineBtn;
    }
}