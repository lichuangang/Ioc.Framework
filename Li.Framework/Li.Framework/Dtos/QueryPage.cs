using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Li.Framework.Dtos
{
    /* ==============================================================================
     * 描述：Query
     * 创建人：李传刚 2017/7/20 14:58:29
     * ==============================================================================
     */
    public class QueryPage
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string SortField { get; set; }

        public string Order { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public bool IsAsc
        {
            get
            {
                return Order != null && "ascending".Equals(Order.ToLower());
            }

        }
    }
}
