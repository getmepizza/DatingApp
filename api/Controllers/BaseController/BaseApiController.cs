using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.BaseController
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {

    }
}