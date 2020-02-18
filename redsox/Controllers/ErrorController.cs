using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;


namespace redsox.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("[controller/v{version:apiVersion}")]
    public class ErrorController : Controller
    {
        [HttpGet]
        public IActionResult Error() => Problem();

        [HttpGet("local-development")]
        public IActionResult LocalDevelopment([FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
                throw new InvalidOperationException("LocalDevelopment Error handler shouldn't be invoken in non-development environments");

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            return Problem(
                detail: context.Error.StackTrace,
                title: context.Error.Message
             );
        }
    }
}
