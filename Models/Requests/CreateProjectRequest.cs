using System.ComponentModel.DataAnnotations;

namespace UserApi.Blazor.Models.Requests;

public class CreateProjectRequest
{
    [Required(ErrorMessage = "Project name is required.")]
    [StringLength(255, ErrorMessage = "Project name must be less than 255 characters.")]
    public string Name { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "Description must be less than 1000 characters.")]
    public string? Description { get; set; }
}