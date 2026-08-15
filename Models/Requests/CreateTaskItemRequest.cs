using System.ComponentModel.DataAnnotations;

namespace UserApi.Blazor.Models.Requests;

public class CreateTaskItemRequest
{
    [Required(ErrorMessage = "Task title is required.")]
    [StringLength(255, ErrorMessage = "Task title must be less than 255 characters.")]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "Description must be less than 1000 characters.")]
    public string? Description { get; set; }

    public DateTime? DueDate { get; set; }
}