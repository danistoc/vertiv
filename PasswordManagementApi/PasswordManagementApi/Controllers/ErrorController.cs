using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace PasswordManagementApi.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/error")]
        public IActionResult Error()
        {
            var error = HttpContext.Features.Get<IExceptionHandlerFeature>().Error;

            // log error

            return StatusCode((int)HttpStatusCode.InternalServerError, $"There was an internal error.");
        }
    }
}
