namespace EmployeesAPI2.Application.Commands
{
    public class EmployeeCommand
    {
        public string Identification { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public int Age { get; set; }
        public decimal Salary { get; set; }
    }
}
