using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Li.Framework.Core.Log4Net
{
    /* ==============================================================================
     * 描述：ILoggerFactory
     * 创建人：李传刚 2017/7/20 13:58:42
     * ==============================================================================
     */
    public interface ILoggerFactory
    {
        ILog Create(string name);

        ILog Create(Type type);
    }
}
