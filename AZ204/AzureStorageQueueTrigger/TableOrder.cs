﻿namespace AzureStorageQueueTrigger
{
    public class TableOrder
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
    }
}
