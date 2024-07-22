using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using TiacPraksaP1.Data;
using TiacPraksaP1.Models;

namespace DataAccessLayer.Repository.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;


        public ProductRepository(AppDbContext productContext)
        {
            _context = productContext;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            var oldProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
            if (oldProduct != null)
            {
                oldProduct.Name = product.Name;
                oldProduct.Description = product.Description;
                oldProduct.Price = product.Price;
                await _context.SaveChangesAsync();
                return product;
            }
            return oldProduct;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            if (product != null && !await _context.Products.AnyAsync(p => p.Id == product.Id))
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return product;
            }
            return product;
        }

        public async Task<Product> DeleteProduct(int id)
        {
            var toDelete = _context.Products.FirstOrDefault(p => p.Id == id);
            if (toDelete != null)
            {
                _context.Remove(toDelete);
                await _context.SaveChangesAsync();
                return toDelete;
            }
            return toDelete;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetSpecificProduct(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
