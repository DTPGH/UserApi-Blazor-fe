using System.ComponentModel.DataAnnotations;
using UserApi.Blazor.Models.Enums;

namespace UserApi.Blazor.Models.Requests;

public class UpdateTaskItemRequest
{
    [Required(ErrorMessage = "Task title is required.")]
    [StringLength(255, ErrorMessage = "Task title must be less than 255 characters.")]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "Description must be less than 1000 characters.")]
    public string? Description { get; set; }

    public TaskItemStatus Status { get; set; }

    public DateTime? DueDate { get; set; }
}