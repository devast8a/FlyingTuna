using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using FlyingTuna.UnitTests.TestingUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlyingTuna.UnitTests
{
    [TestClass]
    public class ServiceManagerTest
    {
        [TestMethod]
        public void GetProvider()
        {
            var manager = new ServiceManager();
            var empty = new EmptyClass();

            manager.SetProvider(empty);

            Assert.AreSame(manager.GetProvider<EmptyClass>(), empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DoubleAddException()
        {
            var manager = new ServiceManager();
            var empty = new EmptyClass();

            manager.SetProvider(empty);
            manager.SetProvider(empty);
        }
        
        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetException()
        {
            var manager = new ServiceManager();
            manager.GetProvider<EmptyClass>();
        }
    }
}
