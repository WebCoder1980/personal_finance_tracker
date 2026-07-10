using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace PersonalFinanceTracker.Frontend.Auth;

public sealed class JwtAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _http;
    private readonly IJSRuntime _js;
    private string? _cachedToken;

    public JwtAuthenticationStateProvider(IHttpClientFactory httpClientFactory, IJSRuntime js)
    {
        _http = httpClientFactory.CreateClient("AuthApi");
        _js = js;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return Task.FromResult(CreateAuthenticationState(_cachedToken));
    }

    public async Task InitializeFromStorageAsync()
    {
        _cachedToken = await GetTokenFromStorageAsync();
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task<(bool Success, string? Error)> LoginAsync(string username, string password)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("auth/login", new
            {
                username,
                password
            });

            if (!response.IsSuccessStatusCode)
                return (false, $"Ошибка входа: {(int)response.StatusCode} {response.ReasonPhrase}");

            var requestContext = await response.Content.ReadFromJsonAsync<LoginResponse>();
            if (string.IsNullOrWhiteSpace(requestContext?.Token))
                return (false, "Сервер не вернул token.");

            _cachedToken = requestContext.Token;
            await _js.InvokeVoidAsync("authStorage.setToken", requestContext.Token);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            return (true, null);
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    public async Task LogoutAsync()
    {
        _cachedToken = null;
        await _js.InvokeVoidAsync("authStorage.removeToken");
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task<string?> GetStoredTokenAsync()
    {
        if (!string.IsNullOrWhiteSpace(_cachedToken))
            return _cachedToken;

        _cachedToken = await GetTokenFromStorageAsync();
        return _cachedToken;
    }

    private async Task<string?> GetTokenFromStorageAsync()
    {
        try
        {
            return await _js.InvokeAsync<string?>("authStorage.getToken");
        }
        catch (Exception ex) when (ex is JSException or InvalidOperationException or JSDisconnectedException)
        {
            return null;
        }
    }

    private static AuthenticationState CreateAuthenticationState(string? token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        var claims = ParseClaimsFromJwt(token);
        var identity = new ClaimsIdentity(claims, authenticationType: "jwt", nameType: ClaimTypes.Name, roleType: ClaimTypes.Role);
        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var parts = jwt.Split('.');
        if (parts.Length < 2)
            yield break;

        var json = Encoding.UTF8.GetString(Base64UrlDecode(parts[1]));
        using var document = JsonDocument.Parse(json);

        foreach (var property in document.RootElement.EnumerateObject())
        {
            var value = property.Value.ValueKind switch
            {
                JsonValueKind.String => property.Value.GetString(),
                JsonValueKind.Number => property.Value.GetRawText(),
                JsonValueKind.True => "true",
                JsonValueKind.False => "false",
                _ => property.Value.GetRawText()
            };

            if (value is not null)
            {
                yield return new Claim(property.Name, value);

                if (property.Name is "unique_name" or "sub")
                    yield return new Claim(ClaimTypes.Name, value);

                if (property.Name is "role")
                    yield return new Claim(ClaimTypes.Role, value);
            }
        }
    }

    private static byte[] Base64UrlDecode(string input)
    {
        var padded = input.Replace('-', '+').Replace('_', '/');
        switch (padded.Length % 4)
        {
            case 2: padded += "=="; break;
            case 3: padded += "="; break;
        }

        return Convert.FromBase64String(padded);
    }
}
