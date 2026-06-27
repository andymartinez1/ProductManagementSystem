using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Data;
using ProductManagementSystem.DTO.Products;
using ProductManagementSystem.Models;

namespace ProductManagementSystem.Services.Products;

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
        ArgumentNullException.ThrowIfNull(addRequest);

        var product = addRequest.ToProduct();

        await _context.Products.AddAsync(product);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogWarning(ex, "Concurrency conflict while adding product.");
            return ToProductResponse(product);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Database update failed while adding product.");
            return ToProductResponse(product);
        }

        _logger.LogInformation("Product with ID: {productId} added.", product.Id);
        return ToProductResponse(product);
    }

    public async Task<List<ProductResponse>> GetAllProductsAsync()
    {
        var products = await _context.Products
            .AsNoTracking()
            .Select(product => ToProductResponse(product))
            .ToListAsync();

        return products;
    }

    public async Task<ProductResponse?> GetProductAsync(int? id)
    {
        if (id is null)
            return null;

        var product = await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

        return product is null ? null : ToProductResponse(product);
    }

    public async Task<ProductResponse> UpdateProductAsync(ProductUpdateRequest? updateRequest)
    {
        ArgumentNullException.ThrowIfNull(updateRequest);

        var productToUpdate = await _context.Products.FirstOrDefaultAsync(p =>
            p.Id == updateRequest.Id
        );

        if (productToUpdate is null)
            throw new ArgumentException("ID does not exist.");

        productToUpdate.ProductName = updateRequest.ProductName;
        productToUpdate.Sku = updateRequest.Sku;
        productToUpdate.Category = updateRequest.Category;
        productToUpdate.Price = updateRequest.Price;
        productToUpdate.DateAdded = updateRequest.DateAdded;
        productToUpdate.Location = updateRequest.Location;
        productToUpdate.IsActive = updateRequest.IsActive;
        productToUpdate.Quantity = updateRequest.Quantity;
        _context.Products.Update(productToUpdate);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogWarning(ex, "Concurrency conflict while updating product.");
            return ToProductResponse(productToUpdate);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Database update failed while updating product.");
            return ToProductResponse(productToUpdate);
        }

        _logger.LogInformation("Product with ID: {productId} updated.", productToUpdate.Id);
        return ToProductResponse(productToUpdate);
    }

    public async Task<bool> DeleteProductAsync(int? id)
    {
        if (id is null)
            throw new ArgumentNullException(nameof(id));

        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

        if (product is null)
            return false;

        _context.Products.Remove(product);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogWarning(ex, "Concurrency conflict while deleting product.");
            return false;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Database update failed while deleting product.");
            return false;
        }

        _logger.LogInformation("Product with ID: {productId} removed.", product.Id);
        return true;
    }

    private static ProductResponse ToProductResponse(Product product)
    {
        return new ProductResponse
        {
            Id = product.Id,
            ProductName = product.ProductName,
            Sku = product.Sku,
            Category = product.Category,
            Price = product.Price,
            DateAdded = product.DateAdded,
            Location = product.Location,
            IsActive = product.IsActive,
            Quantity = product.Quantity
        };
    }
}