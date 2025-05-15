using LabWebAppBlazor.Components;
using LabWebAppBlazor.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

var builder = WebApplication.CreateBuilder(args);

// 👉 Renderizado interactivo + circuitos detallados
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddCircuitOptions(options => options.DetailedErrors = true);

// 👉 HttpClient para conectarse con la API protegida con JWT
builder.Services.AddHttpClient("Api", client =>
{
    client.BaseAddress = new Uri("https://localhost:7114/api/");
});

// 👉 Servicios de autenticación basada en JWT y sesión
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();


// 👉 Autorización en componentes
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<IApiService, ApiService>();

var app = builder.Build();

// 👉 Configuración de entorno
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

// 👉 Middleware esenciales
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// 👉 Mapeo de componentes de Blazor Server
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
