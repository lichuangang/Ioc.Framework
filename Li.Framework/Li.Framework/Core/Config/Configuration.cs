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

        public Configuration UseCache(params Assembly[] assemblies)
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

        public Configuration RegisterRepositorys(params Assembly[] assemblies)
        {
            foreach (Assembly assembly in assemblies)
            {
                foreach (Type type in assembly.GetTypes().Where(type => type.IsClass && !type.IsAbstract &&
                   type.GetCustomAttributes(typeof(RepositoryAttribute), false).Any()))
                {
                    RegisterType<RepositoryAttribute>(type);
                }
            }
            return this;
        }

        public Configuration RegisterComponents(params Assembly[] assemblies)
        {



            return this;
        }

        public Configuration RegisterServices(params Assembly[] assemblies)
        {



            return this;
        }



        public Configuration RegisterBusinessComponents(params Assembly[] assemblies)
        {
            List<Type> registeredTypes = new List<Type>();
            foreach (Assembly assembly in assemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (!registeredTypes.Contains(type))
                    {
                        //RegisterComponentType(type);
                    }
                }
            }
            return this;
        }

        private void RegisterType<T>(Type type) where T : SelfAttribute
        {
            LifeStyle life = ParseLifeStyle<T>(type);
            ContainerManager.RegisterType(type);
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
