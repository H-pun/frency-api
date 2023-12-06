using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Frency.DataAccess.Models
{
    [DefaultStatusCode(DefaultStatusCode)]
    public class SuccessApiResponse : ObjectResult
    {
        private const int DefaultStatusCode = StatusCodes.Status200OK;

        public SuccessApiResponse(string message, object data = null) : base(new { message, data })

        {
            StatusCode = DefaultStatusCode;
        }
    }
}
