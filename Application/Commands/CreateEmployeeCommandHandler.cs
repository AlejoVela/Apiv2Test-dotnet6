using EmployeesAPI2.Application.Models;
using EmployeesAPI2.Infrastructure.interfaces;
using EmployeesAPI2.Infrastructure.Models;
using EmployeesAPI2.Infrastructure.Repository;
using Mapster;
using MediatR;

namespace EmployeesAPI2.Application.Commands
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, EmployeeViewModel>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeViewModel> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            Employee employeeAlreadyExist = await _employeeRepository.GetByIdentificationAsync(request.Identification);
            if(employeeAlreadyExist is not null)
            {
                throw new Exception("El empleado ya existe");
            }

            Employee result = await _employeeRepository.CreateAsync(request.Adapt<Employee>());

            return result.Adapt<EmployeeViewModel>();
        }
    }
}
