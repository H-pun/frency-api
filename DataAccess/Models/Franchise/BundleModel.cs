using Microsoft.AspNetCore.Http;
using Frency.Base;
using Frency.DataAccess.Entities;
using Frency.Helpers;

namespace Frency.DataAccess.Models
{
    public class BundleModel 
    {
        public string FranchiseType { get; set; }
        public string Facility { get; set; }
        public float Price { get; set; }
        public BundleModel(FranchiseBundle bundle)
        {
            FranchiseType = bundle.FranchiseType;
            Facility = bundle.FranchiseType;
            Price = bundle.Price;
        }
    }
}
