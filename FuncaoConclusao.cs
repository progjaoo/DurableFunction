using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Data.Tables;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

public class FuncaoConclusao
{
    [FunctionName(nameof(FuncaoConclusao))]
    public async Task Run([ActivityTrigger] IDurableActivityContext context)
    {
        var info = context.GetInput<List<ContainerResponseResult>>();

        TableClient tableClient = new TableClient(System.Environment.GetEnvironmentVariable("AzureWebJobsStorage"), "nomeArquivosAreas");

        foreach(var nomes in info)
        {
            foreach(var item in nomes.NomeDocumentos)
            {
                TableEntity tableEntity = new TableEntity()
                {
                    {"PartitionKey", Guid.NewGuid().ToString()},
                    {"RowKey", Guid.NewGuid().ToString()},
                    {"NomeArquivo", item}
                };

                tableClient.AddEntity(tableEntity);
            }
        }
    }
}