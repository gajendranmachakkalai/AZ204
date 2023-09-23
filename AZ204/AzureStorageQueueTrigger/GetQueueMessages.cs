using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureStorageQueueTrigger
{
    public class GetQueueMessages
    {
        [FunctionName("GetMessages")]
        public void Run([QueueTrigger("appqueue", Connection = "connectionString")] OrderData myQueueItem, ILogger log)
        {
            log.LogInformation($"OrderID: {myQueueItem.OrderId}");
            log.LogInformation($"Item: {myQueueItem.Item}");
            log.LogInformation($"Quantity: {myQueueItem.Quantity}");
        }
    }
}
