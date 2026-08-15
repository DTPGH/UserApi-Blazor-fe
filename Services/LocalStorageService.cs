using System.Text.Json;
using Microsoft.JSInterop;

namespace UserApi.Blazor.Services;

public class LocalStorageService
{
    private readonly IJSRuntime _jsRuntime;

    public LocalStorageService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task SetItemAsync<T>(string key, T value)
    {
        var json = JsonSerializer.Serialize(value);

        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, json);
    }

    public async Task<T?> GetItemAsync<T>(string key)
    {
        var json = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", key);

        if (string.IsNullOrWhiteSpace(json))
        {
            return default;
        }

        return JsonSerializer.Deserialize<T>(json);
    }

    public async Task RemoveItemAsync(string key)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
    }
}