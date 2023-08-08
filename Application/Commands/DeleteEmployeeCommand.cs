using MediatR;

namespace EmployeesAPI2.Application.Commands
{
    public class DeleteEmployeeCommand : IRequest<bool>
    {
        public string Id { get; set; }
    }
}
