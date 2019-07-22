#r "Newtonsoft.Json"

using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

public static async Task<IActionResult> Run(HttpRequest req, Newtonsoft.Json.Linq.JArray messages, ILogger log)
{
    log.LogInformation("C# HTTP trigger function processed a request.");
    List<Message> returnData = new List<Message>();

    foreach (Newtonsoft.Json.Linq.JToken message in messages)
    {
        log.LogInformation(message.ToString());
        Message m = Newtonsoft.Json.JsonConvert.DeserializeObject<Message>(message.ToString());
        if(!m.Hidden)
            returnData.Add(m);
    }

    return (ActionResult)new OkObjectResult(JsonConvert.SerializeObject(returnData));
}

public class Message
{
    public string PartitionKey { get; set; } //The target phone number acts as the partition key
    public string RowKey { get; set; }
    public string SenderPhone { get; set; }
    public string Text { get; set; }
    public bool Hidden { get; set; }
}