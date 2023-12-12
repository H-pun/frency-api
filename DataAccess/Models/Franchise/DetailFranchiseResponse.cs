using Microsoft.AspNetCore.Http;
using Frency.Base;
using Frency.DataAccess.Entities;
using Frency.Helpers;

namespace Frency.DataAccess.Models
{
    public class DetailFranchiseResponse : BaseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string WhatsappNumber { get; set; }
        public List<BundleModel> Bundles { get; set; }
        public DetailFranchiseResponse()
        {
            IncludeProperty(new string[] { "Bundles" });
        }
        public override void MapToModel<TEntity>(TEntity entity)
        {
            base.MapToModel(entity);
            Franchise franchise = entity as Franchise;
            Bundles = franchise.Bundles.Select(x => new BundleModel(x)).ToList();
        }
    }
}
