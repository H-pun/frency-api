using System.Net.Http.Headers;

namespace Frency.DataAccess.Extensions
{
    public static class IFormFileExtension
    {
        public static string GetFileName(this IFormFile file)
        {
            return ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.ToString().Trim('"');
        }

        public static string SetFileName(this IFormFile file, string fileName)
        {
            string ext = Path.GetExtension(file.GetFileName());
            return fileName + ext;
        }

        public static async Task<MemoryStream> GetFileStream(this IFormFile file)
        {
            MemoryStream filestream = new();
            await file.CopyToAsync(filestream);
            return filestream;
        }

        public static async Task<byte[]> GetFileArray(this IFormFile file)
        {
            MemoryStream filestream = new();
            await file.CopyToAsync(filestream);
            return filestream.ToArray();
        }
    }
}
