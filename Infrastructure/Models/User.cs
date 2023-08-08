using MongoDB.Bson;

namespace EmployeesAPI2.Infrastructure.Models
{
    public class User
    {
        public ObjectId Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
