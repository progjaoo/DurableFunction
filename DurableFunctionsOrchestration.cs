using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class DurableFunctionsOrchestration
    {
        [FunctionName("DurableFunctionsOrchestration")]
        public async Task RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var tasks = new List<Task<ContainerResponseResult>>();

            tasks.Add(context.CallActivityAsync<ContainerResponseResult>(nameof(FuncaoComercial), "FuncaoComercial"));
            tasks.Add(context.CallActivityAsync<ContainerResponseResult>(nameof(FuncaoEngenharia), "FuncaoEngenharia"));
            tasks.Add(context.CallActivityAsync<ContainerResponseResult>(nameof(FuncaoTecnologia), "FuncaoTecnologia"));

            await Task.WhenAll(tasks);

            var resultFromFuncrions = tasks.Select(x => x.Result).ToList();
            
            await context.CallActivityAsync<List<string>>(nameof(FuncaoConclusao), resultFromFuncrions);
        }

        [FunctionName("StartDurableFunctions")]
        public async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync("DurableFunctionsOrchestration", null);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}