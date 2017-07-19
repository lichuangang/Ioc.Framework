using Autofac;
using Autofac.Core.Lifetime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Li.Framework.Core.Ioc
{
    /* ==============================================================================
     * 描述：ContainerManager
     * 创建人：李传刚 2017/7/19 9:32:45
     * ==============================================================================
     */
    public static class ContainerManager
    {
        private static IContainer _container;

        public static void SetContainer(IContainer container)
        {
            _container = container;
        }

        public static IContainer Container
        {
            get { return _container; }
        }

        #region Register

        public static void RegisterAssemblyTypes(params Assembly[] assemblies)
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(assemblies);
            builder.Update(_container);
        }

        public static void RegisterType(Type type)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType(type);
            builder.Update(_container);
        }

        public static void RegisterType<T>()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<T>();
            builder.Update(_container);
        }

        #endregion

        #region Resolve
        public static T Resolve<T>(string key = "", ILifetimeScope scope = null) where T : class
        {
            if (scope == null)
            {
                scope = Scope();
            }
            if (string.IsNullOrEmpty(key))
            {
                return scope.Resolve<T>();
            }
            return scope.ResolveKeyed<T>(key);
        }

        public static object Resolve(Type type, ILifetimeScope scope = null)
        {
            if (scope == null)
            {
                scope = Scope();
            }
            return scope.Resolve(type);
        }

        public static T[] ResolveAll<T>(string key = "", ILifetimeScope scope = null)
        {
            if (scope == null)
            {
                scope = Scope();
            }
            if (string.IsNullOrEmpty(key))
            {
                return scope.Resolve<IEnumerable<T>>().ToArray();
            }
            return scope.ResolveKeyed<IEnumerable<T>>(key).ToArray();
        }

        public static T ResolveUnregistered<T>(ILifetimeScope scope = null) where T : class
        {
            return ResolveUnregistered(typeof(T), scope) as T;
        }

        public static object ResolveUnregistered(Type type, ILifetimeScope scope = null)
        {
            if (scope == null)
            {
                scope = Scope();
            }
            var constructors = type.GetConstructors();
            foreach (var constructor in constructors)
            {
                try
                {
                    var parameters = constructor.GetParameters();
                    var parameterInstances = new List<object>();
                    foreach (var parameter in parameters)
                    {
                        var service = Resolve(parameter.ParameterType, scope);
                        if (service == null) throw new ArgumentException("Unkown dependency");
                        parameterInstances.Add(service);
                    }
                    return Activator.CreateInstance(type, parameterInstances.ToArray());
                }
                catch (ArgumentException)
                {

                }
            }
            throw new ArgumentException("在所有的依赖项中，未找到合适的构造函数。");
        }

        public static bool TryResolve(Type serviceType, ILifetimeScope scope, out object instance)
        {
            if (scope == null)
            {
                scope = Scope();
            }
            return scope.TryResolve(serviceType, out instance);
        }

        public static bool IsRegistered(Type serviceType, ILifetimeScope scope = null)
        {
            if (scope == null)
            {
                scope = Scope();
            }
            return scope.IsRegistered(serviceType);
        }

        public static object ResolveOptional(Type serviceType, ILifetimeScope scope = null)
        {
            if (scope == null)
            {
                scope = Scope();
            }
            return scope.ResolveOptional(serviceType);
        }
        #endregion

        public static ILifetimeScope Scope()
        {
            try
            {
                //if (HttpContext.Current != null)
                //    return AutofacDependencyResolver.Current.RequestLifetimeScope;

                return Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
            }
            catch (Exception)
            {
                return Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
            }
        }

    }
}
