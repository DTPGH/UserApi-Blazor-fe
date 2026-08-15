using UserApi.Blazor.Models.Responses;

namespace UserApi.Blazor.Models.Common;

public class AuthState
{
    public string AccessToken { get; set; } = string.Empty;

    public string RefreshToken { get; set; } = string.Empty;

    public UserResponse User { get; set; } = new();
}