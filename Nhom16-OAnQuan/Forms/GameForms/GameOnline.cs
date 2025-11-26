using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Cloud.Firestore;
using Nhom16_OAnQuan.Classes; // Chứa RoomModel và logic game (nếu có)

namespace Nhom16_OAnQuan.Forms.GameForms
{
    public partial class GameOnline : Form
    {
        private string roomId;
        private string currentUser;
        private bool isHost;

        private FirestoreChangeListener _listener;
        private RoomModel currentRoomData;

        // Giả sử bàn cờ có 12 ô (0-4: Dân P1, 5: Quan, 6-10: Dân P2, 11: Quan)
        private List<Button> squareButtons;
        public GameOnline(string roomId, string currentUser, bool isHost)
        {
            InitializeComponent();
            this.roomId = roomId;
            this.currentUser = currentUser;
            this.isHost = isHost;

            // Map các button vào list để dễ xử lý vòng lặp
            // Bạn cần đảm bảo đã tạo các nút btnSquare0 -> btnSquare11 trên Designer
            InitializeBoardMapping();
        }

        private void InitializeBoardMapping()
        {
            squareButtons = new List<Button>
            {
                btnSquare0, btnSquare1, btnSquare2, btnSquare3, btnSquare4, btnSquare5,
                btnSquare6, btnSquare7, btnSquare8, btnSquare9, btnSquare10, btnSquare11
            };

            // Gán sự kiện Click cho các ô DÂN (Quan không được click)
            for (int i = 0; i < 12; i++)
            {
                int index = i; // capture variable
                if (i != 5 && i != 11)
                {
                    squareButtons[i].Click += (s, e) => Square_Click(index);
                }
            }
        }

        private void GameOnline_Load(object sender, EventArgs e)
        {
            ListenToGame();
        }

        // ==========================================
        // 1. LẮNG NGHE DỮ LIỆU TỪ FIRESTORE (REALTIME)
        // ==========================================
        private void ListenToGame()
        {
            DocumentReference doc = FirestoreService.DB.Collection("rooms").Document(roomId);
            _listener = doc.Listen(snapshot =>
            {
                if (!snapshot.Exists) { MessageBox.Show("Phòng đã bị hủy!"); Close(); return; }

                currentRoomData = snapshot.ConvertTo<RoomModel>();

                // Cập nhật giao diện (Bắt buộc dùng Invoke nếu update UI từ thread khác)
                if (InvokeRequired) Invoke(new Action(() => RenderBoard()));
                else RenderBoard();
            });
        }

        // ==========================================
        // 2. CẬP NHẬT GIAO DIỆN
        // ==========================================
        private void RenderBoard()
        {
            // 1. Hiển thị số quân trên từng ô
            for (int i = 0; i < 12; i++)
            {
                squareButtons[i].Text = currentRoomData.BoardState[i].ToString();
            }

            // 2. Hiển thị lượt đi
            bool isMyTurn = currentRoomData.Turn == currentUser;
            lblTurn.Text = isMyTurn ? "Lượt của BẠN" : $"Lượt của {currentRoomData.Turn}";
            lblTurn.ForeColor = isMyTurn ? Color.Green : Color.Red;

            // 3. Hiển thị điểm số (Nếu bạn có lưu điểm trong RoomModel)
            // lblScoreHost.Text = currentRoomData.ScoreHost.ToString();

            // 4. Kiểm tra Game Over
            CheckGameOver();
        }

