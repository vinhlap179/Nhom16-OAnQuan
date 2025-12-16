using Nhom16_OAnQuan.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using Google.Cloud.Firestore;
using Nhom16_OAnQuan.Properties;

namespace Nhom16_OAnQuan.Forms.GameForms
{
    public partial class StartingGUI : Form
    {
        private SoundPlayer backgroundMusicPlayer;
        public StartingGUI()
        {
            InitializeComponent();
            backgroundMusicPlayer = new SoundPlayer();
            pictureBox1.BackColor = System.Drawing.Color.Transparent;
            CheckLoginToken();
        }


        private void CheckLoginToken()
        {
            if (string.IsNullOrEmpty(GlobalUserSession.CurrentToken))
            {
                // Nếu không có Token, phiên không hợp lệ
                MessageBox.Show("Phiên đăng nhập đã hết hạn hoặc không hợp lệ.", "Cần đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                LogInForm loginForm = new LogInForm();
                this.Hide();
                loginForm.ShowDialog();
                Application.Exit();
                return;
            }
            else
            {
                this.Text = $"OAnQuan - Đăng nhập với: {GlobalUserSession.CurrentUsername}";
            }
        }

        private void SignOutBtn_Click(object sender, EventArgs e)
        {
            GlobalUserSession.ClearSession();

            MessageBox.Show("Bạn đã đăng xuất thành công.", "Đăng xuất");
            this.Close();
            Application.Restart();
        }

        private void GuideBtn_Click(object sender, EventArgs e)
        {
            new GuideGUI().ShowDialog();
        }

        private void PlayerInformationBtn_Click(object sender, EventArgs e)
        {
            new PlayerInfoForm().ShowDialog();
        }

        private void PlayBtn_Click(object sender, EventArgs e)
        {
            new ChooseMode().ShowDialog();
        }

        private async void StartingGUI_Load(object sender, EventArgs e)
        {
            try
            {
                // Gán file nhạc từ Resources (đảm bảo file 'seabed' là file .wav)
                backgroundMusicPlayer.Stream = Properties.Resources.seabed;
                // Phát lặp lại
                backgroundMusicPlayer.PlayLooping();
            }
            catch (Exception ex)
            {
                // Phòng trường hợp lỗi file nhạc thì không bị crash game
                Console.WriteLine("Lỗi phát nhạc: " + ex.Message);
            }

            // Gọi hàm tải bảng xếp hạng ngay tại đây
            await LoadBangXepHang();
        }

        private void DecorateDataGridView()
        {
            // Cài đặt chung
            dgvBXH.BackgroundColor = Color.FromArgb(40, 40, 40);
            dgvBXH.BorderStyle = BorderStyle.None;
            dgvBXH.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvBXH.GridColor = Color.DimGray;
            dgvBXH.RowHeadersVisible = false; 
            dgvBXH.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBXH.MultiSelect = false;
            dgvBXH.AllowUserToResizeRows = false;
            dgvBXH.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; 

            // Chỉnh Header
            dgvBXH.EnableHeadersVisualStyles = false; 
            dgvBXH.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvBXH.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 20, 20); // Màu đen/tối
            dgvBXH.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gold;
            dgvBXH.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            dgvBXH.ColumnHeadersHeight = 40;

            //  Chỉnh Row
            dgvBXH.DefaultCellStyle.BackColor = Color.FromArgb(30, 30, 30); // Nền dòng tối
            dgvBXH.DefaultCellStyle.ForeColor = Color.White; // Chữ trắng
            dgvBXH.DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            dgvBXH.DefaultCellStyle.SelectionBackColor = Color.FromArgb(70, 70, 70); // Màu
            dgvBXH.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvBXH.RowTemplate.Height = 50; // Dòng cao lên
            // Căn giữa
            dgvBXH.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvBXH.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void StartingGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (backgroundMusicPlayer != null)
            {
                backgroundMusicPlayer.Stop();
                backgroundMusicPlayer.Dispose(); // Giải phóng tài nguyên
            }
        }


        // Hàm cấu hình và tải bảng xếp hạng
        private async Task LoadBangXepHang()
        {
            try
            {
                DecorateDataGridView();
                dgvBXH.Columns.Clear();
                DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
                imgCol.Name = "RankImg";
                imgCol.HeaderText = "Hạng";
                imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                dgvBXH.Columns.Add(imgCol);
                dgvBXH.Columns.Add("RankTxt", "Hạng");
                dgvBXH.Columns["RankTxt"].Visible = false;
                // Các cột còn lại
                dgvBXH.Columns.Add("Name", "Người Chơi");
                dgvBXH.Columns.Add("Wins", "Chiến Thắng");

                // Chỉnh độ rộng
                dgvBXH.Columns[0].Width = 60;
                dgvBXH.Columns[2].Width = 250;

                // Lấy dữ liệu Firestore
                var db = FirestoreHelper.Database;
                Query query = db.Collection("UserData").OrderByDescending("Wins").Limit(10);
                QuerySnapshot snap = await query.GetSnapshotAsync();

                dgvBXH.Rows.Clear();
                int rank = 1;

                foreach (DocumentSnapshot doc in snap.Documents)
                {
                    if (doc.Exists)
                    {
                        UserData user = doc.ConvertTo<UserData>();

                        // -------------------Xử lý hình ảnh huy chương---------------------------------------------------
                        // ===================== ĐANG LỖI CẦN FIX CHÈN ẢNH ==========================

                        Image rankImage = null;
                        //if (rank == 1) rankImage = Properties.Resources.seabed; // Ảnh vàng
                        //else if (rank == 2) rankImage = Properties.Resources.medal_2; // Ảnh bạc
                        //else if (rank == 3) rankImage = Properties.Resources.medal_3; // Ảnh đồng

                        // Nếu không có ảnh trong Resource thì để null (code dưới sẽ vẽ số)
                        dgvBXH.Rows.Add(rankImage, rank, user.Username, user.Wins);

                        rank++;
                    }
                }

                dgvBXH.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi BXH: " + ex.Message);
            }
        }
        private void dgvBXH_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int rank = e.RowIndex + 1;
                // Nếu cột đầu tiên (Cột ảnh) không có ảnh (từ top 4 trở đi), hiển thị số rank
                if (dgvBXH.Columns[e.ColumnIndex].Name == "RankImg" && e.Value == null)
                {
                }
                // Tô màu chữ đặc biệt cho Top 3
                if (rank == 1)
                {
                    e.CellStyle.ForeColor = Color.Gold;
                    e.CellStyle.SelectionForeColor = Color.Gold;
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                }
                else if (rank == 2)
                {
                    e.CellStyle.ForeColor = Color.Silver;
                    e.CellStyle.SelectionForeColor = Color.Silver;
                }
                else if (rank == 3)
                {
                    e.CellStyle.ForeColor = Color.FromArgb(205, 127, 50); // Màu đồng
                    e.CellStyle.SelectionForeColor = Color.FromArgb(205, 127, 50);
                }
            }
        }
        private void dgvBXH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}