using System.Net.Http.Json;
using UserApi.Blazor.Models.Common;
using UserApi.Blazor.Models.Requests;
using UserApi.Blazor.Models.Responses;

namespace UserApi.Blazor.Services;

public class ProjectService
{
    private readonly HttpClient _httpClient;

    public ProjectService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ApiResponse<List<ProjectResponse>>?> GetProjecAsync()
    {
        return await _httpClient.GetFromJsonAsync<ApiResponse<List<ProjectResponse>>>("/api/projects");
    }

    public async Task<ApiResponse<ProjectResponse>?> CreateProjectAsync(CreateProjectRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/projects", request);

        return await response.Content.ReadFromJsonAsync<ApiResponse<ProjectResponse>>();
    }

    public async Task<ApiResponse<bool>?> DeleteProjectAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"/api/projects/{id}");

        return await response.Content.ReadFromJsonAsync<ApiResponse<bool>>();
    }

    public async Task<ApiResponse<ProjectResponse>?> GetProjectByIdAsync(int projectId)
    {
        return await _httpClient.GetFromJsonAsync<ApiResponse<ProjectResponse>>($"/api/Projects/{projectId}");
    }

    public async Task<ApiResponse<ProjectResponse>?> UpdateProjectAsync(int projectId, UpdateProjectRequest request)
    {
        var response = await _httpClient.PutAsJsonAsync($"/api/Projects/{projectId}", request);
        return await response.Content.ReadFromJsonAsync<ApiResponse<ProjectResponse>>();
    }

}