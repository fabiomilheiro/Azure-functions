using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Azure_functions
{
    public static class ExecuteContinuously
    {
        [FunctionName("ExecuteContinuously")]
        public static void Run([TimerTrigger("1-20 * * * * *", RunOnStartup = true, UseMonitor = true)]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
