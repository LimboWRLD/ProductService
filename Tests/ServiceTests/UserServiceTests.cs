using AutoMapper;
using FluentAssertions;
using NSubstitute;
using TiacPraksaP1.DTOs.Delete;
using TiacPraksaP1.DTOs.Get;
using TiacPraksaP1.DTOs.Post;
using TiacPraksaP1.Models;
using TiacPraksaP1.Repositories.Interfaces;
using TiacPraksaP1.Repositories.Repository;
using TiacPraksaP1.Services.Interfaces;
using TiacPraksaP1.Services.Service;

namespace Tests.ServiceTests
{
    public class UserServiceTests
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _mapper = Substitute.For<IMapper>();
            _userService = new UserService(_userRepository, _mapper);

        }

        [Fact]
        public async Task Add_User()
        {
            var userPostRequest = new UserPostRequest { UserName = "Test", Email = "Test", Password = "123" };
            var user = new User { UserName = "Test", Email = "Test" };
            var userPostResponse = new UserPostResponse { UserName = "Test", Email = "Test", Id = "1" };

            _mapper.Map<User>(userPostRequest).Returns(user);
            _userRepository.CreateUser(user, userPostRequest.Password).Returns(Task.FromResult(user));
            _mapper.Map<UserPostResponse>(user).Returns(userPostResponse);

            var result = await _userService.CreateUser(userPostRequest);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(userPostResponse);
        }

        [Fact]
        public async Task Add_User_When_ReturnsNull()
        {
            var userPostRequest = new UserPostRequest { Email = "Test", Password = "1", UserName = "Test" };
            var user = new User { };
            _mapper.Map<User>(userPostRequest).Returns(user);
            _userRepository.CreateUser(user, userPostRequest.Password).Returns(Task.FromResult<User>(null));

            var result = await _userService.CreateUser(userPostRequest);

            result.Should().BeNull();
        }

        [Fact]
        public async Task Delete_User_When_Exists()
        {
            var user = new User { Id = "1", UserName = "1231", Email = "Test" };
            var userDeleteRespose = new UserDeleteResponse { UserName = "1231", Id = "1", Email = "Test" };

            _mapper.Map<UserDeleteResponse>(user).Returns(userDeleteRespose);
            _userRepository.DeleteUser(user.Id).Returns(Task.FromResult(user));

            var result = await _userService.DeleteUser(user.Id);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(userDeleteRespose);
        }

        [Fact]
        public async Task Delete_User_When_NonExistant()
        {
            var userId = "1";

            _userRepository.DeleteUser(userId).Returns(Task.FromResult<User>(null));

            var result = await _userService.DeleteUser(userId);

            result.Should().BeNull();
        }

        [Fact]
        public async Task Get_Users_When_Exist()
        {
            var users = new List<User>()
            {
                new User { Id = "1", UserName = "1234", Email="Test" },
                new User { Id = "2", Email = "Test1", UserName = "1234" },
                new User { Id = "3", UserName = "144", Email="2est" }
            };

            var userGetResponses = new List<UserGetResponse>()
            {
                new UserGetResponse { Id = "1", UserName = "1234", Email="Test" },
                new UserGetResponse { Id = "2", Email = "Test1", UserName = "1234" },
                new UserGetResponse { Id = "3", UserName = "144", Email="2est" }
            };

            _mapper.Map<IEnumerable<UserGetResponse>>(users).Returns(userGetResponses);
            _userRepository.GetUsers().Returns(Task.FromResult((IEnumerable<User>)users));


            var result = await _userService.GetAllUsers();

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(userGetResponses);
        }

        [Fact]
        public async Task Get_Users_When_NonExist()
        {
            var users = new List<User>();

            _userRepository.GetUsers().Returns(users);

            var result = await _userService.GetAllUsers();
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(users);
        }

        [Fact]
        public async Task Get_User_When_Exist()
        {
            var user = new User { Id = "1", UserName = "Test", Email = "Test" };
            var userGetResonse = new UserGetResponse { Id = "1", UserName = "Test", Email = "Test" };

            _mapper.Map<UserGetResponse>(user).Returns(userGetResonse);
            _userRepository.GetUser(user.Id).Returns(user);

            var result = await _userService.GetSpecificUser(user.Id);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(userGetResonse);
        }

        [Fact]
        public async Task Get_User_When_NonExistant()
        {
            _userRepository.GetUser("1").Returns(Task.FromResult<User>(null));

            var result = await _userService.GetSpecificUser("1");

            result.Should().BeNull();
        }

        [Fact]
        public async Task Update_User_When_Exist()
        {
            var userPostRequest = new UserPostRequest { UserName = "Test", Email = "Test" };
            var userPostResponse = new UserPostResponse { UserName = "Test", Email = "Test", Id = "1" };
            var user = new User { Id = "1", UserName = "Test", Email = "Test" };

            _mapper.Map<User>(userPostRequest).Returns(user);
            _mapper.Map<UserPostResponse>(user).Returns(userPostResponse);
            _userRepository.UpdateUser(user).Returns(user);

            var result = await _userService.UpdateUser(user.Id,userPostRequest);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(userPostResponse);

        }


        [Fact]
        public async Task Update_User_When_NonExistant()
        {
            var user = new User { UserName = "Test", Email = "Test", Id = "1" };
            var userPostRequest = new UserPostRequest { UserName = "Test", Email = "Test" };


            _userRepository.UpdateUser(user).Returns(Task.FromResult<User>(null));

            var result = await _userService.UpdateUser("1",userPostRequest);

            result.Should().BeNull();
        }
    }
}
