using Azure.Storage.Blobs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Constants;

namespace PlanSphere.Core.Clients.FileStorage;

public class AzureBlobStorageFactory(
    ILogger<AzureBlobStorageFactory> logger,
    BlobServiceClient blobServiceClient,
    IServiceProvider serviceProvider
) : IAzureBlobStorageFactory
{
    private readonly ILogger<AzureBlobStorageFactory> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    private readonly BlobServiceClient _blobServiceClient = blobServiceClient ?? throw new ArgumentNullException(nameof(blobServiceClient));
    private readonly IServiceProvider _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    
    public IPublicAzureBlobStorage CreatePublicStorage()
    {
        _logger.LogInformation("Creating a public Azure Blob Storage instance.");

        var containerClient = _blobServiceClient.GetBlobContainerClient(FileStorageConstants.PublicContainerName);
        return new PublicAzureBlobStorage(
            _serviceProvider.GetRequiredService<ILogger<PublicAzureBlobStorage>>(), 
            containerClient);
    }
}