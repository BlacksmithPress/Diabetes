using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace BlacksmithPress.Diabetes.Types
{
    public interface IConfiguration
    {
        Uri PersistenceUri { get; }
        IContainer Container { get; }
    }
}
