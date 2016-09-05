using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BlacksmithPress.Diabetes.Types
{
    public static class Extensions
    {
        public static bool AreEqual(this NetworkCredential these, NetworkCredential credentials)
        {
            return these.Domain == credentials.Domain
                && these.UserName == credentials.UserName
                && (these.Password == credentials.Password);
        }


        public static string ToBasicAuthentication(this NetworkCredential credentials)
        {
            var plaintext = $"{credentials.UserName}:{credentials.Password}";
            var bytes = Encoding.UTF8.GetBytes(plaintext);
            return Convert.ToBase64String(bytes);
        }

    }
}
