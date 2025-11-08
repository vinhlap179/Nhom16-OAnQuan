using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhom16_OAnQuan.Classes
{
    internal static class FirestoreHelper
    {
        static string fireconfig = @"
        {
      ""type"": ""service_account"",
      ""project_id"": ""nhom16-oanquan"",
      ""private_key_id"": ""4fe6cb142aa3aac664869a16b42cc25ee28c248e"",
      ""private_key"": ""-----BEGIN PRIVATE KEY-----\nMIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQDI41dYCAX2QNvm\n0ZAPqyv3DdlNeQTUjQbrgczIKnUYMWEWqei7mmkEi006duFMKGV9dHRedBNs7TVv\nn2Ths735qHaf8W7OyxgK0JZFGoUHpFH4Am6s+rkEmiDdyH5XcmRZOw0ZAf5NfqT7\nvkdh/H8c0VTe7F2VvCXB+gIwkkhAgfbPhzMfcideYzDxj1UXjt/gNcd0caj2nL3O\nMaqv7m0D5rG9EeKeBu9YTBdT/WWTGnEx9ToWSK+5lxJa6lWjA6Fs91gRlfu2cDID\nGcOzLou52NLmVZlMntbK7n8oeVAqoWuSMHGAXo4gAkuFzbqO5Km9EFECKk5sQfeJ\nseN5dIQLAgMBAAECggEAX63X9fesT0NI+UCqqqvOPtb7vqh7Inkg6J5j2JpQNvud\n0FuQm3nJtv5aBBSG7ez3ma8TlcEXV3QLVWTHebBQNU6FYVxueFPG2a/LQpKE9cTP\nvZ369Kxd3tFCAh/x/Nhg7xbvABAw/Nvq3HQeL49BQ4p4L8zILf70xNS8okGp4/6p\nz880S0mW4N4xOXMr2XFyMICBz/98tdNvgjMWW1jbi7w9KhP7OHk7lq8ZFKECHRHf\nR/Yu1xGKHOUWsGKGDEpyl6s6v0N44QisxOHP+yUryr5ultFoHdo0mkt+vLDKTRm0\n+BS9oopntL2TxDaAvWx5Zddvj2s0v75xsa/Viu6yoQKBgQD1Ijk5k16DaSZ/p30o\nYIxfyUz4bgoS3WlyxMpWBnDZDb8zd7ZSQiAAdzH8TtcKkBSquhHCFPvdsyms4naB\n5I9Xmhrb4F496deVleSnqgVJmgWhBshZ0/i4UrFiV3uAPNoor98MTy9XFq5O/P53\nbx0vIqqngnuMP6AgpcI+41IPmQKBgQDRywSvkOqhwZeMI8RZQpZ5UMXqyn2X2/Nk\nlDucyDQFIvwy2Qo6Sg1QvVq8IbCsz/oTvmSA95pUr+A0Iopgp5FyCulTPyAnImt0\nmp9Pqgw3rSaG6Xm8utktcQEV/cJC9pYu6DV/DIo/wN2vQxhYf2ofkH/UYqSrWaHH\nRhQYsj9HQwKBgCl9+nuxYx0RLlALNJciCICqHRor3g6lZZTklG8NNgq3VfbLO5Qe\n1zXc9xwC6ElEbAsd5aWZUayMB/DYgECqRE0Kq6MuDiWisDgEoCh1EeIMbQl9kWoL\nBB2G+blwn1nscuzSIAoAWT3/e8+o6RWkPCdfUc16u5jYqCz+gauNZLV5AoGBAK7k\nGhHqM0+JvbacEpF+y0ZzKaskBzNTr0HHuTJoS8WEds5E1Rvmo9cNI9j2FPzbnFRd\nQhepUkULIVMVAFq3Fq8FnaFCXKGDNiVMyUZ2Mh0wAZAyjsPg2C/jps4Yb5u3SVft\nSLuTTqT7OVNkEaWlAmkx4v7BBRm7bPxc1WAJKGtVAoGBAPRsJz7IzViJ/S68AYLA\nrF2VGAArJHo0MENjJNClfoIJqM57lLgBrHeP/MTjDU+UUx0WmVhHq0odqeqX5M5Y\nbmOdYDRS0e79xrHn4hAq0Dyvd0myO7dc6K2mvZei26a1z1dsYIz3fw5K4D/d+aZj\nqNMisPgveI7uHa5z+fupy2MJ\n-----END PRIVATE KEY-----\n"",
      ""client_email"": ""firebase-adminsdk-fbsvc@nhom16-oanquan.iam.gserviceaccount.com"",
      ""client_id"": ""108147024506095958161"",
      ""auth_uri"": ""https://accounts.google.com/o/oauth2/auth"",
      ""token_uri"": ""https://oauth2.googleapis.com/token"",
      ""auth_provider_x509_cert_url"": ""https://www.googleapis.com/oauth2/v1/certs"",
      ""client_x509_cert_url"": ""https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-fbsvc%40nhom16-oanquan.iam.gserviceaccount.com"",
      ""universe_domain"": ""googleapis.com""
    }
    ";
        static string filepath = "";
        public static FirestoreDb? Database { get; private set; }

        public static void SetEnvironmentVariable()
        {
            filepath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(Path.GetRandomFileName())) + ".json";
            File.WriteAllText(filepath, fireconfig);
            File.SetAttributes(filepath, FileAttributes.Hidden);
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filepath);
            Database = FirestoreDb.Create("nhom16-oanquan");
            File.Delete(filepath);
        }
    }
}
