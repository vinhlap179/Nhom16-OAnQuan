namespace Nhom16_OAnQuan.Forms.GameForms
{
    partial class ResultForm
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
            lbScorePlayer = new Label();
            lbScoreBot = new Label();
            lbKq = new Label();
            btClose = new Button();
            SuspendLayout();
            // 
            // lbScorePlayer
            // 
            lbScorePlayer.AutoSize = true;
            lbScorePlayer.BackColor = Color.Transparent;
            lbScorePlayer.Font = new Font("Press Start 2P", 9F);
            lbScorePlayer.ForeColor = SystemColors.ButtonHighlight;
            lbScorePlayer.Location = new Point(84, 56);
            lbScorePlayer.Name = "lbScorePlayer";
            lbScorePlayer.Size = new Size(220, 21);
            lbScorePlayer.TabIndex = 0;
            lbScorePlayer.Text = "Player Scores ";
            // 
            // lbScoreBot
            // 
            lbScoreBot.AutoSize = true;
            lbScoreBot.BackColor = Color.Transparent;
            lbScoreBot.Font = new Font("Press Start 2P", 9F);
            lbScoreBot.ForeColor = SystemColors.ButtonHighlight;
            lbScoreBot.Location = new Point(84, 110);
            lbScoreBot.Name = "lbScoreBot";
            lbScoreBot.Size = new Size(160, 21);
            lbScoreBot.TabIndex = 1;
            lbScoreBot.Text = "Bot Scores";
            // 
            // lbKq
            // 
            lbKq.AutoSize = true;
            lbKq.BackColor = Color.Transparent;
            lbKq.Font = new Font("Press Start 2P", 9F);
            lbKq.ForeColor = SystemColors.ButtonFace;
            lbKq.Location = new Point(84, 169);
            lbKq.Name = "lbKq";
            lbKq.Size = new Size(100, 21);
            lbKq.TabIndex = 2;
            lbKq.Text = "Result";
            // 
            // btClose
            // 
            btClose.BackColor = Color.Transparent;
            btClose.FlatAppearance.BorderSize = 0;
            btClose.FlatStyle = FlatStyle.Flat;
            btClose.Font = new Font("Press Start 2P", 9F);
            btClose.ForeColor = SystemColors.ButtonHighlight;
            btClose.Location = new Point(224, 244);
            btClose.Name = "btClose";
            btClose.Size = new Size(141, 56);
            btClose.TabIndex = 3;
            btClose.Text = "BACK";
            btClose.UseVisualStyleBackColor = false;
            btClose.Click += btClose_Click;
            // 
            // ResultForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.fairy_cave_bg;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(605, 342);
            Controls.Add(btClose);
            Controls.Add(lbKq);
            Controls.Add(lbScoreBot);
            Controls.Add(lbScorePlayer);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "ResultForm";
            Text = "ResultForm";
            Load += ResultForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbScorePlayer;
        private Label lbScoreBot;
        private Label lbKq;
        private Button btClose;
    }
}