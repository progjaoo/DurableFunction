using System;

public class BlobServiceBase
{
    private readonly IContainerService _containerService;
    private string ContainerName { get; set; }

    public BlobServiceBase(IContainerService containerService, string containerName)
    {
        ContainerName = containerName;
        _containerService = containerService ?? throw new ArgumentNullException(nameof(BlobServiceBase));
    }

    protected ContainerResponseResult Execute()
    {
        var blobResult = _containerService.GetBlobs(ContainerName);

        var result = new ContainerResponseResult();

        foreach(var blob in blobResult)
        {
            result.NomeDocumentos.Add(blob.Name);
        }

        return result;
    }
}