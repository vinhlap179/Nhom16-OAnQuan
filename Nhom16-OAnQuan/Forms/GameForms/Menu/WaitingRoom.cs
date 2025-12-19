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

            // 1. Nếu là HOST và thấy có Guest vào -> Set GameStarted = true
            if (isHost && !string.IsNullOrEmpty(room.GuestUID) && !room.GameStarted)
            {
                // Delay nhỏ để người dùng kịp nhìn thấy tên Guest hiện lên
                await Task.Delay(1000);
                DocumentReference doc = FirestoreService.DB.Collection("rooms").Document(roomId);
                await doc.UpdateAsync("GameStarted", true);
            }

            // 2. Cả Host và Guest đều lắng nghe: Nếu GameStarted == true -> Vào game
            // Trong file WaitingRoom.cs
            if (room.GameStarted)
            {
                _listener.StopAsync(); // Dừng lắng nghe phòng chờ
                MessageBox.Show("Game bắt đầu!");

                // --- SỬA DÒNG NÀY ---
                // Truyền biến 'isHost' (chữ thường) của class, KHÔNG ĐƯỢC truyền 'true' hay 'false' cứng
                GameOnline game = new GameOnline(roomId, currentUser, this.isHost);
                // --------------------

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
