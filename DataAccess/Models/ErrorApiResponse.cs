using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Frency.DataAccess.Models
{
    [DefaultStatusCode(DefaultStatusCode)]
    public class ErrorApiResponse : ObjectResult
    {
        private const int DefaultStatusCode = StatusCodes.Status500InternalServerError;

        public ErrorApiResponse(string message, object data = null, int? statusCode = null) : base(new { message, data })
        {
            StatusCode = statusCode == null ? DefaultStatusCode : statusCode;
        }
    }
}
