using EmployeesAPI2.Application.Commands;
using EmployeesAPI2.Application.Commands.Validators;
using EmployeesAPI2.Application.Models;
using EmployeesAPI2.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesAPI2.Controllers
{
    [ApiController]
    [Route("/api/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            List<EmployeeViewModel> employees = await _mediator.Send(new GetEmployeesQuery());
            return Ok(employees);
        }

        [HttpPost(Name = "CreateEmployee")]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand createEmployeeCommand)
        {
            CreateEmployeeCommandValidator validator = new();
            FluentValidation.Results.ValidationResult resultValidator = validator.Validate(createEmployeeCommand);

            if (resultValidator.IsValid is false)
            {
                return BadRequest(resultValidator.Errors.FirstOrDefault());
            }

            EmployeeViewModel employee = await _mediator.Send(createEmployeeCommand);

            return Created("", employee);
        }
    }
}
