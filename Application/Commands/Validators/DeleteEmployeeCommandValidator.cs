using EmployeesAPI2.Application.ResourceFiles;
using FluentValidation;

namespace EmployeesAPI2.Application.Commands.Validators
{
    public class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
    {
        public DeleteEmployeeCommandValidator()
        {
            _ = RuleFor(employee => employee.Id)
                .NotEmpty()
                .WithErrorCode(ValidatorCode.ParameterRequiered)
                .WithMessage(ValidatorMessage.ParameterRequired)
                .WithName("id");
        }
    }
}
