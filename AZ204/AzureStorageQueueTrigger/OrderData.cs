using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorageQueueTrigger
{
    public class OrderData
    {
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public string Item { get; set; }
    }
}
