using System.Net.Http.Headers;
using System.Net.Http.Json;
using UserApi.Blazor.Models.Common;
using UserApi.Blazor.Models.Requests;
using UserApi.Blazor.Models.Responses;

namespace UserApi.Blazor.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly LocalStorageService _localStorageServices;

    public event Action? OnAuthStateChanged;

    public string? AccessToken { get; private set; }
    public string? RefreshToken { get; private set; }
    public UserResponse? CurrentUser { get; private set; }
    public bool IsInitialized { get; set; }
    public bool IsAuthenticated => string.IsNullOrWhiteSpace(AccessToken) == false;
    public bool IsAdmin => CurrentUser?.Role == "Admin";

    private const string AuthStorageKey = "userapi_auth_state";

    public AuthService(HttpClient httpClient, LocalStorageService localStorageService)
    {
        _httpClient = httpClient;
        _localStorageServices = localStorageService;
    }

    public async Task LoadAuthStateAsync()
    {
        if (IsInitialized == true)
        {
            return;
        }

        try
        {
            var authState = await _localStorageServices
                .GetItemAsync<AuthState>(AuthStorageKey);

            if (authState is null || string.IsNullOrWhiteSpace(authState.AccessToken))
            {
                ClearAuthStateInMemory();
            }
            else
            {
                AccessToken = authState.AccessToken;
                RefreshToken = authState.RefreshToken;
                CurrentUser = authState.User;

                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", AccessToken);
            }


        }
        finally
        {
            IsInitialized = true;
            NotifyAuthStateChanged();
        }
    }

    private void ClearAuthStateInMemory()
    {
        AccessToken = null;
        RefreshToken = null;
        CurrentUser = null;

        _httpClient.DefaultRequestHeaders.Authorization = null;
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

            await _localStorageServices.SetItemAsync(AuthStorageKey, new AuthState
            {
                AccessToken = AccessToken,
                RefreshToken = RefreshToken,
                User = CurrentUser
            });

            NotifyAuthStateChanged();
        }

        return apiresponse;
    }

    public async Task LogoutAsync()
    {
        try
        {
            if (IsAuthenticated)
            {
                await _httpClient.PostAsync("/api/Auth/logout", null);
            }
        }
        finally
        {
            await ClearAuthStateAsync();
        }
    }

    private async Task ClearAuthStateAsync()
    {
        await _localStorageServices.RemoveItemAsync(AuthStorageKey);

        ClearAuthStateInMemory();
        IsInitialized = true;
        NotifyAuthStateChanged();
    }

    private void ClearAuthState()
    {
        AccessToken = null;
        RefreshToken = null;
        CurrentUser = null;

        _httpClient.DefaultRequestHeaders.Authorization = null;

        NotifyAuthStateChanged();
    }

    private void NotifyAuthStateChanged()
    {
        OnAuthStateChanged?.Invoke();
    }
}
