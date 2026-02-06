using Microsoft.Extensions.Logging;
using Moq;
using ProductManagementSystem.DTO;
using ProductManagementSystem.Services;
using ProductManagementSystem.UnitTests.TestHelpers;

namespace ProductManagementSystem.UnitTests;

public class ProductServiceTests
{
    private readonly Mock<ILogger<ProductService>> _loggerMock;

    public ProductServiceTests()
    {
        _loggerMock = new Mock<ILogger<ProductService>>();
    }

    [Fact]
    public async Task AddProductAsync_IfNull_ReturnsArgumentNullException()
    {
        // Arrange
        await using var context = await DbContextTestExtensions.ConfigureDbContext();
        var sut = new ProductService(context, _loggerMock.Object);
        ProductAddRequest? product = null;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
        {
            await sut.AddProductAsync(product);
        });
    }

    [Fact]
    public async Task AddProductAsync_IfNotNull_PersistsToDb()
    {
        // Arrange
        await using var context = await DbContextTestExtensions.ConfigureDbContext();
        var sut = new ProductService(context, _loggerMock.Object);
        ProductAddRequest? product = new ProductAddRequest()
        {
            ProductName = "Test Product",
            Category = "Test Category",
            Price = 19.99m,
            Quantity = 20,
            DateAdded = DateTime.Now.AddDays(-2),
            IsActive = false,
        };

        // Act
        await sut.AddProductAsync(product);

        // Assert
        var productCount = context.Products.Count();
        Assert.Equal(4, productCount);

        var savedProduct = context.Products.FirstOrDefault(p => p.ProductName == "Test Product");
        Assert.NotNull(savedProduct);
        Assert.Equal(product.ProductName, savedProduct.ProductName);
    }

    [Fact]
    public async Task GetAllProductsAsync_ReturnsSeededProducts()
    {
        // Arrange
        await using var context = await DbContextTestExtensions.ConfigureDbContext();
        var sut = new ProductService(context, _loggerMock.Object);

        // Act
        var products = await sut.GetAllProductsAsync();

        // Assert
        Assert.Equal(3, products.Count);
        Assert.Equal("Keyboard", products[0].ProductName);
    }
}
