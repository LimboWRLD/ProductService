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
            var response = _mapper.Map<ProductPostResponse>(_productRepository.CreateProduct(_mapper.Map<Product>(product)));
            if(response == null)
            {
                return null;
            }
            return response;
        }

        public ProductPostResponse UpdateProduct(ProductPostRequest product)
        {
            var response = _mapper.Map<ProductPostResponse>(_productRepository.UpdateProduct(_mapper.Map<Product>(product)));
            if (response == null)
            {
                return null;
            }
            return response;
        }

        public ProductDeleteResponse DeleteProduct(int id)
        {
            var response = _mapper.Map<ProductDeleteResponse>(_productRepository.DeleteProduct(id));
            if (response == null)
            {
                return null;
            }
            return response;
        }

        public List<ProductGetResponse> GetAllProducts()
        {
            var response = _mapper.Map<List<ProductGetResponse>>(_productRepository.GetAllProducts());
            if (response == null)
            {
                return null;
            }
            return response;
        }

        public List<ProductGetResponse> GetSpecificProducts(string name)
        {
            var response  = _mapper.Map<List<ProductGetResponse>>(_productRepository.GetSpecificProducts(name));
            if (response == null)
            {
                return null;
            }
            return response;
        }


    }
}
