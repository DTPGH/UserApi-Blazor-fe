using System.Net.Http.Headers;
using System.Net.Http.Json;
using UserApi.Blazor.Models.Common;
using UserApi.Blazor.Models.Requests;
using UserApi.Blazor.Models.Responses;

namespace UserApi.Blazor.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;
    public string? AccessToken { get; private set; }
    public string? RefreshToken { get; private set; }
    public UserResponse? CurrentUser { get; private set; }
    public bool IsAuthenticated => string.IsNullOrWhiteSpace(AccessToken) == true;
    public bool IsAdmin => CurrentUser?.Role == "Admin";
    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ApiResponse<AuthResponse>?> LoginAsync(LoginRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/Auth/login", request);

        var apiresponse = await response.Content.ReadFromJsonAsync<ApiResponse<AuthResponse>>();

        if (response.IsSuccessStatusCode && apiresponse?.Content is not null)
        {
            AccessToken = apiresponse.Content.AccessToken;
            RefreshToken = apiresponse.Content.RefreshToken;
            CurrentUser = apiresponse.Content.User;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

        }

        return apiresponse;
    }

    public async Task LogoutAsync()
    {
        if (IsAuthenticated)
        {
            await _httpClient.PostAsync("/api/Auth/logout", null);
        }
        AccessToken = null;
        RefreshToken = null;
        CurrentUser = null;

        _httpClient.DefaultRequestHeaders.Authorization = null;
    }
}
