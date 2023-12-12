using Frency.Base;
using System.ComponentModel.DataAnnotations.Schema;
using Frency.DataAccess.Extensions;
using Frency.Helpers;

namespace Frency.DataAccess.Entities
{
    public class Franchise : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string WhatsappNumber { get; set; }
        public string FilePath
        {
            get => _filePath;
            set => _filePath = _filePath == null ? $"Franchise/{File.SetFileName(value)}" : value;
        }
        public ICollection<FranchiseBundle> Bundles { get; set; } = new HashSet<FranchiseBundle>();
    }
}
