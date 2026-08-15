using System.Net.Http.Json;
using UserApi.Blazor.Models.Common;
using UserApi.Blazor.Models.Responses;

namespace UserApi.Blazor.Services;

public class DashboardService
{
    private readonly HttpClient _httpClient;
    public DashboardService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ApiResponse<DashboardSummaryResponse>?> GetSummaryAsync()
    {
        return await _httpClient.GetFromJsonAsync<ApiResponse<DashboardSummaryResponse>>("/api/dashboard/summary");
    }
}