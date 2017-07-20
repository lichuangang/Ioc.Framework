using Li.Framework.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestProject1.Entity;
using UnitTestProject1.Repositorys;

namespace UnitTestProject1.Services
{
    /* ==============================================================================
     * 描述：MessageService
     * 创建人：李传刚 2017/7/20 9:22:49
     * ==============================================================================
     */
    [Service]
    public class MessageService
    {
        private readonly MessageHstRsp _messageHstRep;

        public MessageService(MessageHstRsp messageHstRep)
        {
            _messageHstRep = messageHstRep;
        }

        public MessageHst GetById(string id)
        {
            return _messageHstRep.GetById(id);
        }
    }
}
