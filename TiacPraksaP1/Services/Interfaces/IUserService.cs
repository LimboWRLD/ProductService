using TiacPraksaP1.DTOs.Delete;
using TiacPraksaP1.DTOs.Get;
using TiacPraksaP1.DTOs.Post;

namespace TiacPraksaP1.Services.Interfaces
{
    public interface IUserService
    {
        public IEnumerable<UserGetResponse> GetAllUsers();

        public UserGetResponse GetSpecificUser(int id);

        public UserPostResponse CreateUser(UserPostRequest User);

        public UserDeleteResponse DeleteUser(int id);

        public UserPostResponse UpdateUser(UserPostRequest user);
    }
}
