using EmployeesAPI2.Infrastructure.Models;
using System.Security.Claims;

namespace EmployeesAPI2.Application.Services.Interfaces
{
    public interface IAuthentificationService
    {
        string GenerateToken(User user);
        ClaimsPrincipal ValidateToken(string token);
    }
}
