using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage;

namespace GS1US.Tests.Functions
{
    public static class FunctionsForTest
    {
        [FunctionName("GetTestReport")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]HttpRequestMessage req,
            TraceWriter log)
        {
            var d = req.GetQueryNameValuePairs().ToDictionary(p => p.Key, p => p.Value);
            var hasName = d.TryGetValue("name", out string name);
            var hasExtra = d.TryGetValue("extra", out string extra);

            if (!hasName)
            {
                return req.CreateResponse(HttpStatusCode.BadRequest, "missing app name", "text/plain");
            }

            var cs = Environment.GetEnvironmentVariable("TestBlobStorageConnectionString", EnvironmentVariableTarget.Process);

            if (!CloudStorageAccount.TryParse(cs, out CloudStorageAccount storageAccount))
            {
                return req.CreateResponse(HttpStatusCode.BadRequest, "can't access blob storage", "text/plain");
            }

            string path = $"{name}/{(hasExtra ? extra : "TestResults.html")}";
            var cloudBlobClient = storageAccount.CreateCloudBlobClient();
            var cloudBlobContainer = cloudBlobClient.GetContainerReference("$web");
            var cloudBlob = cloudBlobContainer.GetBlobReference(path);
            cloudBlob.FetchAttributes();
            var stream = cloudBlob.OpenRead();
            var contentType = cloudBlob.Properties.ContentType;

            var res = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(stream)
            };
            res.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
            return res;
        }
    }
}
