using EmployeesAPI2.Application.Commands;
using EmployeesAPI2.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesAPI2.Controllers
{
    [ApiController]
    [Route("/api/users")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpCommand signUp)
        {
            TokenViewModel result = await _mediator.Send(signUp);
            return Ok(result);
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignInAsync([FromBody] SignInCommand signIn)
        {
            TokenViewModel result = await _mediator.Send(signIn);
            return Ok(result);
        }
    }
}
