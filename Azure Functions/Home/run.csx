using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.IO;

public static HttpResponseMessage Run(HttpRequestMessage req, TraceWriter log)
{
    string fileSource = @"d:\home\site\wwwroot\Home\Home.html";
    if(req.RequestUri.Query.Contains("admin"))
        fileSource = @"d:\home\site\wwwroot\Home\admin.html";

    var response = new HttpResponseMessage(HttpStatusCode.OK);
    var stream = new FileStream(fileSource, FileMode.Open);
    response.Content = new StreamContent(stream);
    response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
    return response;
}