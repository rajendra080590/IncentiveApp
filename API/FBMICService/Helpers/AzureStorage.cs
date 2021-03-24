using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Sas;
using FBMICService.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FBMICService.Helpers
{
    public class AzureStorage
    {
        static BlobContainerClient GetContainer(ContainerType containerType)
        {
            string storageConnectionstring = string.Empty; // read from application settings
            storageConnectionstring = "DefaultEndpointsProtocol=https;AccountName=fbmdevstorageaccount;AccountKey=CnffIaECGNR1u12oDlMw8B0h6jxSX/tq7up+Lugi5ZAIu7VLLKAIsJgrgKE1Y/DCXF5OQiGLsKHRNDJnYvgeog==;EndpointSuffix=core.windows.net";
            //var account = CloudStorageAccount.Parse(storageConnectionstring);
            BlobContainerClient containerClient = new BlobContainerClient(storageConnectionstring, containerType.ToString().ToLower());
            containerClient.CreateIfNotExists();
            //CloudBlobClient client = account.CreateCloudBlobClient();
            return containerClient;//.GetContainerReference(containerType.ToString().ToLower());
        }

        public static async Task<IList<string>> GetFilesListAsync(ContainerType containerType)
        {
            var container = GetContainer(containerType);

            var allBlobsList = new List<string>();
            //BlobContinuationToken token = null;

            //do
            //{
            //    var result = await container.ListBlobsSegmentedAsync(token);
            //    if (result.Results.Count() > 0)
            //    {
            //        var blobs = result.Results.Cast<CloudBlockBlob>().Select(b => b.Name);
            //        allBlobsList.AddRange(blobs);
            //    }
            //    token = result.ContinuationToken;
            //} while (token != null);

            foreach (BlobItem blob in container.GetBlobs())
            {
                allBlobsList.Add(blob.Name);
            }

            return allBlobsList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static async Task<byte[]> GetFileAsync(ContainerType containerType, string name)
        {
            var container = GetContainer(containerType);

            BlobClient blob = container.GetBlobClient(name);
            //var blob = container.GetBlobReference(name);
            if (await blob.ExistsAsync())
            {
                BlobDownloadInfo downloadInfo = await blob.DownloadAsync();
                byte[] blobBytes = new byte[downloadInfo.Content.Length];
                await downloadInfo.Content.WriteAsync(blobBytes, 0, (int)downloadInfo.Content.Length);

                return blobBytes;
            }
            return null;
        }

        public static async Task<string> UploadFileAsync(ContainerType containerType, Stream stream)
        {
            var container = GetContainer(containerType);
            await container.CreateIfNotExistsAsync();

            var name = Guid.NewGuid().ToString();

            BlobClient blob = container.GetBlobClient(name);

            // Upload local file
            await blob.UploadAsync(stream);

            return name;
        }

        public static async Task<string> UploadFileAsync(ContainerType containerType, string filepath)
        {
            var container = GetContainer(containerType);
            await container.CreateIfNotExistsAsync();

            //var name = Guid.NewGuid().ToString();

            string fileName = Path.GetFileName(filepath);

            BlobClient blob = container.GetBlobClient(fileName);

            // Upload local file
            await blob.UploadAsync(filepath);

            return fileName;
        }

        public static async Task<string> UploadFileAsync(ContainerType containerType, string name, Stream stream)
        {
            var container = GetContainer(containerType);
            await container.CreateIfNotExistsAsync();

            BlobClient blob = container.GetBlobClient(name);
            await blob.UploadAsync(stream);

            return name;
        }


        public static async Task<bool> DeleteFileAsync(ContainerType containerType, string name)
        {
            var container = GetContainer(containerType);
            var blob = container.GetBlobClient(name);

            return await blob.DeleteIfExistsAsync();
        }

        public static async Task<bool> DeleteContainerAsync(ContainerType containerType)
        {
            var container = GetContainer(containerType);
            return await container.DeleteIfExistsAsync();
        }

        //public static Uri GetServiceSasUriForContainer(ContainerType containerType,
        //                                   string storedPolicyName = null)
        //{
        //    BlobContainerClient containerClient = GetContainer(containerType);
        //    // Check whether this BlobContainerClient object has been authorized with Shared Key.
        //    if (containerClient.CanGenerateSasUri)
        //    {
        //        // Create a SAS token that's valid for one hour.
        //        BlobSasBuilder sasBuilder = new BlobSasBuilder()
        //        {
        //            BlobContainerName = containerClient.Name,
        //            Resource = "c"
        //        };

        //        if (storedPolicyName == null)
        //        {
        //            sasBuilder.ExpiresOn = DateTimeOffset.UtcNow.AddHours(1);
        //            sasBuilder.SetPermissions(BlobContainerSasPermissions.Read);
        //        }
        //        else
        //        {
        //            sasBuilder.Identifier = storedPolicyName;
        //        }

        //        Uri sasUri = containerClient.GenerateSasUri(sasBuilder);
        //        Console.WriteLine("SAS URI for blob container is: {0}", sasUri);
        //        Console.WriteLine();

        //        return sasUri;
        //    }
        //    else
        //    {
        //        Console.WriteLine(@"BlobContainerClient must be authorized with Shared Key 
        //                  credentials to create a service SAS.");
        //        return null;
        //    }
        //}

        //public static Uri GetUserDelegationSasBlob(ContainerType containerType, string blobName)
        //{
        //    var container = GetContainer(containerType);
        //    var blobClient = container.GetBlobClient(blobName);
        //    BlobServiceClient blobServiceClient =
        //        blobClient.GetParentBlobContainerClient().GetParentBlobServiceClient();


        //    // Create a SAS token that's also valid for 7 days.
        //    BlobSasBuilder sasBuilder = new BlobSasBuilder()
        //    {
        //        BlobContainerName = blobClient.BlobContainerName,
        //        BlobName = blobClient.Name,
        //        Resource = "b",
        //        StartsOn = DateTimeOffset.UtcNow,
        //        ExpiresOn = DateTimeOffset.UtcNow.AddDays(7)
        //    };

        //    // Specify read and write permissions for the SAS.
        //    sasBuilder.SetPermissions(BlobSasPermissions.Read |
        //                              BlobSasPermissions.Write);

        //    // Get a user delegation key for the Blob service that's valid for 7 days.
        //    // You can use the key to generate any number of shared access signatures 
        //    // over the lifetime of the key.
        //    Azure.Storage.Blobs.Models.UserDelegationKey userDelegationKey =
        //         blobServiceClient.GetUserDelegationKey(DateTimeOffset.UtcNow,
        //                                                          DateTimeOffset.UtcNow.AddDays(7));

        //    // Add the SAS token to the blob URI.
        //    BlobUriBuilder blobUriBuilder = new BlobUriBuilder(blobClient.Uri)
        //    {
        //        // Specify the user delegation key.
        //        Sas = sasBuilder.ToSasQueryParameters(userDelegationKey,
        //                                              blobServiceClient.AccountName)
        //    };

        //    Console.WriteLine("Blob user delegation SAS URI: {0}", blobUriBuilder);
        //    Console.WriteLine();
        //    return blobUriBuilder.ToUri();
        //}

        public static Uri GetServiceSasUriForBlob(ContainerType containerType, string blobName,
            string storedPolicyName = null)
        {
            var container = GetContainer(containerType);
            var blobClient = container.GetBlobClient(blobName);
            // Check whether this BlobClient object has been authorized with Shared Key.
            if (blobClient.CanGenerateSasUri)
            {
                // Create a SAS token that's valid for one hour.
                BlobSasBuilder sasBuilder = new BlobSasBuilder()
                {
                    BlobContainerName = blobClient.GetParentBlobContainerClient().Name,
                    BlobName = blobClient.Name,
                    Resource = "b"
                };

                if (storedPolicyName == null)
                {
                    sasBuilder.ExpiresOn = DateTimeOffset.UtcNow.AddHours(1);
                    sasBuilder.SetPermissions(BlobSasPermissions.Read |
                        BlobSasPermissions.Write);
                }
                else
                {
                    sasBuilder.Identifier = storedPolicyName;
                }

                Uri sasUri = blobClient.GenerateSasUri(sasBuilder);
                Console.WriteLine("SAS URI for blob is: {0}", sasUri);
                Console.WriteLine();

                return sasUri;
            }
            else
            {
                Console.WriteLine(@"BlobClient must be authorized with Shared Key 
                          credentials to create a service SAS.");
                return null;
            }
        }
    }
}
