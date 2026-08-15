using System.Net.Http.Json;
using UserApi.Blazor.Models.Common;
using UserApi.Blazor.Models.Requests;
using UserApi.Blazor.Models.Responses;

namespace UserApi.Blazor.Services;

public class UserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ApiResponse<PagedResult<UserResponse>>?> GetUsersAsync(UserQueryParameters query)
    {
        var url = "/api/user" +
                    $"?PageNumber={query.PageNumber}" +
                    $"&PageSize={query.PageSize}" +
                    $"&SortBy={Uri.EscapeDataString(query.SortBy)}" +
                    $"&Desc={query.SortDirection}";

        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            url += $"&SearchTerm={Uri.EscapeDataString(query.Search)}";
        }

        return await _httpClient.GetFromJsonAsync<ApiResponse<PagedResult<UserResponse>>>(url);
    }

    public async Task<ApiResponse<UserResponse>?> CreateUserAsync(CreateUserRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/user", request);

        return await response.Content.ReadFromJsonAsync<ApiResponse<UserResponse>>();
    }

    public async Task<ApiResponse<UserResponse>?> UpdateUserRoleAsync(int userId, UpdateUserRoleRequest request)
    {
        var response = await _httpClient.PatchAsJsonAsync($"/api/user/{userId}/role", request);

        return await response.Content.ReadFromJsonAsync<ApiResponse<UserResponse>>();
    }

    public async Task<ApiResponse<bool>?> DeleteUserAsync(int userId)
    {
        var response = await _httpClient.DeleteAsync($"/api/user/{userId}");

        return await response.Content
            .ReadFromJsonAsync<ApiResponse<bool>>();
    }

    public async Task<ApiResponse<bool>?> RestoreUserAsync(int userId)
    {
        var response = await _httpClient.PatchAsync($"/api/user/{userId}/restore", null);

        return await response.Content.ReadFromJsonAsync<ApiResponse<bool>>();
    }

    public async Task<ApiResponse<List<UserResponse>>?> GetDeletedUsersAsync()
    {
        return await _httpClient.GetFromJsonAsync<ApiResponse<List<UserResponse>>>("/api/user/deleted");
    }

    public async Task<ApiResponse<UserResponse>?> GetUserByIdAsync(int userId)
    {
        return await _httpClient.GetFromJsonAsync<ApiResponse<UserResponse>>($"/api/user/{userId}");
    }

    public async Task<ApiResponse<UserResponse>?> UpdateUserAsync(int userId, UpdateUserRequest request)
    {
        var response = await _httpClient.PutAsJsonAsync($"/api/user/{userId}", request);

        return await response.Content
            .ReadFromJsonAsync<ApiResponse<UserResponse>>();
    }
}