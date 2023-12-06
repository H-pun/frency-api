using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Frency.Helpers.FileHelper;

namespace Frency.Base
{
    public class BaseEntity
    {
        protected string _filePath;
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [NotMapped]
        public IFormFile File { get; set; }
        public UploadFileInfo GetFileInfo()
        {
            return new UploadFileInfo
            {
                File = File,
                FilePath = _filePath
            };
        }
    }
}
