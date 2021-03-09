using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("errors/{code}")] // overrides BaseApiController Route("api") 
    [ApiExplorerSettings(IgnoreApi = true)] // it means that swagger doesn't produce this inside our documentation.
    public class ErrorController : BaseApiController
    {
        public IActionResult Error(int code) // we pass Status Code as a parameter
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}