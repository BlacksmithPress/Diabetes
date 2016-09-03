using System.Data.Entity;

namespace BlacksmithPress.Diabetes.Persistence.Database
{
    public class Context : DbContext
    {
        public Context(string connectionString) : base(connectionString) {}
        public Context() : base("Server=(local);Database=BlacksmithPress.Diabetes;Trusted_Connection=true;MultipleActiveResultSets=true;") {}

        public DbSet<Person> People { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
