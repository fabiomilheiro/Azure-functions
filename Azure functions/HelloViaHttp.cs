using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Azure_functions
{
    public static class HelloViaHttp
    {
        [FunctionName("HelloViaHttp")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            if (string.IsNullOrEmpty(name))
            {
                return new OkObjectResult(
                    "This HTTP triggered function executed successfully. Pass a name in the query string or" +
                    " in the request body for a personalized response.");
            }

            return new OkObjectResult($"Hello, {name}. This HTTP triggered function executed successfully.");
        }
    }
}
