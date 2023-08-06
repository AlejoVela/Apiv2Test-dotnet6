using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EmployeesAPI2.Infrastructure.Models
{
    public class Employee
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Identification { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public int Age { get; set; }
        public decimal Salary { get; set; }
    }
}
