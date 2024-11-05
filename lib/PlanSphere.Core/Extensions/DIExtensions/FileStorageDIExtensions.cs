using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlanSphere.Core.Clients.FileStorage;
using PlanSphere.Core.Interfaces;

namespace PlanSphere.Core.Extensions.DIExtensions;

public static class FileStorageDIExtensions
{
    public static IHostApplicationBuilder AddFileStorage(this IHostApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("AzureBlobStorage");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Azure Blob Storage connection string is not configured.");
        }

        builder.Services.AddSingleton(new BlobServiceClient(connectionString));
        
        builder.Services.AddSingleton<IAzureBlobStorageFactory, AzureBlobStorageFactory>();
        builder.Services.AddSingleton<IPublicAzureBlobStorage>(sp => 
            sp.GetRequiredService<IAzureBlobStorageFactory>().CreatePublicStorage());
        builder.Services.AddSingleton<IPrivateAzureBlobStorage>(sp => 
            sp.GetRequiredService<IAzureBlobStorageFactory>().CreatePrivateStorage());
        
        builder.Services.AddSingleton<IFileStorage>(sp => 
            sp.GetRequiredService<IPublicAzureBlobStorage>());
        
        builder.Services.AddSingleton<IFileStorage>(sp => 
            sp.GetRequiredService<IPrivateAzureBlobStorage>());

        return builder;
    }
}