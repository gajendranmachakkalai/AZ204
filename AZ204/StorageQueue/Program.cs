/*
    Create Queue in Storage account - appqueue
    Install the Azure.Storage.Queue nugetpakage in project
    
 */

using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Newtonsoft.Json;
using StorageQueue;

var storageconnection = "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;";
var queuename = "appqueue";
//Send Message
SendMessage(new OrderData()
{
    Item = "Dell laptop",
    Quantity = 5,
    OrderId = 1
});
SendMessage(new OrderData()
{
    Item = "lenovo laptop",
    Quantity = 10,
    OrderId = 2
});
PeekMessage();
ReadAndDeleteMessages();

void SendMessage(OrderData order)
{
    QueueClient client = new QueueClient(storageconnection, queuename);
    var jsonData = JsonConvert.SerializeObject(order);
    var bytes = System.Text.Encoding.UTF8.GetBytes(jsonData);   
    var message = System.Convert.ToBase64String(bytes);
    if (client.Exists())
    {
        client.SendMessage(message);
        Console.WriteLine("Send Message to Queue");
    }
}

void PeekMessage()
{
    QueueClient client = new QueueClient(storageconnection, queuename);
    int maxMessage = 10;
    PeekedMessage[] messages = client.PeekMessages(maxMessage);
    foreach (var item in messages)
    {
        var order = JsonConvert.DeserializeObject<OrderData>(Base64Decode(item.Body.ToString()));
        Console.WriteLine($"OrderId :{order.OrderId} OrderItem: {order.Item}  OrderQuantity: {order.Quantity}");
    }
}


void ReadMessages()
{
    QueueClient client = new QueueClient(storageconnection, queuename);
    int maxMessage = 10;
    QueueMessage[] messages = client.ReceiveMessages(maxMessage);
    foreach(var item in messages)
    {
        var order = JsonConvert.DeserializeObject<OrderData>(Base64Decode(item.Body.ToString()));
        Console.WriteLine($"OrderId :{order.OrderId} OrderItem: {order.Item}  OrderQuantity: {order.Quantity}");
    }
}

void ReadAndDeleteMessages()
{
    QueueClient client = new QueueClient(storageconnection, queuename);
    int maxMessage = GetQueueLength();
    Console.WriteLine($"Total messages {maxMessage}");
    QueueMessage[] messages = client.ReceiveMessages(maxMessage);
    foreach (var item in messages)
    {
        var order = JsonConvert.DeserializeObject<OrderData>(Base64Decode(item.Body.ToString()));
        Console.WriteLine($"OrderId :{order.OrderId} OrderItem: {order.Item}  OrderQuantity: {order.Quantity}");
        //Delete Message
        client.DeleteMessage( messageId: item.MessageId, popReceipt: item.PopReceipt);
        Console.WriteLine($"Deleted the Message {item.MessageId}");
    }
}


int GetQueueLength()
{
    QueueClient client = new QueueClient(storageconnection, queuename);
    if (client.Exists())
    {
        QueueProperties queueProperties = client.GetProperties();
        return queueProperties.ApproximateMessagesCount;
    }
    return 0;
}

static string Base64Decode(string base64EncodedData)
{
    var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
    return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
}