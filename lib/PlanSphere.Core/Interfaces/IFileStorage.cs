namespace PlanSphere.Core.Interfaces;

public interface IFileStorage
{
    /// <summary>
    /// Uploads a file to Azure Blob Storage and returns the file's URI.
    /// </summary>
    /// <param name="filePath">The path to save the file.</param>
    /// <param name="fileData">The file content to save as a byte array.</param>
    /// <param name="overwrite">If true, overwrites any existing file at the path. Defaults to false.</param>
    /// <returns>The URI of the saved file in Blob Storage.</returns>
    public Task<string> SaveAsync(string filePath, byte[] fileData, bool overwrite = false);
    
    /// <summary>
    /// Retrieves a file from Azure Blob Storage as a byte array.
    /// </summary>
    /// <param name="filePath">The path of the file to retrieve.</param>
    /// <returns>A byte array containing the file's content.</returns>
    public Task<byte[]> GetAsync(string filePath);
    
    /// <summary>
    /// Returns the URL of the blob client for a specified file path.
    /// </summary>
    /// <param name="filePath">The path of the file to get the URL for.</param>
    /// <returns>The URL of the blob file as a string.</returns>
    public string GetBlobClientUrl(string filePath);
    
    /// <summary>
    /// Deletes a file from Azure Blob Storage.
    /// </summary>
    /// <param name="filePath">The path of the file to delete.</param>
    public Task DeleteAsync(string filePath);
}