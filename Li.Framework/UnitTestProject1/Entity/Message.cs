using Li.Framework.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.Entity
{
    /* ==============================================================================
     * 描述：MessageHst
     * 创建人：李传刚 2017/7/19 19:05:40
     * ==============================================================================
     */
    public class Message : IdKey<string>
    {
        public string Tag { get; set; }

        public string Topic { get; set; }

        public long Timestamp { get; set; }

        public string Type { get; set; }
    }
}
