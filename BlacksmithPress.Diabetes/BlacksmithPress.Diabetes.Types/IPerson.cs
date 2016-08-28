using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlacksmithPress.Diabetes.Types
{
    public interface IPerson
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }
}
