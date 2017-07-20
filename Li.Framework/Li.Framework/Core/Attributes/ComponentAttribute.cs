using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Li.Framework.Core.Attributes
{
    /* ==============================================================================
     * 描述：Component Repository Service
     * 创建人：李传刚 2017/7/19 19:55:12
     * ==============================================================================
     */
    public abstract class SelfAttribute : Attribute
    {
        public virtual LifeStyle LifeStyle { get; set; }
    }

    public class ComponentAttribute : SelfAttribute
    {
        public ComponentAttribute() : this(LifeStyle.Transient) { }

        public ComponentAttribute(LifeStyle lifeStyle)
        {
            LifeStyle = lifeStyle;
        }

        public override LifeStyle LifeStyle { get; set; }
    }

    public class RepositoryAttribute : SelfAttribute
    {
        public RepositoryAttribute() : this(LifeStyle.Transient) { }

        public RepositoryAttribute(LifeStyle lifeStyle)
        {
            LifeStyle = lifeStyle;
        }

        public override LifeStyle LifeStyle { get; set; }
    }

    public class ServiceAttribute : SelfAttribute
    {
        public ServiceAttribute() : this(LifeStyle.Transient) { }

        public ServiceAttribute(LifeStyle lifeStyle)
        {
            LifeStyle = lifeStyle;
        }

        public override LifeStyle LifeStyle { get; set; }
    }

    public enum LifeStyle
    {
        Transient,
        Singleton
    }

    /// <summary>
    /// 分层
    /// </summary>
    public enum Layout
    {
        /// <summary>
        /// 仓储层 ---- 与数据库打交道
        /// </summary>
        Repository = 1,
        /// <summary>
        /// 服务层 ---- 调用各个外部接口 组装数据 事务组合
        /// </summary>
        Service = 2,
        /// <summary>
        /// 公共组件 ---- 封装好的直接可用公共逻辑
        /// </summary>
        Component = 4
    }
}
