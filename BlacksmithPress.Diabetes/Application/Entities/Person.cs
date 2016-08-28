using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlacksmithPress.Diabetes.Types;

namespace BlacksmithPress.Diabetes.Entities
{
    public class Person : IPerson
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
