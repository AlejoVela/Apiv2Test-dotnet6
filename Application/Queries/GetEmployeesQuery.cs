using EmployeesAPI2.Application.Models;
using MediatR;

namespace EmployeesAPI2.Application.Queries
{
    public class GetEmployeesQuery : IRequest<List<EmployeeViewModel>>
    {
    }
}
