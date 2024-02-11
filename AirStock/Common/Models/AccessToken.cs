using Microsoft.AspNetCore.Http;
using IdentityModel.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;


namespace AirStock.Common.Models
{
    //public class AccessToken
    //{
    //}

    public interface IGetAccessToken
    {
        public Task<string> AccessTokenAsync();
    }

    public class GetAccessToken : IGetAccessToken
    {
        private string? accessToken = null;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _config;
        public GetAccessToken(IHttpContextAccessor contextAccessor, ILogger<GetAccessToken> logger, IConfiguration config , IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _contextAccessor = contextAccessor;
            _config = config;

            accessToken = contextAccessor.HttpContext?.Request
                                      .Headers["Authorization"]
                                      .FirstOrDefault(h => h.StartsWith("bearer ", StringComparison.InvariantCultureIgnoreCase)) ?? "";

            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                accessToken = accessToken.Replace("bearer ", string.Empty).Replace("Bearer ", string.Empty);
            }
            else
            {
                var client = new HttpClient();

                //var disco = client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
                //{
                //    Address = config["ServiceIssuer:SSOUrl"],
                //    Policy = { RequireHttps = false }
                //}).Result;

                //if (disco.IsError)
                //{
                //    _logger.LogError(disco.Error);
                //    return;
                //}

                // request token
                //var tokenResponse = client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                //{
                //    Address = disco.TokenEndpoint,

                //    ClientId = "IMSInternalAPI",
                //    ClientSecret = "IMSInternalAPIPwd",
                //    Scope = "IdentityServerApi IMSAuthorizer_Core "
                //}).Result;

                //if (tokenResponse.IsError)
                //{
                //    _logger.LogError("Token Response Error :: " + tokenResponse.Error);
                //    return;
                //}

                //accessToken = tokenResponse.AccessToken;
            }

        }
        public async Task<string> AccessTokenAsync()
        {
            return await (Task.FromResult(accessToken));
        }

    }
}


