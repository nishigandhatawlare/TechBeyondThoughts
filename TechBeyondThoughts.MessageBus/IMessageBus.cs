using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechBeyondThoughts.MessageBus
{
    public interface IMessageBus
    {
        //name of topic and queue must be unique in service bus //Azure
        Task PublishMessage(object message, string topic_queue_Name);

    }
}
