// See https://aka.ms/new-console-template for more information
using AzureEventHub;

Console.WriteLine("Azure Event Hub");
SendEvent sendEvent = new SendEvent();
List<Device> listDevices = new List<Device>()
{
    new Device() { deviceId = "D1", temperature = 40.0f}
};
sendEvent.SendData(listDevices);
sendEvent.SendDataPartition(listDevices);
var readEvent = new ReadEvents();
Console.WriteLine("Azure Event Hub - List PartitionIds");
await readEvent.GetPartitionIds();
Console.WriteLine("Azure Event Hub - Read Events");
await readEvent.ReadEvent();
Console.WriteLine("Azure Event Hub - Read Events which was not read before");
await readEvent.ReadEventFromPartition();
EventHubProcessor eventHubProcessor = new EventHubProcessor();
await eventHubProcessor.InitializeAsync();
/*
 *  Consumer app needs to keep on running to process events in real time from the Event Hub.
 *  
 *  After consuming the events do the events get deleted ? - No it wont be deleted because azure event hubs serves a different purpose.
 *  May be another type of consumer needs to ready the events again for another requirement.
 *  
 *  Does that mean azure event hubs will keep the messages indefinitely ? No, Default retention period of the message is 1 day and maximum is 7 days
 *  and it means messages are not treated as permenant data store.
 *  
 *  Do you notice that after running the consumer program again, it is reading all of the events again from the beginning.
 *  There are different ways consumer program can mark the last read message. when it restarts , it will resume from there.
    
 Event Hub Scale
 *  It helps to configure the throughput of the event hub. 
 *  Throughput Capacity ( for 1 throughput)
 *  Ingress (Input traffic) - Upto 1 MB per second or 1000 events per second
 *  Engress (Output traffic) - Upto 2 MB per second or 4096 events per second
 *  
 *  Scale -> Throughput units (Adjust the value based on demand)
 *  Aufo Inflate - Auto Scale
 *  You might start getting the ServerBusyExceptions when the ingress traffic goes beyond the limit
 *  
 Consumer Group
 *  Able to create multiple consumer group.
 *  
 Partitions
 *  You cannot change the partition once the hub is created. except for the dedicated cluster and premium tier offering
 *  Recommended throughput of 1MB per partition
 *  Able to customize which property in data can be the partition key.
 *  Azure Event Hub will hash the value and map the events to the relevant partition.
 *  Recommendation is to have one receiver per partition. It may go up to 5 concurrent readers per partition per consumer group. But you have to be careful no to duplicate
 *  the process of reading the same message.
 
 *  
 */