using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BlacksmithPress.Diabetes.Persistence.Database
{
    public class User : Person
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public NetworkCredential ToCredentials()
        {
            return new NetworkCredential(Username, Password);
        }

        public override string ToString()
        {
            return $"[{Id}] {Name} ({Username})";
        }

        public override int GetHashCode()
        {
            return $"{ToString()}{Password}".GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var user = obj as User;
            if (user == null) return false;

            return $"{ToString()}{Password}" == $"{user.ToString()}{user.Password}";
        }
    }
}
