using System;
using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Azure_functions
{
    public static class HandleNewOrder
    {
        [FunctionName("HandleNewOrder")]
        public static void Run([CosmosDBTrigger(
            databaseName: "Sample",
            collectionName: "Orders",
            ConnectionStringSetting = "CosmosDbConnectionString",
            LeaseCollectionName = "OrderLeases")]IReadOnlyList<Document> input, ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                log.LogInformation("Documents modified " + input.Count);
                log.LogInformation("First document Id " + input[0].Id);
            }
        }
    }
}
