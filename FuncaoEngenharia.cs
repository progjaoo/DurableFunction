using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

public class FuncaoEngenharia: BlobServiceBase
{
    public FuncaoEngenharia(IContainerService containerService)
        :base(containerService, Global.CONTAINER_ENGENHARIA)
        {}

    [FunctionName(nameof(FuncaoEngenharia))]
    public Task<ContainerResponseResult> Run([ActivityTrigger] string name) =>
        Task.FromResult<ContainerResponseResult>(Execute());
}