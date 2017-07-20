using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Li.Framework.Core.Config;

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
                .RegisterRepositorys(assem)
                .UseIocTransaction(assem)
                .UseCache(assem);
        }
    }
}
