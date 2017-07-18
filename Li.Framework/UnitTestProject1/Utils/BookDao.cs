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
     * 描述：BookDao
     * 创建人：李传刚 2017/7/17 14:45:01
     * ==============================================================================
     */
    [Intercept(typeof(CacheInterceptor))]
    public class BookDao : IBookDao
    {
        public virtual string GetBookName(string id)
        {
            return "时间：" + DateTime.Now.ToShortTimeString();
        }
    }
}
