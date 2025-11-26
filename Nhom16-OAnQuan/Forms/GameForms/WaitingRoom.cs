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
        private ListenerRegistration listener;



        public WaitingRoom()
        {
            InitializeComponent();
            this.roomId = roomId;
            this.currentUser = currentUser;

            lblRoomId.Text = "Room ID: " + roomId;
        }

        private void WaitingRoom_Load(object sender, EventArgs e)
        {
            ListenRoomChanges();
        }

        // =====================================================
        // LISTEN REALTIME - CẬP NHẬT KHI CÓ NGƯỜI VÀO PHÒNG
        // =====================================================
        private void ListenRoomChanges()
        {
            DocumentReference doc = FirestoreService.DB.Collection("rooms").Document(roomId);

            listener = doc.Listen(snapshot =>
            {
                if (!snapshot.Exists) return;

                RoomModel room = snapshot.ConvertTo<RoomModel>();

                Invoke(new Action(() =>
                {
                    lblHost.Text = "Host: " + room.HostUID;
                    lblGuest.Text = "Guest: " + (room.GuestUID == "" ? "Đang chờ..." : room.GuestUID);

                    // Nếu đủ người
                    if (room.GuestUID != "" && !room.GameStarted)
                    {
                        StartGame(room);
                    }
                }));
            });
        }

        // =====================================================
        // BẮT ĐẦU GAME (HOST SẼ GỌI LỆNH START)
        // =====================================================
        private async void StartGame(RoomModel room)
        {
            DocumentReference doc = FirestoreService.DB.Collection("rooms").Document(room.RoomId);

            // Chỉ Host mới cập nhật trạng thái game
            if (currentUser == room.HostUID)
            {
                await doc.UpdateAsync("GameStarted", true);
            }

            listener.Stop();

            MessageBox.Show("Game đã bắt đầu!");

            // Chuyển sang form game
            GameBoardGUI game = new GameBoardGUI();
            game.Show();
            this.Hide();
        }

        private void WaitingRoom_FormClosing(object sender, FormClosingEventArgs e)
        {
            listener?.Stop();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close(); // tự động quay lại form trước
        }
    }
}
