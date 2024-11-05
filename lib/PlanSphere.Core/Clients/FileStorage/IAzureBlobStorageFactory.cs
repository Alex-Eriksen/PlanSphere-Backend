namespace PlanSphere.Core.Clients.FileStorage;

public interface IAzureBlobStorageFactory
{
    IPublicAzureBlobStorage CreatePublicStorage();
    IPrivateAzureBlobStorage CreatePrivateStorage();
}