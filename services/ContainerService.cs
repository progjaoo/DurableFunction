using System;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

public class ContainerService : IContainerService
{
    public Pageable<BlobItem> GetBlobs(string containerName)
    {
        try
        {
            var blobContainerClient = new BlobContainerClient
                (System.Environment.GetEnvironmentVariable("AzureWebJobsStorage"), containerName);

            var result = blobContainerClient.GetBlobs(BlobTraits.None, BlobStates.None);

            return result;
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}