using FluentValidation.Results;
using MediatR;
using System;

namespace CDomain.Command.Model
{
    /// <summary>
    /// 命令模型
    /// </summary>
    public abstract class CommandModel : IRequest<bool>
    {
        protected CommandModel()
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
