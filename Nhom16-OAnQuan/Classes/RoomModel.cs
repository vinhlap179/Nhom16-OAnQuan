using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace Nhom16_OAnQuan.Classes
{
    [FirestoreData]
    public class RoomModel
    {
        [FirestoreProperty] public string RoomId { get; set; }
        [FirestoreProperty] public string HostUID { get; set; }
        [FirestoreProperty] public string GuestUID { get; set; }
        [FirestoreProperty] public int[] BoardState { get; set; }
        [FirestoreProperty] public string Turn { get; set; }
        [FirestoreProperty] public bool GameStarted { get; set; }
    }
}
