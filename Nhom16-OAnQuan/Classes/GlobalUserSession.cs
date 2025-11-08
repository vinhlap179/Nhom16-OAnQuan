using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhom16_OAnQuan.Classes
{
    public static class GlobalUserSession
    {
        public static string? CurrentToken { get; set; }

        public static string? CurrentUsername { get; set; }

        public static void ClearSession()
        {
            CurrentToken = null;
            CurrentUsername = null;
        }
    }
}
