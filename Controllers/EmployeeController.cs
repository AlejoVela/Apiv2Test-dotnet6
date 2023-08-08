using EmployeesAPI2.Application.Commands;
using EmployeesAPI2.Application.Filters;
using EmployeesAPI2.Application.Models;
using EmployeesAPI2.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesAPI2.Controllers
{
    [ApiController]
    [Route("/api/employees")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetEmployees")]
        public async Task<IActionResult> GetEmployeesAsync()
        {
            List<EmployeeViewModel> employees = await _mediator.Send(new GetEmployeesQuery());
            return Ok(employees);
        }

        [HttpPost(Name = "CreateEmployee")]
        public async Task<IActionResult> CreateEmployeeAsync([FromBody] CreateEmployeeCommand createEmployeeCommand)
        {
            EmployeeViewModel employee = await _mediator.Send(createEmployeeCommand);

            return Created("", employee);
        }

        [HttpPut("{id}", Name = "UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployeeAsync(
            [FromBody] UpdateEmployeeCommand updateEmployeeCommand,
            [FromRoute] string id)
        {
            updateEmployeeCommand.SetIdToUpdate(id);
            EmployeeViewModel employee = await _mediator.Send(updateEmployeeCommand);

            return Ok(employee);
        }

        [HttpDelete("{id}", Name = "DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployeeAsync(
            [FromRoute] string id)
        {
            bool employee = await _mediator.Send(new DeleteEmployeeCommand { Id = id });

            return Ok(employee);
        }
    }
}
