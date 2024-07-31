using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiacPraksaP1.Models;

namespace DataAccessLayer.Repository.Interfaces
{
    public interface IUserProductsRepository
    {
        public Task<UserProduct> CreateUserProduct(UserProduct userProduct);

        public Task<UserProduct> DeleteUserProduct( int ProductId);

        public Task<UserProduct> UpdateUserProduct(int productId,UserProduct updatedUserProduct);

        public Task<UserProduct> GetUserProduct( int ProductId);

        public Task<IEnumerable<UserProduct>> GetUserProducts();
    }
}
