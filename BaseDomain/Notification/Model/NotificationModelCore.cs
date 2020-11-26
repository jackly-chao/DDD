using MediatR;
using System;

namespace BaseDomain.Notification.Model
{
    /// <summary>
    /// 泛型通知模型基类,继承INotification(意味着拥有中介的发布/订阅模式)
    /// </summary>
    public class NotificationModelCore : INotification
    {
        public NotificationModelCore(string key, string value)
        {
            Id = Guid.NewGuid();
            Key = key;
            Value = value;
        }

        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; private set; }
    }
}
