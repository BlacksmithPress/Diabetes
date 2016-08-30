using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using BlacksmithPress.Diabetes.Types;

namespace BlacksmithPress.Diabetes.Persistence.Repositories
{
    public class Measurements : Repository<IMeasurement, long>
    {
        public Measurements(IContainer container) : base(container, "measurements/") {}
    }
}
