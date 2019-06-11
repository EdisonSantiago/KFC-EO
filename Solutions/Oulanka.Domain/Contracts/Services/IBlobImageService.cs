using System.Threading.Tasks;
using System.Web;

namespace Oulanka.Domain.Contracts.Services
{
    public interface IBlobImageService
    {
       string UploadImage(HttpPostedFileBase image, string blobContainer);
       string UploadImage(byte[] imageBytes, string blobContainer);

    }
}