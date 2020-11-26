using BaseDomain.Event.Model;
using Newtonsoft.Json;

namespace BaseDomain.EventSource.Model
{
    /// <summary>
    /// 泛型事件回溯模型基类
    /// </summary>
    /// <typeparam name="TId">泛型ID</typeparam>
    /// <typeparam name="TEventModelAggregateId">泛型事件模型聚合根ID</typeparam>
    public class EventSourceModelCore<TId, TEventModelAggregateId> : EventModelCore<TEventModelAggregateId>
    {
        public EventSourceModelCore(EventModelCore<TEventModelAggregateId> eventModel, string @operator)
        {
            //if (typeof(TId).Name == "Guid")
            //{
            //    Id = Guid.NewGuid();
            //}
            AggregateId = eventModel.AggregateId;
            MessageType = eventModel.MessageType;
            Data = JsonConvert.SerializeObject(eventModel);
            Operator = @operator;
        }

        //protected EventSourceModel()
        //{

        //}

        /// <summary>
        /// 唯一标识
        /// </summary>
        public TId Id { get; private set; }

        /// <summary>
        /// 存储的数据
        /// </summary>
        public string Data { get; private set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator { get; private set; }
    }
}
