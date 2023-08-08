using EmployeesAPI2.Application.Commands.Validators;
using EmployeesAPI2.Infrastructure.interfaces;
using MediatR;

namespace EmployeesAPI2.Application.Commands
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            DeleteEmployeeCommandValidator validator = new ();
            var validatorResult = validator.Validate(request);

            if (validatorResult.IsValid is false)
            {
                throw new Exception("El id indicado es incorrecto");
            }

            return await _employeeRepository.DeleteAsync(request.Id);
        }
    }
}
