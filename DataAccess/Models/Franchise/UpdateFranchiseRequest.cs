using Microsoft.AspNetCore.Http;
using Frency.Base;
using Frency.DataAccess.Entities;
using Frency.Helpers;

namespace Frency.DataAccess.Models
{
    public class UpdateFranchiseRequest : BaseModel
    {
        public Guid Id { get; set; }
    }
}
