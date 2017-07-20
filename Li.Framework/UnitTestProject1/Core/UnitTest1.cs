using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;
using Autofac.Extras.DynamicProxy;
using UnitTestProject1.Utils;
using Li.Framework.Core.Ioc;
using Li.Framework.Core.Config;
using Li.Framework.Repositorys;
using UnitTestProject1.Repositorys;
using UnitTestProject1.Services;
using Li.Framework.Core.Log4Net;
using UnitTestProject1.Entity;

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
        public void TestMethod41()
        {
            var instance = ContainerManager.Resolve<InfoDao>();
            instance.Info("aaa");
        }

        [TestMethod]
        public void TestMethod5()
        {
            var rsp = ContainerManager.Resolve<MessageHstRsp>();
            var ent = rsp.GetById("1675fc87-ea33-4d01-b953-571828c8f3c2");
            Assert.IsTrue(ent != null);
        }

        [TestMethod]
        public void TestMethod6()
        {
            var service = ContainerManager.Resolve<MessageService>();
            var ent = service.GetById("1675fc87-ea33-4d01-b953-571828c8f3c2");
            Assert.IsTrue(ent != null);
        }

        [TestMethod]
        public void TestMethod7()
        {
            var service = ContainerManager.Resolve<IMessageService>();
            service.Tansation();
        }

        [TestMethod]
        public void TestMethod8()
        {
            var service = ContainerManager.Resolve<MessageHstRsp>();
            var ent = service.GetByIdCache("1675fc87-ea33-4d01-b953-571828c8f3c2");
            //第二次不再查数据库。直接从缓存中取
            var ent2 = service.GetByIdCache("1675fc87-ea33-4d01-b953-571828c8f3c2");
        }
        [TestMethod]
        public void TestMethod9()
        {
            var log = ContainerManager.Resolve<ILoggerFactory>().Create(this.GetType());
            log.InfoFormat("{0}：消息", "info");
        }

        [TestMethod]
        public void TestMethod10()
        {
            //获取匿名实现
            var rsy = ContainerManager.Resolve<BaseRepository<MessageHst, string>>();
            var ent = rsy.GetById("1675fc87-ea33-4d01-b953-571828c8f3c2");
            Assert.IsTrue(ent != null);
        }
    }
}
