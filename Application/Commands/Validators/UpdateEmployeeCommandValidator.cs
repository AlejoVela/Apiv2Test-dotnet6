using EmployeesAPI2.Application.ResourceFiles;
using FluentValidation;

namespace EmployeesAPI2.Application.Commands.Validators
{
    public class UpdateEmployeeCommandValidator : EmployeeCommandValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator() : base()
        {
            _ = RuleFor(employee => employee.Id)
                .NotEmpty()
                .WithErrorCode(ValidatorCode.ParameterRequiered)
                .WithMessage(ValidatorMessage.ParameterRequired)
                .WithName("id");
        }
    }
}
