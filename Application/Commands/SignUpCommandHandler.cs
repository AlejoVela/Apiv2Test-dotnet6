using EmployeesAPI2.Application.Commands.Validators;
using EmployeesAPI2.Application.Models;
using EmployeesAPI2.Application.Services.Interfaces;
using EmployeesAPI2.Infrastructure.interfaces;
using EmployeesAPI2.Infrastructure.Models;
using Mapster;
using MediatR;

namespace EmployeesAPI2.Application.Commands
{
    public class SignUpCommandHandler : IRequestHandler<SignUpCommand, TokenViewModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthentificationService _authentificationService;
        public SignUpCommandHandler(IUserRepository userRepository, IAuthentificationService authentificationService)
        {
            _userRepository = userRepository;
            _authentificationService = authentificationService;
        }

        public async Task<TokenViewModel> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            // FluentValidations
            SignUpCommandValidator validator = new SignUpCommandValidator();
            var validatorResponse = validator.Validate(request);
            if(validatorResponse.IsValid is false)
            {
                throw new Exception(validatorResponse.Errors.FirstOrDefault().ErrorMessage);
            }

            User user = await _userRepository.GetByEmailAsync(request.Email);
            if (user is not null)
            {
                throw new Exception("El usuario indicado ya existe");
            }

            string salt = BCrypt.Net.BCrypt.GenerateSalt(10);
            string hash = BCrypt.Net.BCrypt.HashPassword(request.Password, salt);

            request.Password = hash;

            bool isCreated = await _userRepository.CreateAsync(request.Adapt<User>());

            if (isCreated is false)
            {
                throw new Exception("Ha ocurrido un error al crear el usuario");
            }

            return new TokenViewModel
            {
                AccessToken = _authentificationService.GenerateToken(request.Adapt<User>()),
                ExpirationDate = DateTime.UtcNow.AddDays(1),
            };
        }
    }
}
