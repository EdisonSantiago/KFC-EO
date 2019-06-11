using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Oulanka.Domain.Contracts.Services;

namespace Oulanka.Services
{
    public class BlobImageService : IBlobImageService
    {
        private readonly IEventLogService _eventLogService;

        public BlobImageService(IEventLogService eventLogService)
        {
            _eventLogService = eventLogService;
        }

        private CloudStorageAccount GetConnectionString()
        {
            var account = CloudConfigurationManager.GetSetting("StorageAccountName");
            var key = CloudConfigurationManager.GetSetting("StorageAccountKey");

            var connectionString = $"DefaultEndpointsProtocol=https;AccountName={account};AccountKey={key}";
            return CloudStorageAccount.Parse(connectionString);
        }

        public string UploadImage(HttpPostedFileBase image, string blobContainer)
        {
            string imageFullPath = null;
            if (image == null || image.ContentLength == 0)
            {
                return null;
            }

            try
            {
                var cloudStorageAccount = GetConnectionString();
                var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                var cloudBlobContainer = cloudBlobClient.GetContainerReference(blobContainer);

                if (cloudBlobContainer.CreateIfNotExists())
                {
                    cloudBlobContainer.SetPermissions(new BlobContainerPermissions
                    {
                        PublicAccess = BlobContainerPublicAccessType.Blob
                    });
                }

                var imageName = Guid.NewGuid() + "-" + Path.GetExtension(image.FileName);

                var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(imageName);
                cloudBlockBlob.Properties.ContentType = image.ContentType;
                cloudBlockBlob.UploadFromStream(image.InputStream);

                imageFullPath = cloudBlockBlob.Uri.ToString();
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message, exception.Message, "images", exception);
                throw exception;
            }

            return imageFullPath;
        }

        public string UploadImage(byte[] imageBytes, string blobContainer)
        {
            if (imageBytes == null) return null;

            string imageFullPath = null;
            try
            {
                var cloudStorageAccount = GetConnectionString();
                var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                var cloudBlobContainer = cloudBlobClient.GetContainerReference(blobContainer);

                if (cloudBlobContainer.CreateIfNotExists())
                {
                    cloudBlobContainer.SetPermissions(new BlobContainerPermissions
                    {
                        PublicAccess = BlobContainerPublicAccessType.Blob
                    });
                }

                var imageName = Guid.NewGuid() + "-" + ".jpg";

                var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(imageName);
                cloudBlockBlob.Properties.ContentType = "image/jpeg";

                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    cloudBlockBlob.UploadFromStream(ms);
                }

                imageFullPath = cloudBlockBlob.Uri.ToString();
            }
            catch (Exception exception)
            {
                _eventLogService.AddException(exception.Message, exception.Message, "images", exception);
                throw exception;
            }
            return imageFullPath;
        }
    }
}