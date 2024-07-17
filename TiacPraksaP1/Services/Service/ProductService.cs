using Microsoft.AspNetCore.Mvc.Infrastructure;
using TiacPraksaP1.DTOs.Delete;
using TiacPraksaP1.DTOs.Get;
using TiacPraksaP1.DTOs.Post;
using TiacPraksaP1.Repositories.Interfaces;
using TiacPraksaP1.Services.Interfaces;
using AutoMapper;
using TiacPraksaP1.Models;

namespace TiacPraksaP1.Services.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public ProductPostResponse CreateProduct(ProductPostRequest product)
        {
            return _mapper.Map<ProductPostResponse>(_productRepository.CreateProduct(_mapper.Map<Product>(product))); 
        }

        public ProductPostResponse UpdateProduct(ProductPostRequest product)
        {
            return _mapper.Map<ProductPostResponse>(_productRepository.UpdateProduct(_mapper.Map<Product>(product)));
        }

        public ProductDeleteResponse DeleteProduct(int id)
        {
            return _mapper.Map<ProductDeleteResponse>(_productRepository.DeleteProduct(id));
        }

        public IEnumerable<ProductGetResponse> GetAllProducts()
        {
            
            return _mapper.Map<List<ProductGetResponse>>(_productRepository.GetAllProducts());
        }

        public ProductGetResponse GetSpecificProduct(int id)
        {
             
            return _mapper.Map<ProductGetResponse>(_productRepository.GetSpecificProduct(id));
        }


    }
}
