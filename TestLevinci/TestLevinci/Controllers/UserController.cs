using Jwt.Applications.User.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TestLevinci.Controllers
{
    public class UserController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [AllowAnonymous]
        [HttpPost("user/login")]
        public async Task<IActionResult> Login(Login.Command command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}