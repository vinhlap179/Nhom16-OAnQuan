using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Cloud.Firestore;
using Nhom16_OAnQuan.Classes;


namespace Nhom16_OAnQuan.Forms.GameForms
{
    public partial class WaitingRoom : Form
    {
        private string roomId;
        private string currentUser;
        private bool isHost;
        private FirestoreChangeListener _listener; // Dùng để lắng nghe thay đổi realtime




        public WaitingRoom(string roomId, string currentUser, bool isHost)
        {
            InitializeComponent();
            this.roomId = roomId;
            this.currentUser = currentUser;
            this.isHost = isHost;

            lblRoomId.Text = "Room ID: " + roomId;
            lblHost.Text = "Host: " + (isHost ? currentUser : "...");
        }

        private void WaitingRoom_Load(object sender, EventArgs e)
        {
            ListenToRoomChanges();
        }

        // THAY THẾ TIMER BẰNG LISTENER
        private void ListenToRoomChanges()
        {
            DocumentReference doc = FirestoreService.DB.Collection("rooms").Document(roomId);

            // Hàm này sẽ tự chạy mỗi khi dữ liệu trên Firestore thay đổi
            _listener = doc.Listen(snapshot =>
            {
                if (!snapshot.Exists)
                {
                    MessageBox.Show("Phòng đã bị hủy!");
                    this.Close();
                    return;
                }

                RoomModel room = snapshot.ConvertTo<RoomModel>();

                // Cập nhật UI
                UpdateUI(room);

                // Logic chuyển màn hình
                CheckGameStatus(room);
            });
        }

        private void UpdateUI(RoomModel room)
        {
            // InvokeRequired để tránh lỗi cross-thread khi update UI từ Firestore thread
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateUI(room)));
                return;
            }

            lblHost.Text = "Host: " + room.HostUID;
            lblGuest.Text = string.IsNullOrEmpty(room.GuestUID) ? "Đang chờ..." : "Guest: " + room.GuestUID;
        }

        private async void CheckGameStatus(RoomModel room)
        {
            if (InvokeRequired) { Invoke(new Action(() => CheckGameStatus(room))); return; }

            // 1. Logic dành cho HOST: Kiểm tra đủ người và Random lượt đi
            if (isHost && !string.IsNullOrEmpty(room.GuestUID) && !room.GameStarted)
            {
                await Task.Delay(1000); // Đợi xíu cho UI cập nhật tên khách

                // --- RANDOM NGƯỜI ĐI TRƯỚC ---
                Random rand = new Random();
                // 0 là Host đi, 1 là Guest đi
                string firstPlayerUID = (rand.Next(0, 2) == 0) ? room.HostUID : room.GuestUID;

                DocumentReference doc = FirestoreService.DB.Collection("rooms").Document(roomId);

                // Cập nhật cùng lúc: Game bắt đầu VÀ Lượt của ai
                Dictionary<string, object> updates = new Dictionary<string, object>
        {
            { "GameStarted", true },
            { "Turn", firstPlayerUID } // Lưu UID người đi trước lên server
        };

                await doc.UpdateAsync(updates);
            }

            // 2. Cả Host và Guest đều lắng nghe: Nếu GameStarted == true -> Vào game
            if (room.GameStarted)
            {
                _listener.StopAsync(); // Dừng lắng nghe phòng chờ

                // --- SỬA LẠI: Không hiện MessageBox ở đây nữa cho đỡ rối ---
                // MessageBox.Show("Game bắt đầu!"); 

                GameOnline game = new GameOnline(roomId, currentUser, this.isHost);
                game.Show();
                this.Hide();
            }
        }


        private void WaitingRoom_FormClosing(object sender, FormClosingEventArgs e)
        {
            _listener?.StopAsync();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            _listener?.StopAsync();
            // Xử lý thêm: Nếu Host thoát thì xóa phòng, Guest thoát thì xóa tên khỏi phòng (Optional)
            this.Close();
        }
    }
}
