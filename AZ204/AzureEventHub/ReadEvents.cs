using Azure.Messaging.EventHubs.Consumer;
using System.Text;

namespace AzureEventHub
{
    public class ReadEvents
    {
        /*
         Go to azure event hub
         Create the shared access policy - should select Listen - Copy the connectionsstring
         Go to the consumer group where we create new consumer group.
         Now copy the $Default consumer group.
         */

        private readonly string connectionString = "";
        private readonly string consumerGroup = "$Default";

        public ReadEvents(string connectionString, string consumerGroup)
        {
            this.connectionString = connectionString;
            this.consumerGroup = consumerGroup;
        }

        public ReadEvents()
        { }

        /// <summary>
        /// Print the partitionids in Event Hub
        /// </summary>
        /// <returns></returns>
        public async Task GetPartitionIds()
        {
            EventHubConsumerClient eventHubConsumerClient = new EventHubConsumerClient(consumerGroup, connectionString);
            string[] partitionIds = await eventHubConsumerClient.GetPartitionIdsAsync();
            foreach(var partitionId in partitionIds)
            {
                Console.WriteLine($"Partition Id {0}", partitionId);
            }
        }
        /// <summary>
        /// Read Events from Event Hub
        /// </summary>
        /// <returns></returns>
        public async Task ReadEvent()
        {
            EventHubConsumerClient eventHubConsumerClient = new EventHubConsumerClient(consumerGroup, connectionString);
            var cancellation = new CancellationTokenSource();
            cancellation.CancelAfter(TimeSpan.FromSeconds(300));
           
            await foreach(PartitionEvent partitionEvent in eventHubConsumerClient.ReadEventsAsync(cancellation.Token))
            {
                PrintData(partitionEvent);
            }
        }

        /// <summary>
        /// Read Events from partition and only reads the unread events
        /// </summary>
        /// <returns></returns>
        public async Task ReadEventFromPartition()
        {
            
            EventHubConsumerClient eventHubConsumerClient = new EventHubConsumerClient(consumerGroup, connectionString);
            string partitionId = (await eventHubConsumerClient.GetPartitionIdsAsync()).First();
            var cancellation = new CancellationTokenSource();
            cancellation.CancelAfter(TimeSpan.FromSeconds(300));

            await foreach (PartitionEvent partitionEvent in eventHubConsumerClient.ReadEventsFromPartitionAsync(partitionId, EventPosition.Latest))
            {
                PrintData(partitionEvent);
            }
        }

        private void PrintData(PartitionEvent partitionEvent)
        {
            Console.WriteLine($"Partition ID {partitionEvent.Partition.PartitionId}");
            Console.WriteLine($"Data offset {partitionEvent.Data.Offset}");
            Console.WriteLine($"Sequence Number {partitionEvent.Data.SequenceNumber}");
            Console.WriteLine($"Partition Key {partitionEvent.Data.PartitionKey}");
            Console.WriteLine(Encoding.UTF8.GetString(partitionEvent.Data.EventBody));
        }
    }
}
