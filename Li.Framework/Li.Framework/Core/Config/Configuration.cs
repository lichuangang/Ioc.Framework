using Autofac;
using Autofac.Extras.DynamicProxy;
using Li.Framework.Core.Attributes;
using Li.Framework.Core.Ioc;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Li.Framework.Core.Config
{
    /* ==============================================================================
     * 描述：Configuration
     * 创建人：李传刚 2017/7/17 16:32:11
     * ==============================================================================
     */
    public class Configuration
    {
        #region 字段 构造方法
        private readonly List<Type> _assemblys = new List<Type>();

        private static Configuration _instance = new Configuration();

        private Configuration() { }

        public static Configuration Instance
        {
            get { return _instance; }
        }
        #endregion

        public Configuration UseAutofac()
        {
            ContainerManager.SetContainer(new ContainerBuilder().Build());
            return this;
        }

        /// <summary>
        /// 开启事务  事务必需有对应的接口和实现
        /// </summary>
        public Configuration UseIocTransaction(params Assembly[] assemblies)
        {
            var container = ContainerManager.Container;

            var builder = new ContainerBuilder();
            builder.RegisterType<TransactionInterceptor>();
            builder.RegisterAssemblyTypes(assemblies)
                .Where(type => typeof(ITransaction).IsAssignableFrom(type))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(TransactionInterceptor));

            builder.Update(container);
            return this;
        }

        public Configuration UseCacheInterface(params Assembly[] assemblies)
        {
            var container = ContainerManager.Container;

            var builder = new ContainerBuilder();
            builder.RegisterType<CacheInterceptor>();

            builder.RegisterAssemblyTypes(assemblies)
                .Where(type => type.GetCustomAttributes(typeof(InterceptAttribute), false).Any())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .EnableInterfaceInterceptors();
            builder.Update(container);
            return this;
        }

        public Configuration UseCacheClass(params Assembly[] assemblies)
        {
            var container = ContainerManager.Container;

            var builder = new ContainerBuilder();
            builder.RegisterType<CacheInterceptor>();

            builder.RegisterAssemblyTypes(assemblies)
                .Where(type => type.GetCustomAttributes(typeof(InterceptAttribute), false).Any())
                .EnableClassInterceptors();//开启类方式注入，但方法必需是虚方法
            builder.Update(container);
            return this;
        }

        public Configuration RegisterDefaultDb(string dbStr)
        {
            var connectionStr = ConfigurationManager.ConnectionStrings[dbStr].ConnectionString;

            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = connectionStr, //必填
                DbType = DbType.SqlServer, //必填
                IsAutoCloseConnection = true, //默认false
                InitKeyType = InitKeyType.SystemTable //默认SystemTable
            });

            var builder = new ContainerBuilder();
            builder.Register(m => db);

            builder.Update(ContainerManager.Container);
            return this;
        }

        /// <summary>
        /// 加载相应层级
        /// </summary>
        public Configuration RegisterLayout(Layout layout, params Assembly[] assemblies)
        {
            if ((layout & Layout.Repository) == Layout.Repository)
            {
                RegisterLayout<RepositoryAttribute>(assemblies);
            }

            if ((layout & Layout.Service) == Layout.Service)
            {
                RegisterLayout<ServiceAttribute>(assemblies);
            }

            if ((layout & Layout.Component) == Layout.Component)
            {
                RegisterLayout<ComponentAttribute>(assemblies);
            }

            return this;
        }

        private void RegisterLayout<T>(params Assembly[] assemblies) where T : SelfAttribute
        {
            foreach (Assembly assembly in assemblies)
            {
                foreach (Type type in assembly.GetTypes().Where(type => type.IsClass && !type.IsAbstract &&
                   type.GetCustomAttributes(typeof(T), false).Any()))
                {
                    RegisterType<T>(type);
                }
            }
        }

        private void RegisterType<T>(Type type) where T : SelfAttribute
        {
            LifeStyle life = ParseLifeStyle<T>(type);
            if (!ContainerManager.Container.IsRegistered(type))
            {
                ContainerManager.RegisterType(type);
            }
            //foreach (Type interfaceType in type.GetInterfaces())
            //{

            //}
        }

        public static LifeStyle ParseLifeStyle<T>(Type type) where T : SelfAttribute
        {
            return ((T)type.GetCustomAttributes(typeof(T), false)[0]).LifeStyle;
        }
    }
}
