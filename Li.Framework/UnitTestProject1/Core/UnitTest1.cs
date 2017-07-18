using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;
using Autofac.Extras.DynamicProxy;
using UnitTestProject1.Utils;
using Li.Framework.Core.Ioc;

namespace UnitTestProject1.Core
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var builder = new ContainerBuilder();
            //注入代理拦截
            builder.RegisterType<CacheInterceptor>();

            //启用接口代理拦截
            builder.RegisterType<BookDao>().As<IBookDao>().EnableInterfaceInterceptors();

            var container = builder.Build();

            var instance = container.Resolve<IBookDao>();

            var name1 = instance.GetBookName("AA");

            var name2 = instance.GetBookName("AA");

        }


        [TestMethod]
        public void TestMethod2()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<InfoDao>().EnableClassInterceptors();

            //注入代理拦截
            builder.Register(m => new CacheInterceptor());

            var container = builder.Build();

            var instance = container.Resolve<InfoDao>();

            var info1 = instance.Info("BB");

            var info2 = instance.Info("BB");

        }
    }
}
