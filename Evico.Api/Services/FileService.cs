using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Minio;
using Enum = System.Enum;

namespace Evico.Api.Services;

public class FileService
{
    private const long MaxFileLenght = 10 * 1024 * 1024;
    private readonly MinioBucketsConfiguration _bucketsConfiguration;
    private readonly MinioClient _minioClient;

    public FileService(MinioClient minioClient, IOptions<MinioBucketsConfiguration> bucketsConfiguration)
    {
        _minioClient = minioClient;
        _bucketsConfiguration = bucketsConfiguration.Value;
    }

    public async Task<Result> UploadFileFromUri(Uri inputFileUri, MinioBucketNames bucket, string internalId)
    {
        try
        {
            var httpClient = new HttpClient();
            var responseAsync = await httpClient.GetAsync(inputFileUri);

            if (!responseAsync.IsSuccessStatusCode)
                return Result.Fail(new Error("Unsuccessful status code"));

            var streamData = await responseAsync.Content.ReadAsStreamAsync();
            var contentType = responseAsync.Content.Headers.ContentType?.MediaType;
            var contentLength = responseAsync.Content.Headers.ContentLength;

            if (string.IsNullOrEmpty(contentType))
                return Result.Fail(new Error("Content type is not specified"));

            if (!contentLength.HasValue)
                return Result.Fail(new Error("Content lenght is not specified"));

            return await AddFile(streamData, contentType, contentLength.Value, bucket, internalId);
        }
        catch (Exception exception)
        {
            return Result.Fail(new Error("Unexpected exception")
                .CausedBy(exception));
        }
    }

    public async Task<Result> UploadFile(IFormFile inputFile, MinioBucketNames bucket, string internalId)
    {
        return await AddFile(inputFile.OpenReadStream(),
            inputFile.ContentType, inputFile.Length, bucket, internalId);
    }

    public async Task<Result<FileStreamResult>> DownloadFile(MinioBucketNames bucket, string internalId)
    {
        var bucketValidationResult = ValidateBucketsConfiguration();
        if (bucketValidationResult.IsFailed)
            return Result.Fail(new Error("Error when Validate Buckets Configuration")
                .CausedBy(bucketValidationResult.Errors));

        var bucketConfiguration = _bucketsConfiguration.GetBucket(bucket);
        var checkBucketResult = await CheckBucket(bucketConfiguration);
        if (checkBucketResult.IsFailed)
            return Result.Fail(new Error($"Error when check Bucket {bucketConfiguration.NameString}")
                .CausedBy(checkBucketResult.Errors));


        return await Result.Try(async () =>
        {
            var fileStream = new MemoryStream();

            var objectStat = await _minioClient.GetObjectAsync(new GetObjectArgs()
                .WithObject(internalId)
                .WithBucket(bucketConfiguration.NameString)
                .WithCallbackStream(stream => { stream.CopyTo(fileStream); })
            );

            if (fileStream == null)
                throw new InvalidOperationException("Failed to get a stream");

            fileStream.Position = 0;
            return new FileStreamResult(fileStream, objectStat.ContentType)
            {
                FileDownloadName = objectStat.ObjectName,
                LastModified = objectStat.LastModified
            };
        });
    }

    public async Task<Result> DeleteFile(MinioBucketNames bucket, string internalId)
    {
        var bucketValidationResult = ValidateBucketsConfiguration();
        if (bucketValidationResult.IsFailed)
            return Result.Fail(new Error("Error when Validate Buckets Configuration")
                .CausedBy(bucketValidationResult.Errors));

        var bucketConfiguration = _bucketsConfiguration.GetBucket(bucket);
        var checkBucketResult = await CheckBucket(bucketConfiguration);
        if (checkBucketResult.IsFailed)
            return Result.Fail(new Error($"Error when check Bucket {bucketConfiguration.NameString}")
                .CausedBy(checkBucketResult.Errors));

        return await Result.Try(async () =>
        {
            await _minioClient.RemoveObjectAsync(new RemoveObjectArgs()
                .WithBucket(bucketConfiguration.NameString)
                .WithObject(internalId));
        });
    }

    private async Task<Result> AddFile(Stream streamData, string contentType, long fileLength, MinioBucketNames bucket,
        string internalId)
    {
        if (fileLength > MaxFileLenght)
            return Result.Fail("Max file size reached");

        var bucketValidationResult = ValidateBucketsConfiguration();
        if (bucketValidationResult.IsFailed)
            return Result.Fail(new Error("Error when Validate Buckets Configuration")
                .CausedBy(bucketValidationResult.Errors));

        var bucketConfiguration = _bucketsConfiguration.GetBucket(bucket);
        var checkBucketResult = await CheckBucket(bucketConfiguration);
        if (checkBucketResult.IsFailed)
            return Result.Fail(new Error($"Error when check Bucket {bucketConfiguration.NameString}")
                .CausedBy(checkBucketResult.Errors));

        return await Result.Try(async () =>
        {
            await _minioClient.PutObjectAsync(new PutObjectArgs()
                .WithObjectSize(fileLength)
                .WithStreamData(streamData)
                .WithBucket(bucketConfiguration.NameString)
                .WithContentType(contentType)
                .WithObject(internalId));
        });
    }

    private async Task<Result> CheckBucket(MinioBucketConfiguration bucketConfiguration)
    {
        return await Result.Try(async () =>
        {
            if (!await _minioClient.BucketExistsAsync(new BucketExistsArgs()
                    .WithBucket(bucketConfiguration.NameString)))
                await _minioClient.MakeBucketAsync(new MakeBucketArgs()
                    .WithBucket(bucketConfiguration.NameString)
                    .WithLocation(bucketConfiguration.Location));
        });
    }

    private Result ValidateBucketsConfiguration()
    {
        return Result.Try(() =>
        {
            var minioBucketsEnumValues = (MinioBucketNames[])
                Enum.GetValues(typeof(MinioBucketNames));

            if (minioBucketsEnumValues
                .Any(bucket => _bucketsConfiguration.Buckets
                    .All(b => b.Name != bucket)))
            {
                var bucketNames = string.Join(",", Enum.GetNames(typeof(MinioBucketNames)));
                throw new InvalidOperationException("Not all minio buckets configured in app configuration! " +
                                                    $"Please, specify all this list: {bucketNames}");
            }
        });
    }
}