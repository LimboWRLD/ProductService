using TiacPraksaP1.DTOs.Delete;
using TiacPraksaP1.DTOs.Get;
using TiacPraksaP1.DTOs.Post;
using TiacPraksaP1.Services.Interfaces;
using AutoMapper;
using TiacPraksaP1.Models;
using DataAccessLayer.Repositories.Interfaces;

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

        public async Task<ProductPostResponse> CreateProduct(ProductPostRequest product)
        {
            return _mapper.Map<ProductPostResponse>(await _productRepository.CreateProduct(_mapper.Map<Product>(product))); 
        }

        public async Task<ProductPostResponse> UpdateProduct(int id,ProductPostRequest product)
        {
            var productToUpdate = _mapper.Map<Product>(product);
            productToUpdate.Id = id;
            return _mapper.Map<ProductPostResponse>(await _productRepository.UpdateProduct(productToUpdate));
          
        }

        public async Task<ProductDeleteResponse> DeleteProduct(int id)
        {
            return _mapper.Map<ProductDeleteResponse>(await _productRepository.DeleteProduct(id));
        }

        public async Task<IEnumerable<ProductGetResponse>> GetAllProducts()
        {
            
            return _mapper.Map<List<ProductGetResponse>>(await _productRepository.GetAllProducts());
        }

        public async Task<ProductGetResponse> GetSpecificProduct(int id)
        {
             
            return _mapper.Map<ProductGetResponse>(await _productRepository.GetSpecificProduct(id));
        }


    }
}
