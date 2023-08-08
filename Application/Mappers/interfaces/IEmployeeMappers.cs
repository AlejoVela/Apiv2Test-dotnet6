using EmployeesAPI2.Application.Models;
using EmployeesAPI2.Infrastructure.Models;

namespace EmployeesAPI2.Application.Mappers.interfaces
{
    public interface IEmployeeMappers
    {
        List<EmployeeViewModel> MapFromEmployeeListToEmployeeViewModelList(List<Employee> employees);
        EmployeeViewModel MapFromEmployeeToEmployeeViewModel(Employee employee);
    }
}
