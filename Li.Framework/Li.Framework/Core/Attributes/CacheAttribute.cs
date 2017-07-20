using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Li.Framework.Core.Attributes
{
    /* ==============================================================================
     * 描述：CacheAttribute
     * 创建人：李传刚 2017/7/19 11:28:57
     * ==============================================================================
     */
    public class CacheAttribute : Attribute
    {
        /// <summary>
        /// 默认 60 秒
        /// </summary>
        public CacheAttribute()
            : this(60)
        {

        }

        /// <summary>
        /// 缓存时间，以秒为单位
        /// </summary>
        public CacheAttribute(int expireTime)
        {
            ExpireTime = expireTime;
        }

        public int ExpireTime { get; private set; }
    }
}
