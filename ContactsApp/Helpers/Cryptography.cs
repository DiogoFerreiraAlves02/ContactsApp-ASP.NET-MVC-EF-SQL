using System.Security.Cryptography;
using System.Text;

namespace ContactsApp.Helpers {
    public static class Cryptography {
        public static string GenerateHash(this string val) {

            var hash = SHA1.Create();
            var encoding = new ASCIIEncoding();
            var arr = encoding.GetBytes(val);

            arr = hash.ComputeHash(arr);

            var strHexa = new StringBuilder();

            foreach (var item in arr) {
                strHexa.Append(item.ToString("x2"));
            }

            return strHexa.ToString();
        }
    }
}
