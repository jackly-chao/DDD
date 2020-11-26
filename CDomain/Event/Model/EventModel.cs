using MediatR;
using System;

namespace CDomain.Event.Model
{
    /// <summary>
    /// 事件模型,继承INotification(意味着拥有中介的发布/订阅模式)
    /// </summary>
    public abstract class EventModel : INotification
    {
        protected EventModel()
        {
            MessageType = GetType().Name;
            Timestamp = DateTime.Now;
        }

        /// <summary>
        /// 聚合根唯一标识
        /// </summary>
        public int AggregateId { get; protected set; }

        /// <summary>
        /// 信息类型
        /// </summary>
        public string MessageType { get; protected set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime Timestamp { get; private set; }
    }
}
