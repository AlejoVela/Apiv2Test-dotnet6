using MongoDB.Bson;

namespace EmployeesAPI2.Application.Models
{
    public class EmployeeViewModel
    {
        public string Id { get; set; }
        public string Identification { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public int Age { get; set; }
        public decimal MoneyToPay { get; set; }
    }
}
