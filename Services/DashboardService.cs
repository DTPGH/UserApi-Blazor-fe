using System.Net.Http.Json;
using UserApi.Blazor.Models.Common;
using UserApi.Blazor.Models.Responses;

namespace UserApi.Blazor.Services;

public class DashboardService
{
    private readonly ApiHttpClient _apiHttpClient;
    public DashboardService(ApiHttpClient apiHttpClient)
    {
        _apiHttpClient = apiHttpClient;
    }

    public async Task<ApiResponse<DashboardSummaryResponse>?> GetSummaryAsync()
    {
        return await _apiHttpClient.GetFromJsonAsync<ApiResponse<DashboardSummaryResponse>>("/api/dashboard/summary");
    }
}