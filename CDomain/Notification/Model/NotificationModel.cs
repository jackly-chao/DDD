using MediatR;
using System;

namespace CDomain.Notification.Model
{
    /// <summary>
    /// 通知模型
    /// </summary>
    public class NotificationModel : INotification
    {
        public NotificationModel(string key, string value)
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
