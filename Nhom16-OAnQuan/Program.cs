using Google.Cloud.Firestore.V1;
using Nhom16_OAnQuan.Classes;
using Nhom16_OAnQuan.Forms;

namespace Nhom16_OAnQuan
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            FirestoreHelper.SetEnvironmentVariable();
            ApplicationConfiguration.Initialize();
            Application.Run(new LogInForm());
        }
    }
}