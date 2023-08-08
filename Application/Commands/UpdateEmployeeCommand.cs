using EmployeesAPI2.Application.Models;
using MediatR;

namespace EmployeesAPI2.Application.Commands
{
    public class UpdateEmployeeCommand : EmployeeCommand, IRequest<EmployeeViewModel>
    {
        public string Id { get; set; }

        public void SetIdToUpdate(string id)
        {
            Id = id;
        }
    }
}
