using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Frency.Base;
using Frency.DataAccess.Entities;
using Frency.DataAccess.Models.Constants;
using Frency.DataAccess.Models;
using Frency.DataAccess.Services;

namespace Frency.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FranchiseController : BaseController<
        CreateFranchiseRequest,
        UpdateFranchiseRequest,
        DetailFranchiseResponse,
        Franchise>
    {
        private readonly ILogger<FranchiseController> _logger;
        private readonly IFranchiseService _service;
        public FranchiseController(ILogger<FranchiseController> logger, IFranchiseService service) : base(service)
        {
            _logger = logger;
            _service = service;
        }        
    }
}
