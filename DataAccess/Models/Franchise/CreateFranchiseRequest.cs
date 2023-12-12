using Microsoft.AspNetCore.Http;
using Frency.Base;
using Frency.DataAccess.Entities;
using Frency.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Frency.DataAccess.Models
{
    public class CreateFranchiseRequest : BaseModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string WhatsappNumber { get; set; }
        public List<BundleModel> Bundles { get; set; }
        public override TEntity MapToEntity<TEntity>()
        {

            Franchise franchise = base.MapToEntity<Franchise>();
            franchise.Bundles = Bundles.Select(x => new FranchiseBundle
            {
                FranchiseType = x.FranchiseType,
                Facility = x.Facility,
                Price = x.Price
            }).ToList();
            return franchise as TEntity;
        }
    }
}
