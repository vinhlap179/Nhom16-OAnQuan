using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nhom16_OAnQuan.Classes;
using Google.Cloud.Firestore;

namespace Nhom16_OAnQuan.Forms.GameForms
{
    public partial class LobbyForm : Form
    {
        private string currentUser;

        public LobbyForm(string username)
        {
            InitializeComponent();
            currentUser = username;
        }

        private async void BtnCreateRoom_Click(object sender, EventArgs e)
        {
            await CreateRoom();
        }

        private async void BtnFindRoom_Click(object sender, EventArgs e)
        {
            string roomId = txtRoomId.Text.Trim();

            if (string.IsNullOrEmpty(roomId))
            {
                MessageBox.Show("Nhập Room ID để tìm!");
                return;
            }

            DocumentReference doc = FirestoreService.DB.Collection("rooms").Document(roomId);
            DocumentSnapshot snap = await doc.GetSnapshotAsync();

            if (!snap.Exists)
            {
                MessageBox.Show("Không tìm thấy phòng!", "Search Failed");
                return;
            }

            RoomModel room = snap.ConvertTo<RoomModel>();

            string info =
                $"Room ID: {room.RoomId}\n" +
                $"Host: {room.HostUID}\n" +
                $"Guest: {room.GuestUID}\n" +
                $"Started: {room.GameStarted}";

            MessageBox.Show(info, "Thông tin phòng");
        }

        private async void BtnJoinRoom_Click(object sender, EventArgs e)
        {
            await JoinRoom(txtRoomId.Text.Trim());
        }

        // ------------------------------
        // 1) CHỨC NĂNG TẠO PHÒNG
        // ------------------------------
        private async Task CreateRoom()
        {
            string roomId = Guid.NewGuid().ToString().Substring(0, 6).ToUpper();

            var room = new RoomModel
            {
                RoomId = roomId,
                HostUID = currentUser,
                GuestUID = "",
                BoardState = new int[12], // Setup bàn cờ ban đầu ở đây nếu cần
                Turn = currentUser,
                GameStarted = false
            };

            DocumentReference doc = FirestoreService.DB.Collection("rooms").Document(roomId);
            await doc.SetAsync(room);

            // CHUYỂN SANG WAITING ROOM (Là Host)
            WaitingRoom waitForm = new WaitingRoom(roomId, currentUser, true);
            waitForm.Show();
            this.Hide(); // Ẩn Lobby đi
        }

        // ------------------------------
        // 2) CHỨC NĂNG JOIN PHÒNG
        // ------------------------------
        private async Task JoinRoom(string roomId)
        {
            if (string.IsNullOrEmpty(roomId))
            {
                MessageBox.Show("Vui lòng nhập Room ID!");
                return;
            }

            DocumentReference doc = FirestoreService.DB.Collection("rooms").Document(roomId);

            // Dùng Transaction để đảm bảo không có 2 người cùng vào 1 lúc
            string result = await FirestoreService.DB.RunTransactionAsync(async transaction =>
            {
                DocumentSnapshot snap = await transaction.GetSnapshotAsync(doc);
                if (!snap.Exists) return "NOT_FOUND";

                RoomModel room = snap.ConvertTo<RoomModel>();
                if (!string.IsNullOrEmpty(room.GuestUID)) return "FULL";

                // Update Guest
                transaction.Update(doc, "GuestUID", currentUser);
                return "OK";
            });

            if (result == "NOT_FOUND") { MessageBox.Show("Phòng không tồn tại!"); return; }
            if (result == "FULL") { MessageBox.Show("Phòng đã đầy!"); return; }

            // CHUYỂN SANG WAITING ROOM (Là Guest)
            WaitingRoom waitForm = new WaitingRoom(roomId, currentUser, false);
            waitForm.Show();
            this.Hide();
        }

        private void LobbyForm_Load(object sender, EventArgs e)
        {
           
        }
    }
}
