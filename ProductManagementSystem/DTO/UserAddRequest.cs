using System.ComponentModel.DataAnnotations;

namespace ProductManagementSystem.DTO;

public class UserAddRequest
{
    [Required(ErrorMessage = "Username is required")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(
        20,
        MinimumLength = 8,
        ErrorMessage = "{0} must be between {1} and {2} characters long."
    )]
    public string Password { get; set; }
}
