using UserApi.Blazor.Models.Common;
using UserApi.Blazor.Models.Requests;
using UserApi.Blazor.Models.Responses;

namespace UserApi.Blazor.Services;

public class ProfileService
{
    private readonly ApiHttpClient _apiHttpClient;

    public ProfileService(ApiHttpClient apiHttpClient)
    {
        _apiHttpClient = apiHttpClient;
    }

    public async Task<ApiResponse<UserResponse>?> GetMeAsync()
    {
        return await _apiHttpClient.GetFromJsonAsync<ApiResponse<UserResponse>>("/api/Auth/me");
    }

    public async Task<ApiResponse<UserResponse>?> UpdateMyProfileAsync(int userId, UpdateUserRequest request)
    {
        return await _apiHttpClient.PutAsJsonAsync<UpdateUserRequest, ApiResponse<UserResponse>>($"/api/User/{userId}", request);
    }
}