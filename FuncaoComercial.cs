using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

public class FuncaoComercial: BlobServiceBase
{
    public FuncaoComercial(IContainerService containerService)
        :base(containerService, Global.CONTAINER_COMERCIAL)
        {}

    [FunctionName(nameof(FuncaoComercial))]
    public Task<ContainerResponseResult> Run([ActivityTrigger] string name) =>
        Task.FromResult<ContainerResponseResult>(Execute());
}