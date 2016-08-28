using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using BlacksmithPress.Diabetes.Entities;
using NUnit.Framework;

namespace RepositoryTests
{
    [TestFixture]
    public abstract class TestBase
    {
        protected IContainer Container => Configuration.Instance.Container;
    }
}
