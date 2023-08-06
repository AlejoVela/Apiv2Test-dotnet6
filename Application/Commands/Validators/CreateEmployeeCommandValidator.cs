using EmployeesAPI2.Application.ResourceFiles;
using FluentValidation;

namespace EmployeesAPI2.Application.Commands.Validators
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator() {

            _ = RuleFor(employee => employee.FirstName)
                .NotEmpty()
                .WithErrorCode(ValidatorCode.ParameterRequiered)
                .WithMessage(ValidatorMessage.ParameterRequired)
                .WithName("fistName");

            _ = RuleFor(employee => employee.LastName)
                .NotEmpty()
                .WithErrorCode(ValidatorCode.ParameterRequiered)
                .WithMessage(ValidatorMessage.ParameterRequired)
                .WithName("lastName");

            _ = RuleFor(employee => employee.Age)
                .NotNull()
                .WithErrorCode(ValidatorCode.ParameterRequiered)
                .WithMessage(ValidatorMessage.ParameterRequired)
                .GreaterThanOrEqualTo(18)
                .LessThan(123)
                .WithErrorCode(ValidatorCode.InvalidAge)
                .WithMessage(ValidatorMessage.InvalidAge);

            _ = RuleFor(employee => employee.Identification)
                .NotEmpty()
                .WithErrorCode(ValidatorCode.ParameterRequiered)
                .WithMessage(ValidatorMessage.ParameterRequired)
                .Matches("^[0-9]+$")
                .WithErrorCode(ValidatorCode.InvalidIdentification)
                .WithMessage(ValidatorMessage.InvalidIdentification)
                .WithName("identification");

            _ = RuleFor(employee => employee.Identification)
                .Matches("^[0-9]{10}$")
                .WithErrorCode(ValidatorCode.InvalidIdentification)
                .WithMessage(ValidatorMessage.InvalidIdentification)
                .WithName("identification")
                .When(employee => employee.Age <= 19);

            _ = RuleFor(employee => employee.Salary)
                .GreaterThan(0)
                .WithErrorCode("Salario Invalido")
                .WithMessage("El salario no puede ser menor a cero")
                .LessThanOrEqualTo(decimal.MaxValue)
                .WithErrorCode("Salario Invalido")
                .WithMessage("El salario ingresado no es realista");
        }
    }
}
