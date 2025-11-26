using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace Nhom16_OAnQuan.Classes
{
    public static class FirestoreService
    {
        public static FirestoreDb DB;

        static FirestoreService()
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "service-account.json");
            DB = FirestoreDb.Create("your-project-id");
        }
    }
}
