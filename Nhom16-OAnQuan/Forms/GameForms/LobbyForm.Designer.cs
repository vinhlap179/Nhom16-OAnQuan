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
            BtnCreateRoom.BackColor = Color.DarkGray;
            BtnCreateRoom.FlatAppearance.BorderSize = 0;
            BtnCreateRoom.FlatStyle = FlatStyle.Flat;
            BtnCreateRoom.Font = new Font("Press Start 2P", 7.8F);
            BtnCreateRoom.ForeColor = SystemColors.ButtonHighlight;
            BtnCreateRoom.Location = new Point(193, 70);
            BtnCreateRoom.Name = "BtnCreateRoom";
            BtnCreateRoom.Size = new Size(228, 67);
            BtnCreateRoom.TabIndex = 0;
            BtnCreateRoom.Text = "Create Room";
            BtnCreateRoom.UseVisualStyleBackColor = false;
            BtnCreateRoom.Click += BtnCreateRoom_Click;
            // 
            // BtnFindRoom
            // 
            BtnFindRoom.BackColor = Color.DarkGray;
            BtnFindRoom.FlatAppearance.BorderSize = 0;
            BtnFindRoom.FlatStyle = FlatStyle.Flat;
            BtnFindRoom.Font = new Font("Press Start 2P", 7.8F);
            BtnFindRoom.ForeColor = SystemColors.ButtonHighlight;
            BtnFindRoom.Location = new Point(193, 165);
            BtnFindRoom.Name = "BtnFindRoom";
            BtnFindRoom.Size = new Size(228, 67);
            BtnFindRoom.TabIndex = 1;
            BtnFindRoom.Text = "Find Room";
            BtnFindRoom.UseVisualStyleBackColor = false;
            BtnFindRoom.Click += BtnFindRoom_Click;
            // 
            // BtnJoinRoom
            // 
            BtnJoinRoom.BackColor = Color.DarkGray;
            BtnJoinRoom.FlatAppearance.BorderSize = 0;
            BtnJoinRoom.FlatStyle = FlatStyle.Flat;
            BtnJoinRoom.Font = new Font("Press Start 2P", 7.8F);
            BtnJoinRoom.ForeColor = SystemColors.ButtonHighlight;
            BtnJoinRoom.Location = new Point(193, 263);
            BtnJoinRoom.Name = "BtnJoinRoom";
            BtnJoinRoom.Size = new Size(228, 67);
            BtnJoinRoom.TabIndex = 2;
            BtnJoinRoom.Text = "Join Room";
            BtnJoinRoom.UseVisualStyleBackColor = false;
            BtnJoinRoom.Click += BtnJoinRoom_Click;
            // 
            // txtRoomId
            // 
            txtRoomId.Font = new Font("Press Start 2P", 7.8F);
            txtRoomId.Location = new Point(12, 24);
            txtRoomId.Name = "txtRoomId";
            txtRoomId.Size = new Size(175, 20);
            txtRoomId.TabIndex = 3;
            txtRoomId.Text = "Enter Room ID";
            txtRoomId.TextChanged += txtRoomId_TextChanged;
            // 
            // LobbyForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
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