using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Reface.AppStarter.Proxy.Tests
{
    [TestClass]
    public class GetAttrsTests
    {
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
        public class T : Attribute { }

        [T]
        interface IA { }

        [T]
        [System.ComponentModel.Category("abc")]
        public class A : IA { }

        [T]
        public class B : A { }

        [TestMethod]
        public void GetDisplayNameFromB()
        {
            IEnumerable<T> ts = typeof(B).GetCustomAttributes<T>(true);
            Assert.AreEqual(2, ts.Count());
        }
    }
}
