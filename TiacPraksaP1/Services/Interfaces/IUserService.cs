using TiacPraksaP1.DTOs.Delete;
using TiacPraksaP1.DTOs.Get;
using TiacPraksaP1.DTOs.Post;

namespace TiacPraksaP1.Services.Interfaces
{
    public interface IUserService
    {
        public Task<IEnumerable<UserGetResponse>> GetAllUsers();

        public Task<UserGetResponse> GetSpecificUser(int id);

        public Task<UserPostResponse> CreateUser(UserPostRequest User);

        public Task<UserDeleteResponse> DeleteUser(int id);

        public Task<UserPostResponse> UpdateUser(UserPostRequest user);
    }
}
