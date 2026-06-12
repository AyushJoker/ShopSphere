using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace ShopSphere.OrderService.Infrastructure.Http;

public class JwtForwardingHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JwtForwardingHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken cancellationToken)
    {
        var authorizationHeader =_httpContextAccessor.HttpContext?
                .Request.Headers["Authorization"]
                .ToString();

        if (!string.IsNullOrWhiteSpace(authorizationHeader))
        {
            request.Headers.Authorization =
                AuthenticationHeaderValue.Parse(
                    authorizationHeader);
        }

        return await base.SendAsync(request,cancellationToken);
    }
}