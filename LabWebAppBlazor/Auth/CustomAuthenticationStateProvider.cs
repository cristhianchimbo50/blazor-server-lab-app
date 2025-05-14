using LabWebAppBlazor.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.JSInterop;
using System.Security.Claims;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ProtectedSessionStorage _sessionStorage;
    private readonly IJSRuntime _jsRuntime;

    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

    public CustomAuthenticationStateProvider(
        ProtectedSessionStorage sessionStorage,
        IJSRuntime jsRuntime)
    {
        _sessionStorage = sessionStorage;
        _jsRuntime = jsRuntime;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            if (_jsRuntime is not IJSInProcessRuntime)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }


            var result = await _sessionStorage.GetAsync<LoginResponseDto>("authToken");

            if (result.Success && result.Value != null)
            {
                Console.WriteLine($"Usuario autenticado desde session: {result.Value.Nombre}");
                var user = CreateClaimsPrincipal(result.Value);
                return new AuthenticationState(user);
            }

            Console.WriteLine("Sesión no encontrada o inválida.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error recuperando sesión: {ex.Message}");
        }

        return new AuthenticationState(_anonymous);
    }

    public async Task SignInAsync(LoginResponseDto loginData)
    {
        Console.WriteLine($"Guardando sesión para: {loginData.Nombre}");

        await _sessionStorage.SetAsync("authToken", loginData);

        var user = CreateClaimsPrincipal(loginData);

        Console.WriteLine($"Usuario autenticado: {user.Identity?.Name}");
        Console.WriteLine($"Rol(es): {string.Join(", ", user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value))}");

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public async Task SignOutAsync()
    {
        await _sessionStorage.DeleteAsync("authToken");
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
    }

    public void ForceReauthentication()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    private ClaimsPrincipal CreateClaimsPrincipal(LoginResponseDto loginData)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, loginData.Nombre ?? "Usuario"),
            new Claim(ClaimTypes.Role, loginData.Rol ?? "Invitado")
        };

        var identity = new ClaimsIdentity(claims, "AppAuth");
        return new ClaimsPrincipal(identity);
    }
}
