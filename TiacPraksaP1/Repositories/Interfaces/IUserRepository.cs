using TiacPraksaP1.Models;

namespace TiacPraksaP1.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> CreateUser(User user);

        public Task<User> DeleteUser(int id);

        public Task<User> UpdateUser(User role);

        public Task<User> GetUser(int id);

        public Task<IEnumerable<User>> GetUsers();
    }
}
