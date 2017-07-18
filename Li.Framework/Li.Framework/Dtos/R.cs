using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Li.Framework.Dtos
{
    /* ==============================================================================
     * 描述：R
     * 创建人：李传刚 2017/7/14 13:55:38
     * ==============================================================================
     */
    public class R
    {
        private R()
        {

        }

        public int Code { get; set; }

        public string Msg { get; set; }

        public object Data { get; set; }

        public static R Ok
        {
            get
            {
                return new R { Code = 200, Msg = "成功" };
            }
        }

        public static R Error
        {
            get
            {
                return new R { Code = 500, Msg = "失败" };
            }
        }

        public static R Success(object data)
        {
            return new R { Code = 200, Msg = "成功", Data = data };
        }

        public static R Fail(string msg)
        {
            return new R { Code = 500, Msg = msg };
        }

        public static R Fail(string msg, object data)
        {
            return new R { Code = 500, Msg = msg, Data = data };
        }
    }
}
