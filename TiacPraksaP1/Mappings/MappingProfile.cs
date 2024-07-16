using TiacPraksaP1.DTOs.Get;
using TiacPraksaP1.Models;
using AutoMapper;
using TiacPraksaP1.DTOs.Post;
using TiacPraksaP1.DTOs.Delete;

namespace TiacPraksaP1.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile() {
            CreateMap<ProductGetResponse, Product>();
            CreateMap<Product,ProductGetResponse>();
            CreateMap<ProductPostResponse, Product>();
            CreateMap<Product, ProductPostResponse>();
            CreateMap<Product,ProductPostRequest>();
            CreateMap<ProductPostRequest, Product>();
            CreateMap<ProductDeleteResponse, Product>();
            CreateMap<Product, ProductDeleteResponse>();
            CreateMap<ProductPostRequest, ProductPostResponse>();
            CreateMap<ProductPostResponse, ProductPostRequest>();
        }

        
    }
}
