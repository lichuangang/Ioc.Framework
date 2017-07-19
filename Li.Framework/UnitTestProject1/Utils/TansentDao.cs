using Li.Framework.Core.Attribute;
using Li.Framework.Core.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.Utils
{
    /* ==============================================================================
     * 描述：TansentDao
     * 创建人：李传刚 2017/7/19 10:06:55
     * ==============================================================================
     */
    public class TansentDao : ITransaction,ITansentDao
    {
        [TransactionCallHandler]
        public string Run()
        {
            return DateTime.Now.ToShortTimeString();
        }
    }
}
