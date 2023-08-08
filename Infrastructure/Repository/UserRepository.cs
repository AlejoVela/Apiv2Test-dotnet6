using EmployeesAPI2.Infrastructure.interfaces;
using EmployeesAPI2.Infrastructure.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EmployeesAPI2.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _collection;

        public UserRepository(IMongoCollection<User> collection)
        {
            _collection = collection;
        }

        public async Task<bool> CreateAsync(User user)
        {
            try
            {
                await _collection.InsertOneAsync(user);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _collection
                .Find(user => user.Id == ObjectId.Parse(id))
                .FirstOrDefaultAsync();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _collection
                .Find(user => user.Email == email)
                .FirstOrDefaultAsync();
        }
    }
}
