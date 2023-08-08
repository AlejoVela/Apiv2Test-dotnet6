using EmployeesAPI2.Infrastructure.Models;

namespace EmployeesAPI2.Infrastructure.interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> CreateAsync(Employee employee);

        Task<Employee> UpdateByIdAsync(Employee employee);

        Task<bool> DeleteAsync(string id);

        Task<Employee> GetByIdAsync(string id);
        Task<Employee> GetByIdentificationAsync(string id);
        Task<List<Employee>> GetAllAsync();
    }
}
