using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Li.Framework.Common
{
    /* ==============================================================================
     * 描述：ObjectCont
     * 创建人：李传刚 2017/7/17 16:02:34
     * ==============================================================================
     */
    public class ObjectContainer
    {
        static ContainerBuilder buider = SingleInstance<ContainerBuilder>.Instance;

        public static IContainer Instance
        {
            get
            {
                return buider.Build();
            }
        }
    }
}
