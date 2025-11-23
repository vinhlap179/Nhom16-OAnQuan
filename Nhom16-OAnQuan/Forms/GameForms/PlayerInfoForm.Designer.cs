namespace Nhom16_OAnQuan.Forms.GameForms
{
    partial class PlayerInfoForm
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
            lbUsername = new Label();
            lbWin = new Label();
            lbLoss = new Label();
            lbTotal = new Label();
            btClose = new Button();
            SuspendLayout();
            // 
            // lbUsername
            // 
            lbUsername.AutoSize = true;
            lbUsername.BackColor = Color.Transparent;
            lbUsername.Font = new Font("Press Start 2P", 9F);
            lbUsername.ForeColor = SystemColors.ButtonHighlight;
            lbUsername.Location = new Point(46, 60);
            lbUsername.Name = "lbUsername";
            lbUsername.Size = new Size(205, 21);
            lbUsername.TabIndex = 0;
            lbUsername.Text = "Account Name:";
            // 
            // lbWin
            // 
            lbWin.AutoSize = true;
            lbWin.BackColor = Color.Transparent;
            lbWin.Font = new Font("Press Start 2P", 9F);
            lbWin.ForeColor = SystemColors.ButtonHighlight;
            lbWin.Location = new Point(46, 132);
            lbWin.Name = "lbWin";
            lbWin.Size = new Size(175, 21);
            lbWin.TabIndex = 1;
            lbWin.Text = "Win number:";
            // 
            // lbLoss
            // 
            lbLoss.AutoSize = true;
            lbLoss.BackColor = Color.Transparent;
            lbLoss.Font = new Font("Press Start 2P", 9F);
            lbLoss.ForeColor = SystemColors.ButtonHighlight;
            lbLoss.Location = new Point(46, 216);
            lbLoss.Name = "lbLoss";
            lbLoss.Size = new Size(190, 21);
            lbLoss.TabIndex = 2;
            lbLoss.Text = "Loss number:";
            // 
            // lbTotal
            // 
            lbTotal.AutoSize = true;
            lbTotal.BackColor = Color.Transparent;
            lbTotal.Font = new Font("Press Start 2P", 9F);
            lbTotal.ForeColor = SystemColors.ButtonHighlight;
            lbTotal.Location = new Point(46, 307);
            lbTotal.Name = "lbTotal";
            lbTotal.Size = new Size(205, 21);
            lbTotal.TabIndex = 3;
            lbTotal.Text = "Total match: ";
            // 
            // btClose
            // 
            btClose.BackColor = Color.White;
            btClose.Font = new Font("Press Start 2P", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btClose.Location = new Point(323, 368);
            btClose.Name = "btClose";
            btClose.Size = new Size(122, 46);
            btClose.TabIndex = 4;
            btClose.Text = "Closed";
            btClose.UseVisualStyleBackColor = false;
            btClose.Click += btClose_Click;
            // 
            // PlayerInfoForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            BackgroundImage = Properties.Resources.wasteland_bg;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(btClose);
            Controls.Add(lbTotal);
            Controls.Add(lbLoss);
            Controls.Add(lbWin);
            Controls.Add(lbUsername);
            Name = "PlayerInfoForm";
            Text = "PlayerInfoForm";
            Load += PlayerInfoForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbUsername;
        private Label lbWin;
        private Label lbLoss;
        private Label lbTotal;
        private Button btClose;
    }
}