namespace Nhom16_OAnQuan.Forms.GameForms
{
    partial class LobbyForm
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
            BtnCreateRoom = new Button();
            BtnFindRoom = new Button();
            BtnJoinRoom = new Button();
            txtRoomId = new TextBox();
            SuspendLayout();
            // 
            // BtnCreateRoom
            // 
            BtnCreateRoom.Font = new Font("Segoe UI", 14F);
            BtnCreateRoom.Location = new Point(183, 38);
            BtnCreateRoom.Name = "BtnCreateRoom";
            BtnCreateRoom.Size = new Size(228, 67);
            BtnCreateRoom.TabIndex = 0;
            BtnCreateRoom.Text = "Tạo Phòng";
            BtnCreateRoom.UseVisualStyleBackColor = true;
            BtnCreateRoom.Click += BtnCreateRoom_Click;
            // 
            // BtnFindRoom
            // 
            BtnFindRoom.Font = new Font("Segoe UI", 14F);
            BtnFindRoom.Location = new Point(183, 153);
            BtnFindRoom.Name = "BtnFindRoom";
            BtnFindRoom.Size = new Size(228, 67);
            BtnFindRoom.TabIndex = 1;
            BtnFindRoom.Text = "Tìm Phòng";
            BtnFindRoom.UseVisualStyleBackColor = true;
            BtnFindRoom.Click += BtnFindRoom_Click;
            // 
            // BtnJoinRoom
            // 
            BtnJoinRoom.Font = new Font("Segoe UI", 14F);
            BtnJoinRoom.Location = new Point(183, 261);
            BtnJoinRoom.Name = "BtnJoinRoom";
            BtnJoinRoom.Size = new Size(228, 67);
            BtnJoinRoom.TabIndex = 2;
            BtnJoinRoom.Text = "Tham Gia Phòng";
            BtnJoinRoom.UseVisualStyleBackColor = true;
            BtnJoinRoom.Click += BtnJoinRoom_Click;
            // 
            // txtRoomId
            // 
            txtRoomId.Location = new Point(12, 48);
            txtRoomId.Name = "txtRoomId";
            txtRoomId.Size = new Size(125, 27);
            txtRoomId.TabIndex = 3;
            txtRoomId.Text = "Nhập Room ID";
            // 
            // LobbyForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(616, 389);
            Controls.Add(txtRoomId);
            Controls.Add(BtnJoinRoom);
            Controls.Add(BtnFindRoom);
            Controls.Add(BtnCreateRoom);
            Name = "LobbyForm";
            Text = "LobbyForm";
            Load += LobbyForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnCreateRoom;
        private Button BtnFindRoom;
        private Button BtnJoinRoom;
        private TextBox txtRoomId;
    }
}