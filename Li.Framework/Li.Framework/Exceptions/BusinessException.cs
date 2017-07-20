using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Li.Framework.Exceptions
{
    /* ==============================================================================
     * 描述：BusinessException
     * 创建人：李传刚 2017/7/20 14:54:32
     * ==============================================================================
     */
    public class BusinessException : Exception
    {
        public BusinessException(string message, int? code = null)
            : base(message)
        {
            Code = code;
        }

        public int? Code { get; set; }
    }
}
