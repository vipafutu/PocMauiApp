namespace PocMauiApp.Services;

using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using PocMauiApp.Dtos;
using PocMauiApp.Settings;

public interface IUserService
{
    Task<bool> RegisterUser(string name, string password);

    Task<bool> LoginUser(string name, string password);
}

internal class UserService : IUserService
{
    private readonly IHttpClientProvider _httpClientProvider;
    private readonly IStorageService _storageService;

    public UserService(IHttpClientProvider httpClientProvider, IStorageService storageService)
    {
        _httpClientProvider = httpClientProvider;
        _storageService = storageService;
    }

    public async Task<bool> RegisterUser(string name, string password)
    {
        Console.WriteLine("RegisterUser");
        var register = new RegisterDto(name, password);
        var body = new StringContent(JsonSerializer.Serialize(register), Encoding.UTF8, "application/json");

        var response = await PostRequest("/users/register", body);
        Console.WriteLine($"reponse {response?.StatusCode}");
        return response?.IsSuccessStatusCode ?? false;
    }

    public async Task<bool> LoginUser(string name, string password)
    {
        Console.WriteLine("RegisterUser");
        var register = new LoginDto(name, password);
        var body = new StringContent(JsonSerializer.Serialize(register), Encoding.UTF8, "application/json");

        var response = await PostRequest("/users/login", body);
        Console.WriteLine($"reponse {response?.StatusCode}");
        if (response == null || !response.IsSuccessStatusCode)
        {
            Console.WriteLine("login failed");
            return false;
        }

        var responseDto = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
        if (responseDto == null)
        {
            Console.WriteLine("responseDto was null");
            return false;
        }

        try
        {
            await _storageService.SetUserData(responseDto.userId, name, responseDto.accessToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception: " + ex.ToString());
            return false;
        }

        return true;


    }

    private async Task<HttpResponseMessage?> PostRequest(string path, StringContent body)
    {
        try
        {
            Console.WriteLine($"PostRequest {AppSettings.ApiUrl + path}");
            using var httpClient = _httpClientProvider.Create();
            return await httpClient.PostAsync(AppSettings.ApiUrl + path, body);
        }
        catch (Exception exception)
        {
            Console.WriteLine("Exception " + exception.ToString());
            Console.WriteLine("Exception " + exception.Message);
            Console.WriteLine("Exception " + exception.InnerException?.ToString());
            return null;
        }
    }
}