using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WorkPermitSystem.Services;

public class S3FileUploader : IFileUploader
{
    private readonly IConfiguration _configuration;
    private readonly AmazonS3Client _client;

    public S3FileUploader(IConfiguration configuration)
    {
        _configuration = configuration;

        string awsAccessKeyId = "";
        string awsSecretAccessKey = "";
        _client = new AmazonS3Client(awsAccessKeyId, awsSecretAccessKey);
    }

    public async Task<bool> UploadFileAsync(string fileName, Stream storageStream)
    {
        if (string.IsNullOrEmpty(fileName)) throw new ArgumentException("File name must be specified.");

        var bucketName = _configuration.GetValue<string>("ImageBucket");

        if (storageStream.Length > 0)
            if (storageStream.CanSeek)
                storageStream.Seek(0, SeekOrigin.Begin);

        var request = new PutObjectRequest
        {
            AutoCloseStream = true,
            BucketName = bucketName,
            InputStream = storageStream,
            Key = fileName
        };
        var response = await _client.PutObjectAsync(request).ConfigureAwait(false);
        return response.HttpStatusCode == HttpStatusCode.OK;
    }

    public async Task<MemoryStream> GetFileByKeyAsync(string key)
    {
        var bucketName = _configuration.GetValue<string>("ImageBucket");
        MemoryStream ms = null;
        var s3Object = await _client.GetObjectAsync(bucketName, key);

        if (s3Object.HttpStatusCode == HttpStatusCode.OK)
        {
            using (ms = new MemoryStream())
            {
                await s3Object.ResponseStream.CopyToAsync(ms);
            }
        }

        if (ms is null || ms.ToArray().Length < 1)
            throw new FileNotFoundException(string.Format("The document '{0} is not found", key));

        return ms;
    }

    public async Task<dynamic> GetAllFilesAsync()
    {
        var bucketName = _configuration.GetValue<string>("ImageBucket");

        var request = new ListObjectsV2Request()
        {
            BucketName = bucketName,
        };
        var response = await _client.ListObjectsV2Async(request);

        var s3Objects = response.S3Objects.Select(s =>
        {
            var urlRequest = new GetPreSignedUrlRequest()
            {
                BucketName = bucketName,
                Key = s.Key,
                Expires = DateTime.UtcNow.AddMinutes(1)
            };
            return new S3ObjectDto()
            {
                Name = s.Key.ToString(),
                PresignedUrl = _client.GetPreSignedURL(urlRequest),
            };
        });

        return s3Objects;
    }

    public class S3ObjectDto
    {
        public string? Name { get; set; }
        public string? PresignedUrl { get; set; }
    }
}