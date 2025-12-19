namespace Nhom16_OAnQuan.Forms.GameForms
{
    partial class GameBoardGUI
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
            tblBanCo = new TableLayoutPanel();
            lblDiemNguoi1 = new Label();
            lblDiemNguoi2 = new Label();
            lblThongBao = new Label();
            btnTrai = new Button();
            btnPhai = new Button();
            SuspendLayout();
            // 
            // tblBanCo
            // 
            tblBanCo.ColumnCount = 7;
            tblBanCo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857141F));
            tblBanCo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857141F));
            tblBanCo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857141F));
            tblBanCo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857141F));
            tblBanCo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857141F));
            tblBanCo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857141F));
            tblBanCo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.2857141F));
            tblBanCo.Location = new Point(82, 89);
            tblBanCo.Name = "tblBanCo";
            tblBanCo.RowCount = 3;
            tblBanCo.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tblBanCo.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tblBanCo.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tblBanCo.Size = new Size(1155, 641);
            tblBanCo.TabIndex = 0;
            // 
            // lblDiemNguoi1
            // 
            lblDiemNguoi1.AutoSize = true;
            lblDiemNguoi1.BackColor = Color.Transparent;
            lblDiemNguoi1.FlatStyle = FlatStyle.Flat;
            lblDiemNguoi1.Font = new Font("Press Start 2P", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDiemNguoi1.ForeColor = SystemColors.ButtonHighlight;
            lblDiemNguoi1.Location = new Point(82, 764);
            lblDiemNguoi1.Name = "lblDiemNguoi1";
            lblDiemNguoi1.Size = new Size(336, 32);
            lblDiemNguoi1.TabIndex = 1;
            lblDiemNguoi1.Text = "PLAYER'S SCORE";
            // 
            // lblDiemNguoi2
            // 
            lblDiemNguoi2.AutoSize = true;
            lblDiemNguoi2.BackColor = Color.Transparent;
            lblDiemNguoi2.Font = new Font("Press Start 2P", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDiemNguoi2.ForeColor = SystemColors.ButtonHighlight;
            lblDiemNguoi2.Location = new Point(1074, 768);
            lblDiemNguoi2.Name = "lblDiemNguoi2";
            lblDiemNguoi2.Size = new Size(267, 32);
            lblDiemNguoi2.TabIndex = 2;
            lblDiemNguoi2.Text = "BOT'S SCORE";
            // 
            // lblThongBao
            // 
            lblThongBao.AutoSize = true;
            lblThongBao.BackColor = Color.Transparent;
            lblThongBao.Font = new Font("Press Start 2P", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lblThongBao.ForeColor = SystemColors.ButtonHighlight;
            lblThongBao.Location = new Point(616, 776);
            lblThongBao.Name = "lblThongBao";
            lblThongBao.Size = new Size(201, 27);
            lblThongBao.TabIndex = 3;
            lblThongBao.Text = "Thông Báo";
            // 
            // btnTrai
            // 
            btnTrai.BackColor = SystemColors.WindowText;
            btnTrai.Font = new Font("Press Start 2P", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnTrai.ForeColor = SystemColors.ButtonHighlight;
            btnTrai.Location = new Point(422, 776);
            btnTrai.Name = "btnTrai";
            btnTrai.Size = new Size(116, 43);
            btnTrai.TabIndex = 4;
            btnTrai.Text = "LEFT";
            btnTrai.UseVisualStyleBackColor = false;
            // 
            // btnPhai
            // 
            btnPhai.BackColor = SystemColors.WindowText;
            btnPhai.Font = new Font("Press Start 2P", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPhai.ForeColor = SystemColors.ButtonHighlight;
            btnPhai.Location = new Point(847, 771);
            btnPhai.Name = "btnPhai";
            btnPhai.Size = new Size(122, 48);
            btnPhai.TabIndex = 5;
            btnPhai.Text = "RIGHT";
            btnPhai.UseVisualStyleBackColor = false;
            // 
            // GameBoardGUI
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.gameboard;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1374, 850);
            Controls.Add(btnPhai);
            Controls.Add(btnTrai);
            Controls.Add(lblThongBao);
            Controls.Add(lblDiemNguoi2);
            Controls.Add(lblDiemNguoi1);
            Controls.Add(tblBanCo);
            Name = "GameBoardGUI";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GameBoardGUI";
            Load += GameBoardGUI_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tblBanCo;
        private Label lblDiemNguoi1;
        private Label lblDiemNguoi2;
        private Label lblThongBao;
        private Button btnTrai;
        private Button btnPhai;
    }
}