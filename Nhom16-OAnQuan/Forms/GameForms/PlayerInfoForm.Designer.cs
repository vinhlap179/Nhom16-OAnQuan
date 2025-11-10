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
            lbUsername.Font = new Font("Segoe UI", 14F);
            lbUsername.Location = new Point(60, 38);
            lbUsername.Name = "lbUsername";
            lbUsername.Size = new Size(163, 32);
            lbUsername.TabIndex = 0;
            lbUsername.Text = "Tên tài khoản:";
            // 
            // lbWin
            // 
            lbWin.AutoSize = true;
            lbWin.Font = new Font("Segoe UI", 14F);
            lbWin.Location = new Point(60, 110);
            lbWin.Name = "lbWin";
            lbWin.Size = new Size(164, 32);
            lbWin.TabIndex = 1;
            lbWin.Text = "Số trận thắng:";
            // 
            // lbLoss
            // 
            lbLoss.AutoSize = true;
            lbLoss.Font = new Font("Segoe UI", 14F);
            lbLoss.Location = new Point(60, 194);
            lbLoss.Name = "lbLoss";
            lbLoss.Size = new Size(150, 32);
            lbLoss.TabIndex = 2;
            lbLoss.Text = "Số trận thua:";
            // 
            // lbTotal
            // 
            lbTotal.AutoSize = true;
            lbTotal.Font = new Font("Segoe UI", 14F);
            lbTotal.Location = new Point(60, 285);
            lbTotal.Name = "lbTotal";
            lbTotal.Size = new Size(154, 32);
            lbTotal.TabIndex = 3;
            lbTotal.Text = "Tổng số trận:";
            // 
            // btClose
            // 
            btClose.Font = new Font("Segoe UI", 14F);
            btClose.Location = new Point(323, 368);
            btClose.Name = "btClose";
            btClose.Size = new Size(122, 46);
            btClose.TabIndex = 4;
            btClose.Text = "Đóng";
            btClose.UseVisualStyleBackColor = true;
            btClose.Click += btClose_Click;
            // 
            // PlayerInfoForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
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