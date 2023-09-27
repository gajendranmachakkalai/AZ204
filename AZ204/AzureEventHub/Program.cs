// See https://aka.ms/new-console-template for more information
using AzureEventHub;

Console.WriteLine("Azure Event Hub");
SendEvent sendEvent = new SendEvent();
List<Device> listDevices = new List<Device>()
{
    new Device() { deviceId = "D1", temperature = 40.0f}
};
sendEvent.SendData(listDevices);
var readEvent = new ReadEvents();
Console.WriteLine("Azure Event Hub - List PartitionIds");
await readEvent.GetPartitionIds();
Console.WriteLine("Azure Event Hub - Read Events");
await readEvent.ReadEvent();
Console.WriteLine("Azure Event Hub - Read Events which was not read before");
await readEvent.ReadEventFromPartition();

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
    
 */