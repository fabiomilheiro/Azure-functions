using Azure_functions;
using Azure_functions.Persons;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(StartUp))]
namespace Azure_functions
{
    public class StartUp : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IPersonRepository, PersonRepository>();
        }
    }
}