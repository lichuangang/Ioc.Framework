using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Li.Framework.Common
{
    /* ==============================================================================
     * 描述：SingleInstance
     * 创建人：李传刚 2017/7/17 16:14:36
     * ==============================================================================
     */
    public class SingleInstance<T> where T : class,new()
    {
        private static T mySelf = default(T);
        /// <summary>
        /// 获取单例实例
        /// </summary>
        public static T Instance
        {
            get
            {
                if (mySelf == null)
                {
                    mySelf = new T();
                }
                return mySelf;
            }
        }
    }
}
