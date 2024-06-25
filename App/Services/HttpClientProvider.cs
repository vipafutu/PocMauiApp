using System.Net;

namespace PocMauiApp.Services;

public interface IHttpClientProvider
{
    HttpClient Create();
}

internal class HttpClientProvider : IHttpClientProvider
{
    public HttpClient Create()
    {
        return new HttpClient(new BypassSslValidationHttpClientHandler());   
    }
}

//
// For bypassing http client security checks. Only for debugging, do not use in production code.
//
class BypassSslValidationHttpClientHandler : HttpClientHandler
{
    public BypassSslValidationHttpClientHandler()
    {
        ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
    }
}


