using EmployeesAPI2.Application.ResourceFiles;
using FluentValidation;

namespace EmployeesAPI2.Application.Commands.Validators
{
    public class SignCommandValidator<T> : AbstractValidator<T> where T : SignCommand
    {
        public SignCommandValidator()
        {
            _ = RuleFor(user => user.Email)
                .NotEmpty()
                .WithErrorCode(ValidatorCode.ParameterRequiered)
                .WithMessage(ValidatorMessage.ParameterRequired)
                .Matches("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\\.[a-zA-Z0-9-]+)*$")
                .WithErrorCode(ValidatorCode.InvalidEmail)
                .WithMessage(ValidatorMessage.InvalidEmail)
                .WithName("email");

            _ = RuleFor(user => user.Password)
                .NotEmpty()
                .WithErrorCode(ValidatorCode.ParameterRequiered)
                .WithMessage(ValidatorMessage.ParameterRequired)
                .Matches("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}")
                .WithErrorCode(ValidatorCode.InvalidPassword)
                .WithMessage(ValidatorMessage.InvalidPassword)
                .WithName("password");
        }
    }
}
