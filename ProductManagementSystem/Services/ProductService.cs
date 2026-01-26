using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Data;
using ProductManagementSystem.DTO;
using ProductManagementSystem.DTO.Extensions;

namespace ProductManagementSystem.Services;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ProductService> _logger;

    public ProductService(ApplicationDbContext context, ILogger<ProductService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ProductResponse> AddProductAsync(ProductAddRequest? addRequest)
    {
        if (addRequest is null)
            throw new ArgumentNullException(nameof(addRequest));

        var product = addRequest.ToProduct();

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Product with ID: {productId} added.", product.Id);

        return product.ToProductResponse();
    }

    public async Task<List<ProductResponse>> GetAllProducts()
    {
        return _context.Products.Select(p => p.ToProductResponse()).ToList();
    }

    public async Task<ProductResponse> GetProduct(int? id)
    {
        if (id is null)
            return null;

        var product = await _context.Products.FindAsync(id);

        if (product is null)
            return null;

        return product.ToProductResponse();
    }

    public async Task<ProductResponse> UpdateProduct(ProductUpdateRequest? updateRequest)
    {
        if (updateRequest is null)
            throw new ArgumentNullException(nameof(updateRequest));

        var productToUpdate = await _context.Products.FirstOrDefaultAsync(p =>
            p.Id == updateRequest.Id
        );

        if (productToUpdate is null)
            throw new ArgumentException("ID does not exist.");

        productToUpdate.ProductName = updateRequest.ProductName;
        productToUpdate.Category = updateRequest.Category;
        productToUpdate.Price = updateRequest.Price;
        productToUpdate.DateAdded = updateRequest.DateAdded;
        productToUpdate.IsActive = updateRequest.IsActive;
        productToUpdate.Quantity = updateRequest.Quantity;

        _context.Products.Update(productToUpdate);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Product with ID: {productId} updated.", productToUpdate.Id);

        return productToUpdate.ToProductResponse();
    }

    public async Task<bool> DeleteProduct(int? id)
    {
        if (id is null)
            throw new ArgumentNullException(nameof(id));

        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

        if (product is null)
            return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Product with ID: {productId} removed.", product.Id);

        return true;
    }
}
