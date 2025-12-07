namespace Nhom16_OAnQuan.Forms.GameForms
{
    partial class StartingGUI
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartingGUI));
            pictureBox1 = new PictureBox();
            PlayerInformationBtn = new Button();
            GuideBtn = new Button();
            PlayBtn = new Button();
            SignOutBtn = new Button();
            panel1 = new Panel();
            timer1 = new System.Windows.Forms.Timer(components);
            formLoadTimer = new System.Windows.Forms.Timer(components);
            dgvBXH = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBXH).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(97, 30);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(870, 216);
            pictureBox1.TabIndex = 23;
            pictureBox1.TabStop = false;
            // 
            // PlayerInformationBtn
            // 
            PlayerInformationBtn.BackColor = Color.Gray;
            PlayerInformationBtn.Font = new Font("Microsoft Sans Serif", 9F);
            PlayerInformationBtn.ForeColor = SystemColors.ButtonHighlight;
            PlayerInformationBtn.Location = new Point(32, 188);
            PlayerInformationBtn.Name = "PlayerInformationBtn";
            PlayerInformationBtn.Size = new Size(204, 60);
            PlayerInformationBtn.TabIndex = 21;
            PlayerInformationBtn.Text = "PLAYER INFORMATION";
            PlayerInformationBtn.UseVisualStyleBackColor = false;
            PlayerInformationBtn.Click += PlayerInformationBtn_Click;
            // 
            // GuideBtn
            // 
            GuideBtn.BackColor = Color.Gray;
            GuideBtn.Font = new Font("Microsoft Sans Serif", 9F);
            GuideBtn.ForeColor = SystemColors.ButtonHighlight;
            GuideBtn.Location = new Point(32, 106);
            GuideBtn.Name = "GuideBtn";
            GuideBtn.Size = new Size(204, 61);
            GuideBtn.TabIndex = 20;
            GuideBtn.Text = "GUIDE";
            GuideBtn.UseVisualStyleBackColor = false;
            GuideBtn.Click += GuideBtn_Click;
            // 
            // PlayBtn
            // 
            PlayBtn.BackColor = Color.Gray;
            PlayBtn.Font = new Font("Microsoft Sans Serif", 9F);
            PlayBtn.ForeColor = SystemColors.ButtonHighlight;
            PlayBtn.Location = new Point(32, 24);
            PlayBtn.Name = "PlayBtn";
            PlayBtn.Size = new Size(204, 56);
            PlayBtn.TabIndex = 22;
            PlayBtn.Text = "PLAY";
            PlayBtn.UseVisualStyleBackColor = false;
            PlayBtn.Click += PlayBtn_Click;
            // 
            // SignOutBtn
            // 
            SignOutBtn.BackColor = Color.Gray;
            SignOutBtn.Font = new Font("Microsoft Sans Serif", 9F);
            SignOutBtn.ForeColor = SystemColors.ButtonHighlight;
            SignOutBtn.Location = new Point(32, 265);
            SignOutBtn.Name = "SignOutBtn";
            SignOutBtn.Size = new Size(204, 58);
            SignOutBtn.TabIndex = 19;
            SignOutBtn.Text = "LOGOUT ";
            SignOutBtn.UseVisualStyleBackColor = false;
            SignOutBtn.Click += SignOutBtn_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.Controls.Add(PlayerInformationBtn);
            panel1.Controls.Add(SignOutBtn);
            panel1.Controls.Add(PlayBtn);
            panel1.Controls.Add(GuideBtn);
            panel1.Location = new Point(835, 277);
            panel1.Name = "panel1";
            panel1.Size = new Size(258, 345);
            panel1.TabIndex = 24;
            // 
            // formLoadTimer
            // 
            formLoadTimer.Enabled = true;
            formLoadTimer.Interval = 10;
            // 
            // dgvBXH
            // 
            dgvBXH.AllowUserToAddRows = false;
            dgvBXH.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBXH.Location = new Point(37, 277);
            dgvBXH.Name = "dgvBXH";
            dgvBXH.ReadOnly = true;
            dgvBXH.RowHeadersVisible = false;
            dgvBXH.RowHeadersWidth = 51;
            dgvBXH.Size = new Size(569, 310);
            dgvBXH.TabIndex = 25;
            // 
            // StartingGUI
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(1105, 625);
            Controls.Add(dgvBXH);
            Controls.Add(pictureBox1);
            Controls.Add(panel1);
            Name = "StartingGUI";
            StartPosition = FormStartPosition.Manual;
            Text = "StartingGUI";
            Load += StartingGUI_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvBXH).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private PictureBox pictureBox1;
        private Button PlayerInformationBtn;
        private Button GuideBtn;
        private Button PlayBtn;
        private Button SignOutBtn;
        private Panel panel1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer formLoadTimer;
        private DataGridView dgvBXH;
    }
}