        // ==========================================
        // 3. XỬ LÝ KHI NGƯỜI CHƠI CLICK
        // ==========================================
        private async void Square_Click(int index)
        {
            // --- VALIDATION (Kiểm tra hợp lệ) ---

            // 1. Kiểm tra có phải lượt mình không
            if (currentRoomData.Turn != currentUser)
            {
                MessageBox.Show("Chưa đến lượt của bạn!");
                return;
            }

            // 2. Kiểm tra ô có thuộc quyền sở hữu không
            // Host (P1): 0-4, Guest (P2): 6-10
            bool isMySquare = isHost ? (index >= 0 && index <= 4) : (index >= 6 && index <= 10);
            if (!isMySquare)
            {
                MessageBox.Show("Bạn chỉ được chọn ô của mình!");
                return;
            }

            // 3. Kiểm tra ô có quân không
            if (currentRoomData.BoardState[index] == 0)
            {
                MessageBox.Show("Ô này hết quân rồi!");
                return;
            }

            // --- CHỌN HƯỚNG ĐI ---
            // Ở đây làm đơn giản bằng MessageBox, bạn có thể làm Form nhỏ đẹp hơn
            DialogResult result = MessageBox.Show("Chọn hướng rải quân:\nYes = Tay Phải (Chiều kim đồng hồ)\nNo = Tay Trái (Ngược chiều)",
                                                  "Chọn hướng", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Cancel) return;

            bool moveRight = (result == DialogResult.Yes);

            // --- TÍNH TOÁN LOGIC GAME (LOCAL) ---
            // Gọi hàm xử lý logic rải quân (Copy từ GameGUI sang hoặc viết mới)
            ProcessMove(index, moveRight);

            // --- ĐẨY DỮ LIỆU MỚI LÊN FIRESTORE ---
            await UpdateGameState();
        }

        // ==========================================
        // 4. LOGIC RẢI QUÂN (MÔ PHỎNG)
        // ==========================================
        private void ProcessMove(int startIndex, bool moveRight)
        {
            // Lấy trạng thái bàn cờ hiện tại để tính toán
            int[] board = currentRoomData.BoardState;
            int stones = board[startIndex];
            board[startIndex] = 0; // Bốc hết quân

            int currentPos = startIndex;

            // Vòng lặp rải quân cơ bản (Bạn cần thay thế bằng Logic Ăn Quân đầy đủ của bạn)
            while (stones > 0)
            {
                // Di chuyển vị trí
                if (moveRight) currentPos++; else currentPos--;

                // Xử lý vòng tròn mảng (0-11)
                if (currentPos > 11) currentPos = 0;
                if (currentPos < 0) currentPos = 11;

                board[currentPos]++;
                stones--;
            }

            // TODO: Tại đây bạn cần chèn Logic "Ăn Quân" (Nếu ô kế tiếp trống...)
            // Logic này khá dài, bạn hãy copy hàm xử lý từ GameGUI qua đây.
            // Ví dụ: CheckCapture(board, currentPos, moveRight);

            // Sau khi tính xong, cập nhật lượt đi
            SwapTurn();
        }

        private void SwapTurn()
        {
            // Đổi người chơi
            if (currentRoomData.Turn == currentRoomData.HostUID)
                currentRoomData.Turn = currentRoomData.GuestUID;
            else
                currentRoomData.Turn = currentRoomData.HostUID;
        }

        // ==========================================
        // 5. CẬP NHẬT FIRESTORE
        // ==========================================
        private async Task UpdateGameState()
        {
            DocumentReference doc = FirestoreService.DB.Collection("rooms").Document(roomId);

            Dictionary<string, object> updates = new Dictionary<string, object>
            {
                { "BoardState", currentRoomData.BoardState }, // Mảng int[] mới sau khi tính
                { "Turn", currentRoomData.Turn }
                // { "ScoreHost", currentRoomData.ScoreHost }, // Nếu có tính điểm
                // { "ScoreGuest", currentRoomData.ScoreGuest }
            };

            await doc.UpdateAsync(updates);
        }

        private void CheckGameOver()
        {
            // Ví dụ: Nếu cả 2 ô quan (5 và 11) đều bằng 0
            if (currentRoomData.BoardState[5] == 0 && currentRoomData.BoardState[11] == 0)
            {
                _listener.StopAsync();
                MessageBox.Show("Trò chơi kết thúc!");
                // Tính điểm tổng kết ở đây...
            }
        }

        private void GameOnline_FormClosing(object sender, FormClosingEventArgs e)
        {
            _listener?.StopAsync();
        }
    }
}
