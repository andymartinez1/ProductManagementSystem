using ProductManagementSystem.Models;

namespace ProductManagementSystem.DTO;

public class ProductAddRequest
{
    public string? ProductName { get; set; }

    public decimal? Price { get; set; }

    public DateTime? DateAdded { get; set; }

    public bool IsActive { get; set; }

    public int? Quantity { get; set; }

    public Product ToProduct()
    {
        return new Product
        {
            ProductName = ProductName,
            Price = Price,
            DateAdded = DateAdded,
            IsActive = IsActive,
            Quantity = Quantity,
        };
    }
}
