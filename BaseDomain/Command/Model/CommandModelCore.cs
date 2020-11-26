using FluentValidation.Results;
using MediatR;
using System;

namespace BaseDomain.Command.Model
{
    /// <summary>
    /// 泛型命令模型抽象基类
    /// </summary>
    /// <typeparam name="TAggregateId">泛型聚合根ID</typeparam>
    public abstract class CommandModelCore<TAggregateId> : IRequest<bool>
    {
        protected CommandModelCore()
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
        /// 验证结果
        /// </summary>
        public ValidationResult ValidationResult { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime Timestamp { get; private set; }

        /// <summary>
        /// 判断是否有效
        /// </summary>
        /// <returns></returns>
        public abstract bool IsValid();
    }
}
