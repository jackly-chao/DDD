using CDomain.Event.Model;
using Newtonsoft.Json;

namespace CDomain.EventSource.Model
{
    /// <summary>
    /// 事件回溯模型
    /// </summary>
    public class EventSourceModel : EventModel
    {
        public EventSourceModel(EventModel eventModel, string user)
        {
            AggregateId = eventModel.AggregateId;
            MessageType = eventModel.MessageType;
            Data = JsonConvert.SerializeObject(eventModel);
            User = user;
        }

        /// <summary>
        /// 唯一标识
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// 存储的数据
        /// </summary>
        public string Data { get; private set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string User { get; private set; }
    }
}
