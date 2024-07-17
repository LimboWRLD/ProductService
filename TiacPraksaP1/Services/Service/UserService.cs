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



        public UserPostResponse CreateUser(UserPostRequest user)
        {
            return _mapper.Map<UserPostResponse>(_userRepository.CreateUser(_mapper.Map<User>(user)));
        }

        public UserDeleteResponse DeleteUser(int id)
        {
            return _mapper.Map<UserDeleteResponse>(_userRepository.DeleteUser(id));
        }

        public IEnumerable<UserGetResponse> GetAllUsers()
        {
            return _mapper.Map<IEnumerable<UserGetResponse>>(_userRepository.GetUsers());
        }

        public UserGetResponse GetSpecificUser(int id)
        {
            return _mapper.Map<UserGetResponse>(_userRepository.GetUser(id));
        }

        public UserPostResponse UpdateUser(UserPostRequest user)
        {
            return _mapper.Map<UserPostResponse>(_userRepository.UpdateUser(_mapper.Map<User>(user)));
        }
    }
}
