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

        public async Task<bool> DeleteAsync(string id)
        {

            FilterDefinition<Employee> filter = Builders<Employee>.Filter.Eq(employee => employee.Id, ObjectId.Parse(id));

            DeleteResult deleteResult = await _collection.DeleteOneAsync(filter);
            return (deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0);
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _collection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(string id)
        {
            FilterDefinition<Employee> filter = Builders<Employee>.Filter.
                Eq(employee => employee.Id, ObjectId.Parse(id));

            return await _collection
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        public async Task<Employee> GetByIdentificationAsync(string identification)
        {
            return await _collection
                .Find(employee => employee.Identification == identification)
                .FirstOrDefaultAsync();
        }

        public async Task<Employee> UpdateByIdAsync(Employee employeeToUpdate)
        {

            FilterDefinition<Employee> filter = Builders<Employee>.Filter.
                Eq(employee => employee.Id, employeeToUpdate.Id);

            UpdateDefinition<Employee> employeeUpdateBuilder = Builders<Employee>.Update
                .Set(employee => employee.Id, employeeToUpdate.Id)
                .Set(employee => employee.FirstName, employeeToUpdate.FirstName)
                .Set(employee => employee.LastName, employeeToUpdate.LastName)
                .Set(employee => employee.Age, employeeToUpdate.Age)
                .Set(employee => employee.Identification, employeeToUpdate.Identification)
                .Set(employee => employee.Salary, employeeToUpdate.Salary);

            UpdateResult updateResult = await _collection
                .UpdateOneAsync(filter, employeeUpdateBuilder);

            if (updateResult.IsAcknowledged && updateResult.ModifiedCount > 0)
            {
                return employeeToUpdate;
            }

            throw new Exception("No se ha podido actualizar el empleado");
        }
    }
}
