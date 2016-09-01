using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using BlacksmithPress.Diabetes.Entities;
using BlacksmithPress.Diabetes.Types;

namespace BlacksmithPress.Diabetes.Persistence.Repositories
{
    public class People : Repository<IPerson, long>
    {
        public People() : this(BuildDefaultContainer()) {}

        public People(IContainer container) : base(container, "people/") {}
    }
}
