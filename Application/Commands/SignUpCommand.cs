using EmployeesAPI2.Application.Models;
using MediatR;

namespace EmployeesAPI2.Application.Commands
{
    public class SignUpCommand : SignCommand, IRequest<TokenViewModel>
    {

    }
}
