using TiacPraksaP1.Models;

namespace TiacPraksaP1.Repositories.Interfaces
{
    public interface IProductRepository
    {
        public Task<Product> CreateProduct(Product product);

        public Task<Product> UpdateProduct(Product product);

        public Task<Product> DeleteProduct(int id);

        public Task<IEnumerable<Product>> GetAllProducts();

        public Task<Product> GetSpecificProduct(int id);
    }
}
