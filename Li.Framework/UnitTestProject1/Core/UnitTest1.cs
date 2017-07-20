using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;
using Autofac.Extras.DynamicProxy;
using UnitTestProject1.Utils;
using Li.Framework.Core.Ioc;
using Li.Framework.Core.Config;
using Li.Framework.Repositorys;
using UnitTestProject1.Repositorys;

namespace UnitTestProject1.Core
{
    [TestClass]
    public class UnitTest1 : BaseTest
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

        [TestMethod]
        public void TestMethod3()
        {
            Configuration.Instance.UseAutofac().UseIocTransaction(this.GetType().Assembly);
            var instance = ContainerManager.Resolve<ITansentDao>();
            instance.Run();
        }

        [TestMethod]
        public void TestMethod4()
        {
            var instance = ContainerManager.Resolve<IBookDao>();
            instance.GetBookName("abc");
        }

        [TestMethod]
        public void TestMethod5()
        {
            //ContainerManager.RegisterType<MessageHstRsp>();
            var rsp = ContainerManager.Resolve<MessageHstRsp>();
            var ent = rsp.GetById("1675fc87-ea33-4d01-b953-571828c8f3c2");
            Assert.IsTrue(ent != null);
        }
    }
}
