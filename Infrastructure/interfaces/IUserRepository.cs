using EmployeesAPI2.Infrastructure.Models;

namespace EmployeesAPI2.Infrastructure.interfaces
{
    public interface IUserRepository
    {
        Task<bool> CreateAsync(User user);
        Task<User> GetByIdAsync(string id);
        Task<User> GetByEmailAsync(string Email);
    }
}
