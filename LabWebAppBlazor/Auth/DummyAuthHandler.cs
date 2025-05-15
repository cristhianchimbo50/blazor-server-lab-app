using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

public class DummyAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public DummyAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock)
        : base(options, logger, encoder, clock)
    { }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // No autentica a nadie, es solo para evitar excepciones internas
        return Task.FromResult(AuthenticateResult.NoResult());
    }
}
