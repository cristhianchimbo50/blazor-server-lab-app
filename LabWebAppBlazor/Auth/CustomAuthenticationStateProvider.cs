using LabWebAppBlazor.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;



public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ProtectedSessionStorage _sessionStorage;
    private readonly NavigationManager _navigationManager;
    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

    private const string SessionKey = "authToken";

    public CustomAuthenticationStateProvider(ProtectedSessionStorage sessionStorage, NavigationManager navigationManager)
    {
        _sessionStorage = sessionStorage;
        _navigationManager = navigationManager;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (_navigationManager.Uri.StartsWith("about:", StringComparison.OrdinalIgnoreCase))
        {
            // En prerendering, devolver anónimo
            return new AuthenticationState(_anonymous);
        }


        try
        {
            var result = await _sessionStorage.GetAsync<LoginResponseDto>(SessionKey);

            if (result.Success && result.Value != null)
            {
                Console.WriteLine($"✅ Usuario autenticado desde sesión: {result.Value.Nombre}");
                var user = CreateClaimsPrincipal(result.Value);
                return new AuthenticationState(user);
            }

            Console.WriteLine("⚠️ Sesión no encontrada o inválida.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error recuperando sesión: {ex.Message}");
        }

        return new AuthenticationState(_anonymous);
    }
    public async Task SignInAsync(LoginResponseDto loginData)
    {
        await _sessionStorage.SetAsync(SessionKey, loginData);

        var user = CreateClaimsPrincipal(loginData);

        Console.WriteLine($"👤 Usuario autenticado: {user.Identity?.Name}");
        Console.WriteLine($"🛡️ Rol(es): {string.Join(", ", user.FindAll(ClaimTypes.Role).Select(c => c.Value))}");

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public async Task SignOutAsync()
    {
        await _sessionStorage.DeleteAsync(SessionKey);
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
            new Claim(ClaimTypes.Name, loginData.CorreoUsuario ?? "usuario@desconocido.com"),
            new Claim(ClaimTypes.Role, loginData.Rol ?? "Invitado")
        };

        var identity = new ClaimsIdentity(claims, "AppAuth");
        return new ClaimsPrincipal(identity);
    }
}
