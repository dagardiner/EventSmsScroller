#r "Microsoft.WindowsAzure.Storage"
using Microsoft.WindowsAzure.Storage.Table;
using System.Net;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, CloudTable outputTable, TraceWriter log)
{
    var partition = req.RequestUri.Query.Split("&part=")[1].Split("&")[0].Replace("%20", " ");
    var key = req.RequestUri.Query.Split("&id=")[1];
    var item = new Message();
    item.PartitionKey = partition;
    item.RowKey = key;
    item.Hidden = true;
    item.ETag = "*";

    var operation = TableOperation.Merge(item);
    await outputTable.ExecuteAsync(operation);

    return new HttpResponseMessage(HttpStatusCode.NoContent);
}

public class Message: TableEntity
{
    public bool Hidden { get; set; }
}