#r "System.Runtime"
#r "Newtonsoft.Json"

using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

public static async Task<ActionResult> Run(HttpRequestMessage req, ICollector<Message> messages, ILogger log)
{
    log.LogInformation("C# HTTP trigger function processed a request.");

    var data = await req.Content.ReadAsStringAsync();

    log.LogInformation("Received data " + data);

    var formValues = data.Split('&')
        .Select(value => value.Split('='))
        .ToDictionary(pair => Uri.UnescapeDataString(pair[0]).Replace("+", " "), 
                      pair => Uri.UnescapeDataString(pair[1]).Replace("+", " "));

    log.LogInformation("Writing to table message with body " + formValues["Body"]);

    messages.Add(new Message() {
        PartitionKey = formValues["To"],
        RowKey = Guid.NewGuid().ToString(),
        SenderPhone = formValues["From"],
        Text = formValues["Body"],
        Hidden = false
    });
    
    string responseMessage = "Got it - thanks!";

    log.LogInformation("Sending confirmation: " + responseMessage);

    return (ActionResult)new OkObjectResult(responseMessage);
}

public class Message
{
    public string PartitionKey { get; set; } //The target phone number acts as the partition key
    public string RowKey { get; set; }
    public string SenderPhone { get; set; }
    public string Text { get; set; }
    public bool Hidden { get; set; }
}