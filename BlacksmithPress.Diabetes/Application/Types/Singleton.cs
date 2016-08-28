using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlacksmithPress.Diabetes.Types
{
    public class Singleton<InstanceType> where InstanceType : class, new()
    {
        private static readonly Lazy<InstanceType> _instance = new Lazy<InstanceType>(() => new InstanceType());
        public static InstanceType Instance { get { return _instance.Value; } }
    }
}
