using System.Net.Http.Json;
using UserApi.Blazor.Models.Common;
using UserApi.Blazor.Models.Requests;
using UserApi.Blazor.Models.Responses;

namespace UserApi.Blazor.Services;

public class TaskItemService
{
    private readonly HttpClient _httpClient;

    public TaskItemService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ApiResponse<List<TaskItemResponse>>?> GetTasksAsync(int projectId)
    {
        return await _httpClient
            .GetFromJsonAsync<ApiResponse<List<TaskItemResponse>>>($"/api/project/{projectId}/tasks");
    }

    public async Task<ApiResponse<TaskItemResponse>?> CreateTaskAsync(int projectId, CreateTaskItemRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync($"/api/project/{projectId}/tasks", request);

        return await response.Content.ReadFromJsonAsync<ApiResponse<TaskItemResponse>>();
    }

    public async Task<ApiResponse<TaskItemResponse>?> UpdateTaskAsync(int projectId, int taskId, UpdateTaskItemRequest request)
    {
        var response = await _httpClient.PutAsJsonAsync($"/api/project/{projectId}/tasks/{taskId}", request);

        return await response.Content.ReadFromJsonAsync<ApiResponse<TaskItemResponse>>();
    }

    public async Task<ApiResponse<bool>?> DeleteTaskAsync(int projectId, int taskId)
    {
        var response = await _httpClient.DeleteAsync($"/api/project/{projectId}/tasks/{taskId}");

        return await response.Content.ReadFromJsonAsync<ApiResponse<bool>>();
    }

    public async Task<ApiResponse<TaskItemResponse>?> GetTaskByIdAsync(int projectId, int taskId)
    {
        return await _httpClient.GetFromJsonAsync<ApiResponse<TaskItemResponse>>($"/api/project/{projectId}/tasks/{taskId}");
    }
}