using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TechBeyondThoughts.MessageBus
{
    public class MessageBus : IMessageBus
    {
        private string connectionString = "";   // my connectionstring present on Azure portal
        //that way we are able to publish message through our service bus
        public async Task PublishMessage(object message, string topic_queue_Name)
        {
            await using var client = new ServiceBusClient(connectionString);
            ServiceBusSender sender = client.CreateSender(topic_queue_Name);
            var jsonMwssage = JsonConvert.SerializeObject(message);
            ServiceBusMessage finalMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMwssage))
            {
                CorrelationId = Guid.NewGuid().ToString(),
            };
            await sender.SendMessageAsync(finalMessage);
            client.DisposeAsync();
        }
    }
}
