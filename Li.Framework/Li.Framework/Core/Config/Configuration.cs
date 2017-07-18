using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Li.Framework.Core.Config
{
    /* ==============================================================================
     * 描述：Configuration
     * 创建人：李传刚 2017/7/17 16:32:11
     * ==============================================================================
     */
    public class Configuration
    {
        #region 字段 构造方法
        private readonly List<Type> _assemblys = new List<Type>();

        private static Configuration _instance = new Configuration();

        private Configuration() { }

        public static Configuration Instance
        {
            get { return _instance; }
        }
        #endregion

        public Configuration RegisterBusinessComponents(params Assembly[] assemblies)
        {
            List<Type> registeredTypes = new List<Type>();
            foreach (Assembly assembly in assemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (!registeredTypes.Contains(type))
                    {
                        //RegisterComponentType(type);
                    }
                }
            }
            return this;
        }

    }
}
