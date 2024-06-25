namespace PocMauiApp.Services;

using System.Net.Http.Headers;
using PocMauiApp.Settings;

public interface IApiService
{
    Task<HttpResponseMessage?> GetRequest(string path);
}

public class ApiService : IApiService
{
    private readonly IHttpClientProvider _httpClientProvider;
    private readonly IStorageService _storageService;

    public ApiService(IHttpClientProvider httpClientProvider,
        IStorageService storageService)
    {
        _httpClientProvider = httpClientProvider;
        _storageService = storageService;
    }

    public async Task<HttpResponseMessage?> GetRequest(string path)
    {
        try
        {
            using var client = _httpClientProvider.Create();
            var accessToken = await _storageService.GetAccessToken();
            Console.WriteLine("accessToken: " + accessToken);
            Console.WriteLine("url: " + AppSettings.ApiUrl + path);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            return await client.GetAsync(AppSettings.ApiUrl + path);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message} {ex.ToString()}");            
        }

        return null;
    }

}