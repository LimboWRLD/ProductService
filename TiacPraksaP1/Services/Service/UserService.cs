using AutoMapper;
using TiacPraksaP1.DTOs.Delete;
using TiacPraksaP1.DTOs.Get;
using TiacPraksaP1.DTOs.Post;
using TiacPraksaP1.Models;
using TiacPraksaP1.Repositories.Interfaces;
using TiacPraksaP1.Services.Interfaces;

namespace TiacPraksaP1.Services.Service
{
    public class UserService:IUserService
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
            return _mapper.Map<UserPostResponse>(await _userRepository.CreateUser(_mapper.Map<User>(user)));
        }

        public async Task<UserDeleteResponse> DeleteUser(int id)
        {
            return _mapper.Map<UserDeleteResponse>(await _userRepository.DeleteUser(id));
        }

        public async Task<IEnumerable<UserGetResponse>> GetAllUsers()
        {
            return _mapper.Map<IEnumerable<UserGetResponse>>(await _userRepository.GetUsers());
        }

        public async Task<UserGetResponse> GetSpecificUser(int id)
        {
            return _mapper.Map<UserGetResponse>(await _userRepository.GetUser(id));
        }

        public async Task<UserPostResponse> UpdateUser(UserPostRequest user)
        {
            return _mapper.Map<UserPostResponse>(await _userRepository.UpdateUser(_mapper.Map<User>(user)));
        }
    }
}
