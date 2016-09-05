using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using BlacksmithPress.Diabetes.Types;

namespace BlacksmithPress.Diabetes.Persistence.Database
{
    public class Context : DbContext
    {
        public Context() : this("BlacksmithPress.Diabetes") {}
        public Context(string connectionString) : base(connectionString) {}
        public DbSet<Person> People { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<User> Users { get; set; }


        public IPrincipal Authenticate(NetworkCredential credentials)
        {
            foreach (var user in Users.Where(u => !string.IsNullOrEmpty(u.Username)))
            {
                if (user.ToCredentials().AreEqual(credentials))
                {
                    var claim = new Claim(ClaimTypes.Name, credentials.UserName);
                    var identity = new ClaimsIdentity(new Claim[] {claim}, AuthenticationTypes.Basic);
                    var principal = new ClaimsPrincipal(identity);
                    return principal;
                }
            }
            return default(IPrincipal);
        }

    }
}
