namespace Nhom16_OAnQuan.Forms.GameForms
{
    partial class GameOnline
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
            btnPhai = new Button();
            btnTrai = new Button();
            lblThongBao = new Label();
            lblDiemNguoi2 = new Label();
            lblDiemNguoi1 = new Label();
            tblBanCo = new TableLayoutPanel();
            SuspendLayout();
            // 
            // btnPhai
            // 
            btnPhai.BackColor = SystemColors.WindowText;
            btnPhai.Font = new Font("Press Start 2P", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPhai.ForeColor = SystemColors.ButtonHighlight;
            btnPhai.Location = new Point(891, 737);
            btnPhai.Name = "btnPhai";
            btnPhai.Size = new Size(122, 48);
            btnPhai.TabIndex = 11;
            btnPhai.Text = "RIGHT ";
            btnPhai.UseVisualStyleBackColor = false;
            // 
            // btnTrai
            // 
            btnTrai.BackColor = SystemColors.WindowText;
            btnTrai.Font = new Font("Press Start 2P", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnTrai.ForeColor = SystemColors.ButtonHighlight;
            btnTrai.Location = new Point(466, 742);
            btnTrai.Name = "btnTrai";
            btnTrai.Size = new Size(116, 43);
            btnTrai.TabIndex = 10;
            btnTrai.Text = "LEFT ";
            btnTrai.UseVisualStyleBackColor = false;
            // 
            // lblThongBao
            // 
            lblThongBao.AutoSize = true;
            lblThongBao.BackColor = Color.Transparent;
            lblThongBao.Font = new Font("Press Start 2P", 16.2F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lblThongBao.ForeColor = SystemColors.ButtonHighlight;
            lblThongBao.Location = new Point(660, 742);
            lblThongBao.Name = "lblThongBao";
            lblThongBao.Size = new Size(278, 38);
            lblThongBao.TabIndex = 9;
            lblThongBao.Text = "Thông Báo";
            // 
            // lblDiemNguoi2
            // 
            lblDiemNguoi2.AutoSize = true;
            lblDiemNguoi2.BackColor = Color.Transparent;
            lblDiemNguoi2.Font = new Font("Press Start 2P", 13.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lblDiemNguoi2.ForeColor = SystemColors.ButtonHighlight;
            lblDiemNguoi2.Location = new Point(1118, 734);
            lblDiemNguoi2.Name = "lblDiemNguoi2";
            lblDiemNguoi2.Size = new Size(385, 32);
            lblDiemNguoi2.TabIndex = 8;
            lblDiemNguoi2.Text = "Điểm người chơi 2";
            // 
            // lblDiemNguoi1
            // 
            lblDiemNguoi1.AutoSize = true;
            lblDiemNguoi1.BackColor = Color.Transparent;
            lblDiemNguoi1.Font = new Font("Press Start 2P", 13.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lblDiemNguoi1.ForeColor = SystemColors.ButtonHighlight;
            lblDiemNguoi1.Location = new Point(126, 730);
            lblDiemNguoi1.Name = "lblDiemNguoi1";
            lblDiemNguoi1.Size = new Size(385, 32);
            lblDiemNguoi1.TabIndex = 7;
            lblDiemNguoi1.Text = "Điểm người chơi 1";
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
            tblBanCo.Location = new Point(69, 12);
            tblBanCo.Name = "tblBanCo";
            tblBanCo.RowCount = 3;
            tblBanCo.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tblBanCo.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tblBanCo.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tblBanCo.Size = new Size(1317, 692);
            tblBanCo.TabIndex = 6;
            // 
            // GameOnline
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.gameboard;
            ClientSize = new Size(1438, 811);
            Controls.Add(btnPhai);
            Controls.Add(btnTrai);
            Controls.Add(lblThongBao);
            Controls.Add(lblDiemNguoi2);
            Controls.Add(lblDiemNguoi1);
            Controls.Add(tblBanCo);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "GameOnline";
            Text = "GameOnline";
            Load += GameOnline_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnPhai;
        private Button btnTrai;
        private Label lblThongBao;
        private Label lblDiemNguoi2;
        private Label lblDiemNguoi1;
        private TableLayoutPanel tblBanCo;
    }
}