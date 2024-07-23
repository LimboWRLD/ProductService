using AutoMapper;
using BusinessLogicLayer.DTOs.Delete;
using BusinessLogicLayer.DTOs.Get;
using BusinessLogicLayer.DTOs.Post;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Mappings
{
    public class UserProductProfile : Profile
    {
        public UserProductProfile()
        {
            CreateMap<UserProductGetResponse, UserProduct>();
            CreateMap<UserProduct, UserProductGetResponse>();
            CreateMap<UserProductPostResponse, UserProduct>();
            CreateMap<UserProduct, UserProductPostResponse>();
            CreateMap<UserProduct, UserProductPostRequest>();
            CreateMap<UserProductPostRequest, UserProduct>();
            CreateMap<UserProductDeleteResponse, UserProduct>();
            CreateMap<UserProduct, UserProductDeleteResponse>();
            CreateMap<UserProductPostRequest, UserProductPostResponse>();
            CreateMap<UserProductPostResponse, UserProductPostRequest>();
        }
    }
}
