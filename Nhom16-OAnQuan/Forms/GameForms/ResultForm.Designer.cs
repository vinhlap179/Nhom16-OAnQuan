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
            lbScorePlayer.Font = new Font("Segoe UI", 14F);
            lbScorePlayer.Location = new Point(84, 56);
            lbScorePlayer.Name = "lbScorePlayer";
            lbScorePlayer.Size = new Size(192, 32);
            lbScorePlayer.TabIndex = 0;
            lbScorePlayer.Text = "Điểm người chơi";
            // 
            // lbScoreBot
            // 
            lbScoreBot.AutoSize = true;
            lbScoreBot.Font = new Font("Segoe UI", 14F);
            lbScoreBot.Location = new Point(84, 110);
            lbScoreBot.Name = "lbScoreBot";
            lbScoreBot.Size = new Size(123, 32);
            lbScoreBot.TabIndex = 1;
            lbScoreBot.Text = "Điểm máy";
            // 
            // lbKq
            // 
            lbKq.AutoSize = true;
            lbKq.Font = new Font("Segoe UI", 14F);
            lbKq.Location = new Point(84, 169);
            lbKq.Name = "lbKq";
            lbKq.Size = new Size(96, 32);
            lbKq.TabIndex = 2;
            lbKq.Text = "Kết quả";
            // 
            // btClose
            // 
            btClose.Font = new Font("Segoe UI", 14F);
            btClose.Location = new Point(84, 231);
            btClose.Name = "btClose";
            btClose.Size = new Size(141, 56);
            btClose.TabIndex = 3;
            btClose.Text = "Quay lại";
            btClose.UseVisualStyleBackColor = true;
            btClose.Click += btClose_Click;
            // 
            // ResultForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(605, 342);
            Controls.Add(btClose);
            Controls.Add(lbKq);
            Controls.Add(lbScoreBot);
            Controls.Add(lbScorePlayer);
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