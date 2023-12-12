using Frency.Base;
using System.ComponentModel.DataAnnotations.Schema;
using Frency.DataAccess.Extensions;
using Frency.Helpers;

namespace Frency.DataAccess.Entities
{
    public class FranchiseBundle : BaseEntity
    {
        public Guid IdFranchise { get; set; }
        public string FranchiseType { get; set; }
        public string Facility { get; set; }
        public float Price { get; set; }
        [ForeignKey(nameof(IdFranchise))]
        public Franchise Franchise { get; set; }

    }
}
