using AutoMapper;
using TiacPraksaP1.DTOs.Delete;
using TiacPraksaP1.DTOs.Get;
using TiacPraksaP1.DTOs.Post;
using TiacPraksaP1.Models;

namespace TiacPraksaP1.Mappings
{
    public class UserProfile:Profile
    {
        public UserProfile() 
        {
            CreateMap<UserGetResponse, User>();
            CreateMap<User, UserGetResponse>();
            CreateMap<UserPostResponse, User>();
            CreateMap<User, UserPostResponse>();
            CreateMap<User, UserPostRequest>();
            CreateMap<UserPostRequest, User>();
            CreateMap<UserDeleteResponse, User>();
            CreateMap<User, UserDeleteResponse>();
            CreateMap<UserPostRequest, UserPostResponse>();
            CreateMap<UserPostResponse, UserPostRequest>();
        }
    }
}
