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
                .RegisterDefaultDb("MessageDb")
                .RegisterLayout(Layout.Repository | Layout.Service, assem)
                .UseIocTransaction(assem)
                .UseCacheInterface(assem)
                .UseCacheClass(assem);
        }
    }
}
