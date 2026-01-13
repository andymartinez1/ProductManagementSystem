using ProductManagementSystem.Data;

namespace ProductManagementSystem.Services;

public class ProductService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ProductService> _logger;

    public ProductService(ApplicationDbContext context, ILogger<ProductService> logger)
    {
        _context = context;
        _logger = logger;
    }
}
