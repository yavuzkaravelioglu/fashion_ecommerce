using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController] // API controller attribute: Automatically checks the is Api valid
    [Route("api/[controller]")] // root ismi class ismi ile aynı: products
    public class BaseApiController : ControllerBase
    {
        
    }

} 