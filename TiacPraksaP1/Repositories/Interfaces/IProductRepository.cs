using TiacPraksaP1.Models;

namespace TiacPraksaP1.Repositories.Interfaces
{
    public interface IProductRepository
    {
        public Product CreateProduct(Product product);

        public Product UpdateProduct(Product product);

        public Product DeleteProduct(int id);

        public List<Product> GetAllProducts();

        public List<Product> GetSpecificProducts(string name);
    }
}
