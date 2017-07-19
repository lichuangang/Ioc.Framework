using Autofac;
using Autofac.Extras.DynamicProxy;
using Li.Framework.Core.Ioc;
using System;
using System.Collections.Generic;
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

    }
}
