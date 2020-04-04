using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Azure_functions.Persons;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Azure_functions
{
    public class GreetPersonViaHttp
    {
        private readonly IPersonRepository personRepository;

        public GreetPersonViaHttp(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        [FunctionName("Greet")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            int.TryParse(req.Query["id"], out var personId);

            if (personId == 0)
            {
                return new ObjectResult("Identify yourself!")
                {
                    StatusCode = (int)HttpStatusCode.Forbidden
                };
            }

            var person = this.personRepository.GetPerson(personId);

            if (person == null)
            {
                return new NotFoundObjectResult("I don't know you!");
            }

            return new OkObjectResult($"Hello, {person.Name}!");
        }
    }
}
