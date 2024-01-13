using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

public class FuncaoTecnologia: BlobServiceBase
{
    public FuncaoTecnologia(IContainerService containerService)
        :base(containerService, Global.CONTAINER_TECNOLOGIA)
        {}

    [FunctionName(nameof(FuncaoTecnologia))]
    public Task<ContainerResponseResult> Run([ActivityTrigger] string name) =>
        Task.FromResult<ContainerResponseResult>(Execute());
}