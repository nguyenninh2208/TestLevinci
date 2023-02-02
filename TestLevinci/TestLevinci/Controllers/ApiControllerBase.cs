using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestLevinci.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/")]
    public class ApiControllerBase : ControllerBase
    {
    }

    [ApiController]
    [Route("api/public/v1/")]
    public class ApiPublicControllerBase : ControllerBase
    {

    }
}
