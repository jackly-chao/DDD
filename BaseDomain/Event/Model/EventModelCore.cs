using MediatR;
using System;

namespace BaseDomain.Event.Model
{
    /// <summary>
    /// 泛型事件模型抽象基类,继承INotification(意味着拥有中介的发布/订阅模式)
    /// </summary>
    /// <typeparam name="TAggregateId">泛型聚合根ID</typeparam>
    public abstract class EventModelCore<TAggregateId> : INotification
    {
        protected EventModelCore()
        {
            MessageType = GetType().Name;
            Timestamp = DateTime.Now;
        }

        /// <summary>
        /// 聚合根唯一标识
        /// </summary>
        public TAggregateId AggregateId { get; protected set; }

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
