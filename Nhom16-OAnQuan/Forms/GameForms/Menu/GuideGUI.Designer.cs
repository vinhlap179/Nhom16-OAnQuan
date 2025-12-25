namespace Nhom16_OAnQuan.Forms.GameForms
{
    partial class GuideGUI
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
            button1 = new Button();
            picGuide = new PictureBox();
            btnBack = new Button();
            btnNext = new Button();
            ((System.ComponentModel.ISupportInitialize)picGuide).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = SystemColors.AppWorkspace;
            button1.Font = new Font("Press Start 2P", 9F);
            button1.ForeColor = SystemColors.ButtonHighlight;
            button1.Location = new Point(22, 689);
            button1.Name = "button1";
            button1.Size = new Size(152, 52);
            button1.TabIndex = 0;
            button1.Text = "Return";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // picGuide
            // 
            picGuide.Location = new Point(12, 12);
            picGuide.Name = "picGuide";
            picGuide.Size = new Size(1158, 671);
            picGuide.TabIndex = 1;
            picGuide.TabStop = false;
            picGuide.Click += picGuide_Click;
            // 
            // btnBack
            // 
            btnBack.BackColor = SystemColors.AppWorkspace;
            btnBack.Font = new Font("Press Start 2P", 9F);
            btnBack.ForeColor = SystemColors.ButtonHighlight;
            btnBack.Location = new Point(899, 689);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(117, 52);
            btnBack.TabIndex = 2;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // btnNext
            // 
            btnNext.BackColor = SystemColors.AppWorkspace;
            btnNext.Font = new Font("Press Start 2P", 9F);
            btnNext.ForeColor = SystemColors.ButtonHighlight;
            btnNext.Location = new Point(1053, 689);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(117, 52);
            btnNext.TabIndex = 3;
            btnNext.Text = "Next";
            btnNext.UseVisualStyleBackColor = false;
            btnNext.Click += btnNext_Click;
            // 
            // GuideGUI
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.beach_bg;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1182, 753);
            Controls.Add(btnNext);
            Controls.Add(btnBack);
            Controls.Add(picGuide);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "GuideGUI";
            Text = "GuideGUI";
            Load += GuideGUI_Load;
            ((System.ComponentModel.ISupportInitialize)picGuide).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private PictureBox picGuide;
        private Button btnBack;
        private Button btnNext;
    }
}