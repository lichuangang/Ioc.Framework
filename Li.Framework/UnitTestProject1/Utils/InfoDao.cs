using Autofac.Extras.DynamicProxy;
using Li.Framework.Core.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.Utils
{
    /* ==============================================================================
     * 描述：InfoDao
     * 创建人：李传刚 2017/7/17 15:53:01
     * ==============================================================================
     */
    [Intercept(typeof(CacheInterceptor))]
    public class InfoDao
    {
        public virtual string Info(string id)
        {
            return "Id:" + id + DateTime.Now.ToShortTimeString();
        }
    }
}
