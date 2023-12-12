using Frency.Base;
using Frency.DataAccess.Entities;
using Frency.DataAccess.Models;

namespace Frency.DataAccess.Services
{
    public interface IFranchiseService : IBaseService<Franchise>
    {

    }
    public class FranchiseService : BaseService<Franchise>, IFranchiseService
    {
        public FranchiseService(AppDbContext appDbContext) : base(appDbContext) { }

    }
}
