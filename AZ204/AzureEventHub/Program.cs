// See https://aka.ms/new-console-template for more information
using AzureEventHub;

Console.WriteLine("Azure Event Hub");
SendEvent sendEvent = new SendEvent();
List<Device> listDevices = new List<Device>()
{
    new Device() { deviceId = "D1", temperature = 40.0f},
    new Device() { deviceId = "D2", temperature = 35.0f},
    new Device() { deviceId = "D3", temperature = 39.0f},
    new Device() { deviceId = "D1", temperature = 31.0f},
    new Device() { deviceId = "D2", temperature = 30.0f},
    new Device() { deviceId = "D3", temperature = 37.0f}
};
//sendEvent.SendData(listDevices);
await sendEvent.SendDataPartition(listDevices.Where(x => x.deviceId == "D1").ToList());
//var readEvent = new ReadEvents();
//Console.WriteLine("Azure Event Hub - List PartitionIds");
//await readEvent.GetPartitionIds();
////Console.WriteLine("Azure Event Hub - Read Events");
////await readEvent.ReadEvent();
//Console.WriteLine("Azure Event Hub - Read Events which was not read before");
//await readEvent.ReadEventFromPartition();
//EventHubProcessor eventHubProcessor = new EventHubProcessor();
//await eventHubProcessor.InitializeAsync();
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
 Capture feature
 *  When the feature turn on, Azure Event Hub stores the events in the storage account. 
 *  Azure portal -> eventhubnamespace -> eventhub -> capture (blade) -> select the container in the blob storage ( configure time interval and maximum size in MB to send data)
 *  It will store the data in blob storage as "avro" file format.
 *  
 SQL digonstics to Azure Event Hub
 * appdb -> Digonostics settings -> Add Digonostics settings -> select Metrics data (Basics) -> select destination as Azure Event Hub.
 * Increase the throughput up to 4 in Azure Event Hub Namespace
 * Create new Event Hub (sqleventhub) with partition (4) 
 * Select the new event hub as destination in the Dignostics settings.
 * 
 * 
 * Azure Service Bus vs Azure Event Hub
 * 
 * ASB - It is message broker. it is used to communicate between different service
 * AEH - Multiple devices, apps can send the events to Event Hub and consumer group can consume it else can store it to blob.
 */








