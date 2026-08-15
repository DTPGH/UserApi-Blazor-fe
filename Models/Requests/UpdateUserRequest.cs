using System.ComponentModel.DataAnnotations;

namespace UserApi.Blazor.Models.Requests;

public class UpdateUserRequest
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(255, ErrorMessage = "Name must be less than 255 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    [StringLength(255, ErrorMessage = "Email must be less than 255 characters.")]
    public string Email { get; set; } = string.Empty;

    [Range(0, 120, ErrorMessage = "Age must be between 0 and 120.")]
    public int Age { get; set; }

    [StringLength(1000, ErrorMessage = "Description must be less than 1000 characters.")]
    public string? Description { get; set; }
}