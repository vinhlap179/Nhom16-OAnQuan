namespace Nhom16_OAnQuan.Forms.GameForms
{
    partial class WaitingRoom
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
            lblRoomId = new Label();
            lblHost = new Label();
            lblGuest = new Label();
            btnBack = new Button();
            SuspendLayout();
            // 
            // lblRoomId
            // 
            lblRoomId.AutoSize = true;
            lblRoomId.Font = new Font("Press Start 2P", 13.8F);
            lblRoomId.Location = new Point(35, 43);
            lblRoomId.Name = "lblRoomId";
            lblRoomId.Size = new Size(244, 32);
            lblRoomId.TabIndex = 0;
            lblRoomId.Text = "Room ID : ";
            // 
            // lblHost
            // 
            lblHost.AutoSize = true;
            lblHost.Font = new Font("Press Start 2P", 13.8F);
            lblHost.Location = new Point(35, 102);
            lblHost.Name = "lblHost";
            lblHost.Size = new Size(175, 32);
            lblHost.TabIndex = 1;
            lblHost.Text = "Host : ";
            // 
            // lblGuest
            // 
            lblGuest.AutoSize = true;
            lblGuest.Font = new Font("Press Start 2P", 13.8F);
            lblGuest.Location = new Point(35, 168);
            lblGuest.Name = "lblGuest";
            lblGuest.Size = new Size(198, 32);
            lblGuest.TabIndex = 2;
            lblGuest.Text = "Guest : ";
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.White;
            btnBack.Font = new Font("Press Start 2P", 13.8F);
            btnBack.Location = new Point(268, 250);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(174, 58);
            btnBack.TabIndex = 3;
            btnBack.Text = "Return";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // WaitingRoom
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Info;
            ClientSize = new Size(734, 364);
            Controls.Add(btnBack);
            Controls.Add(lblGuest);
            Controls.Add(lblHost);
            Controls.Add(lblRoomId);
            Name = "WaitingRoom";
            Text = "WaitingRoom";
            Load += WaitingRoom_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblRoomId;
        private Label lblHost;
        private Label lblGuest;
        private Button btnBack;
    }
}