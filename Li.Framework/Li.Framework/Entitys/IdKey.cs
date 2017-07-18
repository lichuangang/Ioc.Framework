using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Li.Framework.Entitys
{
    /* ==============================================================================
     * 描述：IdKey
     * 创建人：李传刚 2017/7/14 14:14:21
     * ==============================================================================
     */
    public class IdKey<T>
    {
        public T Id { get; set; }
    }
}
