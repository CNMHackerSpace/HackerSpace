namespace Client.Features.Auth0
{
    public class CustomHttpClient
    {
        IHttpClientFactory _httpClientFactory;
        public CustomHttpClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public HttpClient Anonymous { get { return _httpClientFactory.CreateClient("AnonymousAPI"); } }

        public HttpClient Secure { get { return _httpClientFactory.CreateClient("SecureAPIClient"); } }
    }
}
