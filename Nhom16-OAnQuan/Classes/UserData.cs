using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhom16_OAnQuan.Classes
{
    [FirestoreData]
    internal class UserData
    {
        [FirestoreProperty]
        public string Username { get; set; }
        [FirestoreProperty]
        public string Password { get; set; }
        //Quen mat khau
        [FirestoreProperty]
        public string? ResetToken { get; set; }

        [FirestoreProperty]
        public DateTime? TokenExpiry { get; set; }

        [FirestoreProperty]
        public int Wins { get; set; } = 0;

        [FirestoreProperty]
        public int Losses { get; set; } = 0;

        [FirestoreProperty]
        public int TotalGames { get; set; } = 0;

    }
}
