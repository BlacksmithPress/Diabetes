using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BlacksmithPress.Diabetes.Cloud.Models
{
    public class Database : DbContext
    {
        public Database(DbContextOptions<Database> options) : base(options) {}

        public DbSet<Person> People { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<UnitOfMeasure> UnitsOfMeasure { get; set; }
        public DbSet<MeasuredAttribute> MeasuredAttributes { get; set; }
    }
}
