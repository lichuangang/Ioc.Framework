﻿using Autofac.Extras.DynamicProxy;
using Li.Framework.Core.Attributes;
using Li.Framework.Core.Ioc;
using Li.Framework.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestProject1.Entity;

namespace UnitTestProject1.Repositorys
{
    /* ==============================================================================
     * 描述：MessageHstRsp
     * 创建人：李传刚 2017/7/19 19:08:55
     * ==============================================================================
     */
    [Repository]
    [Intercept(typeof(CacheInterceptor))]
    public class MessageHstRsp : BaseRepository<MessageHst, string>
    {
        [Cache(30)]
        public virtual MessageHst GetByIdCache(string id)
        {
            return GetById(id);
        }
    }
}
