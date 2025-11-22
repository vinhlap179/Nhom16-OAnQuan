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
            tblBanCo.Location = new Point(15, 12);
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
            lblDiemNguoi1.Location = new Point(70, 694);
            lblDiemNguoi1.Name = "lblDiemNguoi1";
            lblDiemNguoi1.Size = new Size(120, 20);
            lblDiemNguoi1.TabIndex = 1;
            lblDiemNguoi1.Text = "Điểm người chơi";
            // 
            // lblDiemNguoi2
            // 
            lblDiemNguoi2.AutoSize = true;
            lblDiemNguoi2.Location = new Point(1004, 694);
            lblDiemNguoi2.Name = "lblDiemNguoi2";
            lblDiemNguoi2.Size = new Size(77, 20);
            lblDiemNguoi2.TabIndex = 2;
            lblDiemNguoi2.Text = "Điểm máy";
            // 
            // lblThongBao
            // 
            lblThongBao.AutoSize = true;
            lblThongBao.Location = new Point(527, 694);
            lblThongBao.Name = "lblThongBao";
            lblThongBao.Size = new Size(81, 20);
            lblThongBao.TabIndex = 3;
            lblThongBao.Text = "Thông Báo";
            // 
            // btnTrai
            // 
            btnTrai.Location = new Point(370, 681);
            btnTrai.Name = "btnTrai";
            btnTrai.Size = new Size(116, 43);
            btnTrai.TabIndex = 4;
            btnTrai.Text = "Trái";
            btnTrai.UseVisualStyleBackColor = true;
            // 
            // btnPhai
            // 
            btnPhai.Location = new Point(665, 679);
            btnPhai.Name = "btnPhai";
            btnPhai.Size = new Size(122, 48);
            btnPhai.TabIndex = 5;
            btnPhai.Text = "Phải";
            btnPhai.UseVisualStyleBackColor = true;
            // 
            // GameBoardGUI
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1182, 753);
            Controls.Add(btnPhai);
            Controls.Add(btnTrai);
            Controls.Add(lblThongBao);
            Controls.Add(lblDiemNguoi2);
            Controls.Add(lblDiemNguoi1);
            Controls.Add(tblBanCo);
            Name = "GameBoardGUI";
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