using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlacksmithPress.Diabetes.Types
{
    public interface IPerson : IEntity<long>
    {
        string Name { get; set; }
    }
}
