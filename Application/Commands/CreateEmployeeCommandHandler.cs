using EmployeesAPI2.Application.Commands.Validators;
using EmployeesAPI2.Application.Mappers.interfaces;
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
        private readonly IEmployeeMappers _employeeMappers;
        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IEmployeeMappers employeeMappers)
        {
            _employeeRepository = employeeRepository;
            _employeeMappers = employeeMappers;
        }

        public async Task<EmployeeViewModel> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            CreateEmployeeCommandValidator validator = new();
            FluentValidation.Results.ValidationResult resultValidator = validator.Validate(request);

            if (resultValidator.IsValid is false)
            {
                FluentValidation.Results.ValidationFailure exception = resultValidator.Errors.FirstOrDefault();
                throw new Exception(exception.ErrorMessage);
            }

            Employee employeeAlreadyExist = await _employeeRepository.GetByIdentificationAsync(request.Identification);
            if(employeeAlreadyExist is not null)
            {
                throw new Exception("El empleado ya existe");
            }

            Employee result = await _employeeRepository.CreateAsync(request.Adapt<Employee>());

            return _employeeMappers.MapFromEmployeeToEmployeeViewModel(result);
        }
    }
}
