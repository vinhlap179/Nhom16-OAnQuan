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
            // 1. Cài đặt chung
            dgvBXH.BackgroundColor = Color.FromArgb(40, 40, 40); // Màu nền tối nhẹ hoặc Color.White tuỳ theme
            dgvBXH.BorderStyle = BorderStyle.None;
            dgvBXH.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal; // Chỉ kẻ ngang
            dgvBXH.GridColor = Color.DimGray; // Màu đường kẻ mờ
            dgvBXH.RowHeadersVisible = false; // Ẩn cột đầu dòng thừa thãi
            dgvBXH.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBXH.MultiSelect = false;
            dgvBXH.AllowUserToResizeRows = false;
            dgvBXH.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Tự giãn cột

            // 2. Chỉnh Header (Tiêu đề cột)
            dgvBXH.EnableHeadersVisualStyles = false; // Bắt buộc để chỉnh màu header
            dgvBXH.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvBXH.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 20, 20); // Màu đen/tối
            dgvBXH.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gold; // Chữ vàng sang trọng
            dgvBXH.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            dgvBXH.ColumnHeadersHeight = 40;

            // 3. Chỉnh Row (Dòng dữ liệu)
            dgvBXH.DefaultCellStyle.BackColor = Color.FromArgb(30, 30, 30); // Nền dòng tối
            dgvBXH.DefaultCellStyle.ForeColor = Color.White; // Chữ trắng
            dgvBXH.DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            dgvBXH.DefaultCellStyle.SelectionBackColor = Color.FromArgb(70, 70, 70); // Màu khi chọn
            dgvBXH.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvBXH.RowTemplate.Height = 50; // Dòng cao lên cho thoáng

            // Căn giữa nội dung
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
                // Setup giao diện đẹp trước
                DecorateDataGridView();

                // Tạo cột
                dgvBXH.Columns.Clear();

                // Cột 0: Huy chương (Dùng ImageColumn thay vì Text)
                DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
                imgCol.Name = "RankImg";
                imgCol.HeaderText = "Hạng";
                imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom; // Co giãn ảnh vừa ô
                dgvBXH.Columns.Add(imgCol);

                // Cột 1: Rank Text (Để hiển thị số 4, 5, 6... nếu không có huy chương)
                dgvBXH.Columns.Add("RankTxt", "Hạng");
                dgvBXH.Columns["RankTxt"].Visible = false; // Ẩn cột này đi, chỉ dùng để lưu số

                // Các cột còn lại
                dgvBXH.Columns.Add("Name", "Đại Cao Thủ");
                dgvBXH.Columns.Add("Wins", "Chiến Thắng");

                // Chỉnh độ rộng
                dgvBXH.Columns[0].Width = 60;  // Cột huy chương nhỏ
                dgvBXH.Columns[2].Width = 250; // Cột tên rộng

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

                        // Xử lý hình ảnh huy chương
                        Image rankImage = null;
                        if (rank == 1) rankImage = Image.FromFile(@"Resources\medal_1.png"); // Ảnh vàng
                        //else if (rank == 2) rankImage = Properties.Resources.medal_2; // Ảnh bạc
                        //else if (rank == 3) rankImage = Properties.Resources.medal_3; // Ảnh đồng

                        // Nếu không có ảnh trong Resource thì để null (code dưới sẽ vẽ số)

                        // Thêm dòng
                        // Lưu ý: Cột 0 là ảnh, Cột 1 là số rank (ẩn), Cột 2 Tên, Cột 3 Win
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
            // Kiểm tra nếu đang render các dòng dữ liệu (không phải header)
            if (e.RowIndex >= 0)
            {
                // Lấy giá trị Rank (Cột ẩn "RankTxt" là cột index 1)
                // Hoặc đơn giản lấy RowIndex + 1 vì bảng đã sort sẵn
                int rank = e.RowIndex + 1;

                // Nếu cột đầu tiên (Cột ảnh) không có ảnh (từ top 4 trở đi), hiển thị số rank
                if (dgvBXH.Columns[e.ColumnIndex].Name == "RankImg" && e.Value == null)
                {
                    // Vẽ số rank vào cột ảnh nếu không có huy chương (Xử lý nâng cao, hoặc dùng cột RankTxt hiển thị lại)
                    // Đơn giản nhất: Nếu bạn muốn hiện số cho Top 4-10, hãy bật lại cột RankTxt
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