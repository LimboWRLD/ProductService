using BusinessLogicLayer.DTOs.Delete;
using BusinessLogicLayer.DTOs.Get;
using BusinessLogicLayer.DTOs.Post;


namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IUserProductService
    {
        public Task<IEnumerable<UserProductGetResponse>> GetAllUserProducts();

        public Task<UserProductGetResponse> GetSpecificUserProduct(int ProductId);

        public Task<UserProductPostResponse> CreateUserProduct(UserProductPostRequest UserProduct);

        public Task<UserProductDeleteResponse> DeleteUserProduct(int userProductId);

        public Task<UserProductPostResponse> UpdateUserProduct(int ProductId, UserProductPostRequest product);
    }
}
