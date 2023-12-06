using OfficeOpenXml;
using Frency.Base;
using Frency.DataAccess.Extensions;
using System.IO;
using System.IO.Compression;
using System.Net;

namespace Frency.Helpers
{
    public class FileHelper
    {
        public class DownloadInfo
        {
            public MemoryStream Stream { get; set; }
            public string ContentType { get; set; }
            public string FileName { get; set; }
        }
        public class UploadFileInfo
        {
            public IFormFile File { get; set; }
            public string FilePath { get; set; }
        }

        public class TreeNode
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public List<TreeNode> Children { get; set; }
        }

        public static IEnumerable<TreeNode> GetDirectoryTree(string folderPath)
        {
            var nodes = new List<TreeNode>();

            var directories = Directory.GetDirectories(folderPath);
            foreach (var directory in directories)
            {
                var directoryNode = new TreeNode
                {
                    Name = Path.GetFileName(directory),
                    Type = "Folder",
                    Children = GetDirectoryTree(directory).ToList()
                };
                nodes.Add(directoryNode);
            }

            var files = Directory.GetFiles(folderPath);
            foreach (var file in files)
            {
                var fileNode = new TreeNode
                {
                    Name = Path.GetFileName(file),
                    Type = "File"
                };
                nodes.Add(fileNode);
            }

            return nodes;
        }

        // public static DownloadInfo DownloadExcel<T>(CancellationToken cancellationToken)
        // {

        //     // query data from database  
        //     // List<SchoolClass> list = new SchoolClassService().GetAll();
        //     var stream = new MemoryStream();

        //     using (var package = new ExcelPackage(stream))
        //     {
        //         var workSheet = package.Workbook.Worksheets.Add("Sheet1");
        //         workSheet.TabColor = System.Drawing.Color.Black;  
        //         workSheet.DefaultRowHeight = 12;  
        //         //Header of table  
        //         //  
        //         workSheet.Row(1).Height = 20;  
        //         workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //         workSheet.Row(1).Style.Font.Bold = true;  
        //         // workSheet.Cells.LoadFromCollection(list, true);
        //         workSheet.Column(1).AutoFit();  
        //         workSheet.Column(2).AutoFit();  
        //         workSheet.Column(3).AutoFit();  
        //         workSheet.Column(4).AutoFit();
        //         workSheet.Column(5).AutoFit();
        //         workSheet.Column(6).AutoFit();
        //         // ws.Cells["A3"].Style.Numberformat.Format = "yyyy-mm-dd";
        //         // ws.Cells["A3"].Formula = "=DATE(2014,10,5)";
        //         package.Save();
        //     }

        //     stream.Position = 0;
        //     //return File(stream, "application/octet-stream", excelName);
        //     return new DownloadInfo(){
        //         stream = stream,
        //         contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //         fileName = $"UserList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx"
        //     };
        // }
        public static List<T> GetExcelData<T>(IFormFile file, CancellationToken cancellationToken)
        {
            if (file == null || file.Length <= 0)
                throw new ArgumentException("No file chosen!");

            if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("File extension not supported");

            using var stream = new MemoryStream();
            file.CopyToAsync(stream, cancellationToken);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var package = new ExcelPackage(stream);
            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
            List<T> entities = worksheet.ConvertSheetToObjects<T>();
            return entities;
        }
        public static FileStream DownloadFolderZip(string[] dir)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload", Path.Combine(dir));

            if (!Directory.Exists(path))
                throw new HttpRequestException("Folder not found!", null, HttpStatusCode.NotFound);

            string zipPath = $"{path}_{((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds()}.zip";
            ZipFile.CreateFromDirectory(path, zipPath);

            // byte[] fileByte = await File.ReadAllBytesAsync(zipPath);
            // File.Delete(zipPath);

            return new FileStream(zipPath, FileMode.Open, FileAccess.Read, FileShare.None, 4096, FileOptions.DeleteOnClose);
        }
        public static async Task UploadFileAsync(UploadFileInfo upload)
        {
            if (upload.File == null || upload.File.Length == 0) { return; }
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload", upload.FilePath);
            new FileInfo(path).Directory.Create();

            using var stream = new FileStream(path, FileMode.Create);
            await upload.File.CopyToAsync(stream);
        }
    }
}
