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
                BoardState = new int[12], // board trống
                Turn = currentUser,
                GameStarted = false
            };

            DocumentReference doc = FirestoreService.DB.Collection("rooms").Document(roomId);
            await doc.SetAsync(room);

            MessageBox.Show($"Tạo phòng thành công!\nRoom ID: {roomId}", "Success");
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
            DocumentSnapshot snap = await doc.GetSnapshotAsync();

            if (!snap.Exists)
            {
                MessageBox.Show("Phòng không tồn tại!");
                return;
            }

            RoomModel room = snap.ConvertTo<RoomModel>();

            if (room.GuestUID != "")
            {
                MessageBox.Show("Phòng đã đủ người!");
                return;
            }

            // cập nhật guest vào phòng
            await doc.UpdateAsync("GuestUID", currentUser);

            MessageBox.Show("Tham gia phòng thành công!", "Success");
        }
    }
}
