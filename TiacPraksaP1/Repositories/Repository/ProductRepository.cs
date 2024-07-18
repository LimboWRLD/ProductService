using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using TiacPraksaP1.Data;
using TiacPraksaP1.Models;
using TiacPraksaP1.Repositories.Interfaces;
using TiacPraksaP1.Validators;

namespace TiacPraksaP1.Repositories.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;


        public ProductRepository(AppDbContext productContext)
        {
            this._context = productContext;
        }

        public async Task<Product> UpdateProduct(Product product)
        {                
            var oldProduct =await _context.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
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
                _context.SaveChangesAsync();
                return product;
            }
            return product;
        }

        public async Task<Product> DeleteProduct(int id)
        {
                var toDelete = _context.Products.FirstOrDefault(p => p.Id == id);
                if(toDelete != null)
                {
                    _context.Remove(toDelete);
                    await _context.SaveChangesAsync();
                    return toDelete;
                }
                throw new NullReferenceException("Product that you wanted to delete is null");
        }

        public async  Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async  Task<Product> GetSpecificProduct(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
