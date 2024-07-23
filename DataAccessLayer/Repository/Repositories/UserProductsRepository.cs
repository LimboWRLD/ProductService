using DataAccessLayer.Entities;
using DataAccessLayer.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TiacPraksaP1.Data;

namespace DataAccessLayer.Repository.Repositories
{
    public class UserProductsRepository : IUserProductsRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContext;

        public UserProductsRepository(AppDbContext dbContext, IHttpContextAccessor httpContext)
        {
            _dbContext = dbContext;
            _httpContext = httpContext;
        }

        private string GetUserId() => _httpContext.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        public async Task<UserProduct> CreateUserProduct(UserProduct userProduct)
        {
            if (_dbContext.Products.Any(x=> x.Id == userProduct.ProductId)!=null) 
            {
                userProduct.UserId = GetUserId();
                _dbContext.UserProduts.Add(userProduct);
                await _dbContext.SaveChangesAsync();
                return userProduct;
            }
            return userProduct;
        }

        public async Task<UserProduct> DeleteUserProduct( int ProductId)
        {
            var userId = GetUserId();
            var userProduct = _dbContext.UserProduts.FirstOrDefault(x => x.UserId == userId && x.ProductId==ProductId);
            if(userProduct != null)
            {
                _dbContext.UserProduts.Remove(userProduct);
                await _dbContext.SaveChangesAsync();
                return userProduct;
            }
            return userProduct;
        }

        public async Task<UserProduct> GetUserProduct(int ProductId)
        {
            return await _dbContext.UserProduts.FirstOrDefaultAsync(x => x.UserId == GetUserId() && x.ProductId == ProductId);
        }

        public async Task<IEnumerable<UserProduct>> GetUserProducts()
        {
            return await _dbContext.UserProduts.Where(x=> x.UserId == GetUserId()).ToListAsync();
        }

        public async Task<UserProduct> UpdateUserProduct(int productId, UserProduct updatedUserProduct)
        {
            var oldUserProduct = await _dbContext.UserProduts.FirstOrDefaultAsync(x=> x.ProductId == productId && x.UserId == GetUserId());
            if(oldUserProduct != null)
            {
                oldUserProduct.UserId = updatedUserProduct.UserId;
                await _dbContext.SaveChangesAsync();
                return updatedUserProduct;
            }
            return oldUserProduct;
        }
    }
}
