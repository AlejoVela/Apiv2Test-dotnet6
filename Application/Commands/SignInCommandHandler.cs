using EmployeesAPI2.Application.Commands.Validators;
using EmployeesAPI2.Application.Models;
using EmployeesAPI2.Application.Services.Interfaces;
using EmployeesAPI2.Infrastructure.interfaces;
using EmployeesAPI2.Infrastructure.Models;
using MediatR;

namespace EmployeesAPI2.Application.Commands
{
    public class SignInCommandHandler : IRequestHandler<SignInCommand, TokenViewModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthentificationService _authentificationService;
        public SignInCommandHandler(IUserRepository userRepository, IAuthentificationService authentificationService)
        {
            _userRepository = userRepository;
            _authentificationService = authentificationService;
        }
        public async Task<TokenViewModel> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            SignInCommandValidator validation = new SignInCommandValidator();
            var validationResult = validation.Validate(request);
            if (validationResult.IsValid is false)
            {
                throw new Exception(validationResult.Errors.FirstOrDefault().ErrorMessage);
            }

            User user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
            {
                throw new Exception("El usuario no existe");
            }

            bool isAuthorized = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
            if (isAuthorized is false)
            {
                throw new Exception("Usuario o contraseña invalido");
            }

            string token = _authentificationService.GenerateToken(user);

            return new TokenViewModel
            {
                AccessToken = token,
                ExpirationDate = DateTime.UtcNow.AddDays(1),
            };
        }
    }
}
