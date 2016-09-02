using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlacksmithPress.Diabetes.Types
{
    /// <summary>
    /// The base implementation of a singleton class. For more information about the singleton pattern, see <a href="https://en.wikipedia.org/wiki/Singleton_pattern">Singleton Pattern</a> at Wikipedia.
    /// </summary>
    /// <typeparam name="InstanceType">The type of the instance.</typeparam>
    public class Singleton<InstanceType> where InstanceType : class, new()
    {
        private static readonly Lazy<InstanceType> _instance = new Lazy<InstanceType>(() => new InstanceType());
        /// <summary>
        /// Gets the one and only instance of this singleton.
        /// </summary>
        /// <value>The instance.</value>
        public static InstanceType Instance { get { return _instance.Value; } }
    }
}
