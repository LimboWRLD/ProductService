using TiacPraksaP1.Models;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> CreateUser(User user, string password);

        public Task<User> DeleteUser(string id);

        public Task<User> UpdateUser(User role);

        public Task<User> GetUser(string id);

        public Task<IEnumerable<User>> GetUsers();
    }
}
