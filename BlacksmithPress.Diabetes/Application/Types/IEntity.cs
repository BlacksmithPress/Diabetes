using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlacksmithPress.Diabetes.Types
{
    public interface IEntity<KeyType> where KeyType : struct 
    {
        KeyType Id { get; set; }
    }
}
