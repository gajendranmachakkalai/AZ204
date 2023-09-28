using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Processor;
using Azure.Storage.Blobs;

namespace AzureEventHub
{
    public class EventHubProcessor
    {
        /*
            Event Hub Processor internally tracks the event checkpoint in the blob storage
            Nuget Package
                1. Azure.Messaging.EventHubs.Processor
                2. Azure.Storage.Blobs
            connectionString - should be Shared Access Policy with listen 
         */
        /// <summary>
        /// should be Shared Access Policy with listen  in event hub
        /// </summary>
        string connectionString = "";
        /// <summary>
        /// consumer group in the event hub
        /// </summary>
        string consumerGroup = "apphub";
        /// <summary>
        /// Blob storage connectionstring
        /// </summary>
        string blobConnectionString = "blobconnectionstring";
        string containerName = "checkpointcontainer";

        public async Task InitializeAsync()
        {
            BlobContainerClient blobContainerClient = new BlobContainerClient(blobConnectionString, containerName);
            EventProcessorClient eventProcessorClient = new EventProcessorClient(blobContainerClient, consumerGroup, connectionString);
            eventProcessorClient.ProcessEventAsync += ProcessEvent;
            eventProcessorClient.ProcessErrorAsync += ErrorHandler;
            await eventProcessorClient.StartProcessingAsync();
            Console.ReadKey();
            await eventProcessorClient.StopProcessingAsync();
        }

        public async Task ProcessEvent(ProcessEventArgs processEvent)
        {
            Console.WriteLine(processEvent.Data.EventBody.ToString());
        }

        public static Task ErrorHandler(ProcessErrorEventArgs processErrorEventArgs)
        {
            Console.WriteLine(processErrorEventArgs.Exception.Message);
            return Task.CompletedTask;
        }
    }
}
