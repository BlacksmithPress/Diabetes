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
    }
}
