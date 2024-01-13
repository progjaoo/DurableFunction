using Azure;
using Azure.Storage.Blobs.Models;

public interface IContainerService
{
    Pageable<BlobItem> GetBlobs(string containerName);
}