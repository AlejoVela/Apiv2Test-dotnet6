using EmployeesAPI2.Application.Commands.Validators;
using EmployeesAPI2.Application.Mappers.interfaces;
using EmployeesAPI2.Application.Models;
using EmployeesAPI2.Infrastructure.interfaces;
using EmployeesAPI2.Infrastructure.Models;
using Mapster;
using MediatR;
using System;

namespace EmployeesAPI2.Application.Commands
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, EmployeeViewModel>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeMappers _employeeMappers;

        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IEmployeeMappers employeeMappers)
        {
            _employeeRepository = employeeRepository;
            _employeeMappers = employeeMappers;
        }

        public async Task<EmployeeViewModel> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            UpdateEmployeeCommandValidator updateValidator = new UpdateEmployeeCommandValidator();
            FluentValidation.Results.ValidationResult validatorResult = updateValidator.Validate(request);
            if(validatorResult.IsValid is false)
            {
                throw new Exception(validatorResult.Errors.FirstOrDefault().ErrorMessage);
            }

            Employee employeToUpdate = await _employeeRepository.GetByIdAsync(request.Id);

            if(employeToUpdate is null)
            {
                throw new Exception("El empleado indicado no existe");
            }

            Employee employeeUpdated = await _employeeRepository.UpdateByIdAsync(request.Adapt<Employee>());

            return _employeeMappers.MapFromEmployeeToEmployeeViewModel(employeeUpdated);
        }
    }
}
