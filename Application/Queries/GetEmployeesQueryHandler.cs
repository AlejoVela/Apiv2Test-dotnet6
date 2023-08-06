using EmployeesAPI2.Application.Mappers.interfaces;
using EmployeesAPI2.Application.Models;
using EmployeesAPI2.Infrastructure.interfaces;
using EmployeesAPI2.Infrastructure.Models;
using Mapster;
using MediatR;

namespace EmployeesAPI2.Application.Queries
{
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, List<EmployeeViewModel>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeMappers _employeeMappers;
        public GetEmployeesQueryHandler(IEmployeeRepository employeeRepository, IEmployeeMappers employeeMappers)
        {
            _employeeRepository = employeeRepository;
            _employeeMappers = employeeMappers;
        }
        public async Task<List<EmployeeViewModel>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            List<Employee> results = await _employeeRepository.GetAllAsync();

            return _employeeMappers.MapFromEmployeeListToEmployeeViewModelList(results);
        }
    }
}
