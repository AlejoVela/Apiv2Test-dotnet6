using EmployeesAPI2.Infrastructure.interfaces;
using EmployeesAPI2.Infrastructure.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EmployeesAPI2.Infrastructure.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IMongoCollection<Employee> _collection;
        public EmployeeRepository(IMongoCollection<Employee> collection) 
        {
            _collection = collection;
        }
        public async Task<Employee> CreateAsync(Employee employee)
        {
            await _collection.InsertOneAsync(employee);

            return await _collection
                .Find(employeeToFind => employeeToFind.Identification == employee.Identification)
                .FirstOrDefaultAsync();
        }

        public Task<bool> DeleteAsync(string id)
        {

            throw new NotImplementedException();
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _collection.Find(new BsonDocument()).ToListAsync();
        }

        public Task<Employee> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Employee> GetByIdentificationAsync(string identification)
        {
            return await _collection
                .Find(employee => employee.Identification == identification)
                .FirstOrDefaultAsync();
        }

        public Task<Employee> UpdateAsync(Employee employee, string id)
        {
            throw new NotImplementedException();
        }
    }
}
