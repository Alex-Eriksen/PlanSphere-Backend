using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Interfaces;

namespace PlanSphere.Core.Clients.FileStorage;

public abstract class AzureBlobStorage(
    ILogger<AzureBlobStorage> logger,
    BlobContainerClient blobContainerClient
) : IFileStorage
{
    private readonly ILogger<AzureBlobStorage> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly BlobContainerClient _blobContainerClient = blobContainerClient ?? throw new ArgumentNullException(nameof(blobContainerClient));

    public async Task<byte[]> GetAsync(string filePath)
    {
        _logger.LogInformation("Retrieving file from file storage of path: {filePath}", filePath);

        var blobClient = _blobContainerClient.GetBlobClient(filePath);
        var response = await blobClient.DownloadAsync();

        using var ms = new MemoryStream();
        var buffer = new byte[16 * 1024];
        int read;

        while ((read = response.Value.Content.Read(buffer, 0, buffer.Length)) > 0)
        {
            ms.Write(buffer, 0, read);
        }

        _logger.LogInformation("File was read successfully from path: {filePath}", filePath);

        return ms.GetBuffer();
    }

    public async Task<string> SaveAsync(string filePath, byte[] fileData, bool overwrite = false)
    {
        _logger.LogInformation("Saving file to path: {filePath}", filePath);
        await _blobContainerClient.CreateIfNotExistsAsync();
        var blobClient = _blobContainerClient.GetBlobClient(filePath);
        await blobClient.UploadAsync(new MemoryStream(fileData, true), overwrite);
        _logger.LogInformation("Saved file to path: {filePath} successfully", filePath);
        return blobClient.Uri.ToString();
    }

    public string GetBlobClientUrl(string filePath)
    {
        var fileUrl = filePath;

        if (!string.IsNullOrEmpty(filePath))
        {
            var blobClient = _blobContainerClient.GetBlobClient(filePath);
            fileUrl = blobClient.Uri.ToString();
        }

        return fileUrl;
    }

    public async Task DeleteAsync(string filePath)
    {
        _logger.LogInformation("Deleting file with path: {filePath}", filePath);
        await _blobContainerClient.CreateIfNotExistsAsync();
        var blobClient = _blobContainerClient.GetBlobClient(filePath);
        await blobClient.DeleteAsync();
        _logger.LogInformation("Deleted file with path: {filePath} successfully", filePath);
    }
}