using System.ComponentModel.DataAnnotations;

namespace ProductManagementSystem.DTO;

public class ProductResponse
{
    public int Id { get; set; }

    [Display(Name = "Product Name")]
    public string? ProductName { get; set; }

    public string? Category { get; set; }

    public decimal? Price { get; set; }

    [Display(Name = "Date Added")]
    public DateTime? DateAdded { get; set; }

    [Display(Name = "Is Active?")]
    public bool IsActive { get; set; }

    public int? Quantity { get; set; }
}
