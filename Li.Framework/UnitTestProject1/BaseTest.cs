using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Li.Framework.Core.Config;
using Li.Framework.Core.Attributes;

namespace UnitTestProject1
{
    [TestClass]
    public class BaseTest
    {
        public BaseTest()
        {
            var assem = this.GetType().Assembly;

            Configuration.Instance
                .UseAutofac()
                .UseLog4Net()
                .RegisterDefaultDb("MessageDb")
                .RegisterLayout(Layout.Repository | Layout.Service, assem)
                .UseTransaction(assem)
                .UseCacheInterface(assem)
                .UseCacheClass(assem);
        }
    }
}
