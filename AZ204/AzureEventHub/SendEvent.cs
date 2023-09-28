using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureEventHub
{
    public class SendEvent
    {
        private readonly string connectionString = "";
        private readonly string eventHubName = "apphub";

        /*
         Install Azure.Messaging.EventHub Nugetpackage
         Get connectionstring from Shared Access Policy
         
         */
        public async void SendData(List<Device> devices)
        {
            EventHubProducerClient eventHubProducerClient = new EventHubProducerClient(connectionString, eventHubName);
            EventDataBatch eventDataBatch = await eventHubProducerClient.CreateBatchAsync();
            foreach(Device device in devices)
            {
                EventData eventData = new EventData(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(device)));
                if (!eventDataBatch.TryAdd(eventData))
                    Console.WriteLine("Error has occured adding data to the batch");
            }
            await eventHubProducerClient.SendAsync(eventDataBatch);
            Console.WriteLine("Events are sent");
            await eventHubProducerClient.DisposeAsync();
        }

        /// <summary>
        /// SendEvent Data with Partitionkey
        /// </summary>
        /// <param name="devices"></param>
        public async void SendDataPartition(List<Device> devices)
        {
            EventHubProducerClient eventHubProducerClient = new EventHubProducerClient(connectionString, eventHubName);
            List<EventData> dataList = new List<EventData>();
            foreach(Device device in devices)
            {
                EventData eventData = new EventData(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(device)));
                dataList.Add(eventData);
            }
            await eventHubProducerClient.SendAsync(dataList, new SendEventOptions() { PartitionKey = "D1" });
        }
    }
}
