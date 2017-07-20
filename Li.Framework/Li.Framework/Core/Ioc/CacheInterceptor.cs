using Castle.DynamicProxy;
using Li.Framework.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Li.Framework.Core.Ioc
{
    /* ==============================================================================
     * 描述：CacheInterceptor
     * 创建人：李传刚 2017/7/17 14:31:16
     * ==============================================================================
     */
    public class CacheInterceptor : IInterceptor
    {
        static Dictionary<string, object> _catheDict = new Dictionary<string, object>();

        public void Intercept(IInvocation invocation)
        {
            var attributes = invocation.MethodInvocationTarget.GetCustomAttributes(typeof(CacheAttribute), true);
            var attr = attributes.OfType<CacheAttribute>().FirstOrDefault();
            if (attr != null)
            {
                var methodName = invocation.Method.Name;
                var param = string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray());
                var key = methodName + "_" + param;

                if (_catheDict.ContainsKey(key))
                {
                    invocation.ReturnValue = _catheDict[key];
                }
                else
                {
                    invocation.Proceed();
                    _catheDict.Add(key, invocation.ReturnValue);
                }
            }
            else
            {
                invocation.Proceed();
            }
        }
    }
}
