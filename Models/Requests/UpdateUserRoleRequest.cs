using System.ComponentModel.DataAnnotations;

namespace UserApi.Blazor.Models.Requests;

public class UpdateUserRoleRequest
{
    [Required]
    public string Role { get; set; } = "User";
}