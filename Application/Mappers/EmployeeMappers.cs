using EmployeesAPI2.Application.Mappers.interfaces;
using EmployeesAPI2.Application.Models;
using EmployeesAPI2.Infrastructure.Models;
using Mapster;

namespace EmployeesAPI2.Application.Mappers
{
    public class EmployeeMappers : IEmployeeMappers
    {
        public EmployeeMappers()
        {
            #region Map From Employee list to Employee view model list
            _ = TypeAdapterConfig<Employee, EmployeeViewModel>.NewConfig()
                    .Map(dest => dest.MoneyToPay, src => src.Salary);
            #endregion
        }

        public List<EmployeeViewModel> MapFromEmployeeListToEmployeeViewModelList(List<Employee> employees)
        {
            return employees.Adapt<List<EmployeeViewModel>>();
        }

        public EmployeeViewModel MapFromEmployeeToEmployeeViewModel(Employee employee)
        {
            return employee.Adapt<EmployeeViewModel>();
        }
    }
}
