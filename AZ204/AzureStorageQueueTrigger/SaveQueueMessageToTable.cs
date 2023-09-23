using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureStorageQueueTrigger
{
    public class SaveQueueMessageToTable
    {
        [FunctionName("SaveQueueMessageToTable")]
        [return: Table("Order", Connection = "connectionString")]
        public TableOrder Run([QueueTrigger("appqueue", Connection = "connectionString")] OrderData myQueueItem, ILogger log)
        {
            TableOrder tableOrder = new TableOrder();
            tableOrder.PartitionKey = myQueueItem.OrderId.ToString();
            tableOrder.RowKey = myQueueItem.Item.ToString();
            log.LogInformation($"Queue Messages are stored in Table");
            return tableOrder; 
        }
    }
}
