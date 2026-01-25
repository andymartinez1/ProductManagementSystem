using ProductManagementSystem.DTO;

namespace ProductManagementSystem.Services;

public interface IProductService
{
    public Task<ProductResponse> AddProductAsync(ProductAddRequest? addRequest);

    public Task<List<ProductResponse>> GetAllProducts();

    public Task<ProductResponse> GetProduct(int? id);

    public Task<ProductResponse> UpdateProduct(ProductUpdateRequest? updateRequest);

    public Task<bool> DeleteProduct(int? id);
}
