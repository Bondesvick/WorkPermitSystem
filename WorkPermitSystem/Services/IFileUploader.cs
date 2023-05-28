namespace WorkPermitSystem.Services;

public interface IFileUploader
{
    Task<bool> UploadFileAsync(string fileName, Stream storageStream);

    Task<MemoryStream> GetFileByKeyAsync(string key);
}