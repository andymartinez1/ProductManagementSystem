namespace ProductManagementSystem.DTO;

public class ProductResponse
{
    public int Id { get; set; }

    public string? ProductName { get; set; }

    public string? Category { get; set; }

    public decimal? Price { get; set; }

    public DateTime? DateAdded { get; set; }

    public bool IsActive { get; set; }

    public int? Quantity { get; set; }
}
