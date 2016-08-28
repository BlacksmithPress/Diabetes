using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlacksmithPress.Diabetes.Data
{
    public class Context : DbContext
    {
        public Context(string connectionString) : base(connectionString) {}
        public Context() : base("DefaultConnection") {}

        public DbSet<Person> People { get; set; }
        public DbSet<Measurement> Measurements { get; set; }

    }
}
