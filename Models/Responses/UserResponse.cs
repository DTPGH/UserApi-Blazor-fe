namespace UserApi.Blazor.Models.Responses;

public class UserResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Age { get; set; }
    public string Role { get; set; } = string.Empty;
}