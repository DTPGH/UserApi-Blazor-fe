using System.ComponentModel.DataAnnotations;

namespace UserApi.Blazor.Models.Requests;

public class CreateUserRequest
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(255, ErrorMessage = "Name must be less than 255 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    [StringLength(255, ErrorMessage = "Email must be less than 255 characters.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(5, ErrorMessage = "Password must be at least 5 characters.")]
    public string Password { get; set; } = string.Empty;

    [Range(0, 120, ErrorMessage = "Age must be between 0 and 120.")]
    public int Age { get; set; }

    [Required(ErrorMessage = "Role is required.")]
    public string Role { get; set; } = "User";
}