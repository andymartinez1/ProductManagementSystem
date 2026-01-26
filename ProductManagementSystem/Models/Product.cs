using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagementSystem.Models;

public class Product
{
    public int Id { get; set; }

    public string? ProductName { get; set; }

    public string? Category { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Price { get; set; }

    public int? Quantity { get; set; }

    public DateTime? DateAdded { get; set; }

    public bool IsActive { get; set; }
}
