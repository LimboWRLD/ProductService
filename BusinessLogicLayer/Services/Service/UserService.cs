using AutoMapper;
using TiacPraksaP1.DTOs.Delete;
using TiacPraksaP1.DTOs.Get;
using TiacPraksaP1.DTOs.Post;
using TiacPraksaP1.Models;
using TiacPraksaP1.Repositories.Interfaces;
using TiacPraksaP1.Services.Interfaces;

namespace TiacPraksaP1.Services.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserPostResponse> CreateUser(UserPostRequest user)
        {
            var newUser = _mapper.Map<User>(user);
            var createdUser = await _userRepository.CreateUser(newUser, user.Password);
            return _mapper.Map<UserPostResponse>(createdUser);
        }

        public async Task<UserDeleteResponse> DeleteUser(string id)
        {
            var deletedUser = await _userRepository.DeleteUser(id);
            return _mapper.Map<UserDeleteResponse>(deletedUser);
        }

        public async Task<IEnumerable<UserGetResponse>> GetAllUsers()
        {
            var users = await _userRepository.GetUsers();
            return _mapper.Map<IEnumerable<UserGetResponse>>(users);
        }

        public async Task<UserGetResponse> GetSpecificUser(string id)
        {
            var user = await _userRepository.GetUser(id);
            return _mapper.Map<UserGetResponse>(user);
        }

        public async Task<UserPostResponse> UpdateUser(string id,UserPostRequest user)
        {
            var userToUpdate = _mapper.Map<User>(user);
            userToUpdate.Id = id;
            var updatedUser = await _userRepository.UpdateUser(userToUpdate);
            return _mapper.Map<UserPostResponse>(updatedUser);
        }
    }
}
