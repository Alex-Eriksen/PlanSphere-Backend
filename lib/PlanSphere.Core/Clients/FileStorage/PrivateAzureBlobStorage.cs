using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;

namespace PlanSphere.Core.Clients.FileStorage;

public class PrivateAzureBlobStorage(ILogger<AzureBlobStorage> logger, BlobContainerClient blobContainerClient) 
    : AzureBlobStorage(logger, blobContainerClient), IPrivateAzureBlobStorage;