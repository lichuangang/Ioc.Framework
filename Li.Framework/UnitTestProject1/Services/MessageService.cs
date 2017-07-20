using Li.Framework.Core.Attributes;
using Li.Framework.Core.Ioc;
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
    public class MessageService : IMessageService, ITransaction
    {
        private readonly MessageHstRsp _messageHstRep;
        private readonly MessageRsp _messageRep;

        public MessageService(MessageHstRsp messageHstRep, MessageRsp messageRep)
        {
            _messageHstRep = messageHstRep;
            _messageRep = messageRep;
        }

        public MessageHst GetById(string id)
        {
            return _messageHstRep.GetById(id);
        }

        [TransactionHandler]
        public void Tansation()
        {
            Message msg = new Message()
            {
                Id = Guid.NewGuid().ToString(),
                Tag = "test",
                Timestamp = DateTime.Now.Ticks,
                Topic = "test",
                Type = "2"
            };

            MessageHst hst = new MessageHst()
            {
                Id = Guid.NewGuid().ToString(),
                Tag = "test",
                Timestamp = DateTime.Now.Ticks,
                Topic = "test",
                Type = "2"
            };

            _messageRep.Insert(msg);
            _messageHstRep.Insert(hst);
            //_messageHstRep.Insert(hst);
        }

    }
}
