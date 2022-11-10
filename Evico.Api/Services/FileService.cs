using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Minio;
using Enum = System.Enum;

namespace Evico.Api.Services;

public class FileService
{
    private readonly MinioClient _minioClient;
    private readonly MinioBucketsConfiguration _bucketsConfiguration;

    public FileService(MinioClient minioClient, IOptions<MinioBucketsConfiguration> bucketsConfiguration)
    {
        _minioClient = minioClient;
        _bucketsConfiguration = bucketsConfiguration.Value;
    }
    
    public async Task<Result<IFormFile>> ValidateInput(IFormFile inputFile)
    {
        
        return Result.Ok(inputFile);
    }

    public async Task<Result> UploadFile(IFormFile inputFile, MinioBucketNames bucket, String internalId)
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
            await _minioClient.PutObjectAsync(new PutObjectArgs()
                .WithObjectSize(inputFile.Length)
                .WithStreamData(inputFile.OpenReadStream())
                .WithBucket(bucketConfiguration.NameString)
                .WithContentType(inputFile.ContentType)
                .WithObject(internalId));
        });
    }

    public async Task<Result<FileStreamResult>> DownloadFile(MinioBucketNames bucket, String internalId)
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
                .WithCallbackStream(stream =>
                {
                    stream.CopyTo(fileStream);
                })
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

    public async Task<Result> DeleteFile(MinioBucketNames bucket, String internalId)
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
                var bucketNames = String.Join(",", Enum.GetNames(typeof(MinioBucketNames)));
                throw new InvalidOperationException("Not all minio buckets configured in app configuration! " +
                                                    $"Please, specify all this list: {bucketNames}");
            }
        });
    }
}