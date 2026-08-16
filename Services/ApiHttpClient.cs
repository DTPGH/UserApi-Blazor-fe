using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;

namespace UserApi.Blazor.Services;

public class ApiHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly AuthService _authService;
    private readonly NavigationManager _navigationManager;

    public ApiHttpClient(HttpClient httpClient, AuthService authService, NavigationManager navigationManager)
    {
        _httpClient = httpClient;
        _authService = authService;
        _navigationManager = navigationManager;
    }

    public async Task<HttpResponseMessage> SendAsync(Func<HttpRequestMessage> requestFactory)
    {
        var request = requestFactory();
        var response = await _httpClient.SendAsync(request);

        if (response.StatusCode != HttpStatusCode.Unauthorized)
        {
            return response;
        }

        response.Dispose();

        var refreshSuccess = await _authService.TryRefreshTokenAsync();

        if (!refreshSuccess)
        {
            _navigationManager.NavigateTo("/login");
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }

        var retryRequest = requestFactory();
        return await _httpClient.SendAsync(retryRequest);
    }

    public async Task<T?> GetFromJsonAsync<T>(string url)
    {
        var response = await SendAsync(() => new HttpRequestMessage(HttpMethod.Get, url));

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return default;
        }
        return await response.Content.ReadFromJsonAsync<T>();
    }

    public async Task<T?> PostAsJsonAsync<TRequest, T>(string url, TRequest requestBody)
    {
        var response = await SendAsync(() => CreateJsonRequest(HttpMethod.Post, url, requestBody));
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return default;
        }
        return await response.Content.ReadFromJsonAsync<T>();
    }

    public async Task<T?> PutAsJsonAsync<TRequest, T>(
        string url,
        TRequest requestBody)
    {
        var response = await SendAsync(() =>
            CreateJsonRequest(HttpMethod.Put, url, requestBody));

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return default;
        }

        return await response.Content.ReadFromJsonAsync<T>();
    }

    public async Task<T?> PatchAsJsonAsync<TRequest, T>(
        string url,
        TRequest requestBody)
    {
        var response = await SendAsync(() =>
            CreateJsonRequest(HttpMethod.Patch, url, requestBody));

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return default;
        }

        return await response.Content.ReadFromJsonAsync<T>();
    }

    public async Task<T?> PatchAsync<T>(string url)
    {
        var response = await SendAsync(() =>
            new HttpRequestMessage(HttpMethod.Patch, url));

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return default;
        }

        return await response.Content.ReadFromJsonAsync<T>();
    }

    public async Task<T?> DeleteAsync<T>(string url)
    {
        var response = await SendAsync(() =>
            new HttpRequestMessage(HttpMethod.Delete, url));

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return default;
        }

        return await response.Content.ReadFromJsonAsync<T>();
    }

    private static HttpRequestMessage CreateJsonRequest<TRequest>(HttpMethod method, string url, TRequest? requestBody)
    {
        return new HttpRequestMessage(method, url)
        {
            Content = JsonContent.Create(requestBody)
        };
    }
}