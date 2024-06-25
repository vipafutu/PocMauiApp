using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using PocMauiApp.Dtos;
using PocMauiApp.Entities;
using PocMauiApp.Settings;

namespace PocMauiApp.Services;

public interface ICategoriesService
{
    Task<List<Category>> GetCategories();
}

internal class CategoriesService : ICategoriesService
{
    private readonly IHttpClientProvider _httpClientProvider;
    private readonly IStorageService _storageService;

    public CategoriesService(IHttpClientProvider httpClientProvider, IStorageService storageService)
    {
        _httpClientProvider = httpClientProvider;
        _storageService = storageService;
    }

    public async Task<List<Category>> GetCategories()
    {
        var response = await GetRequest("/categories");
        if (response == null || !response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Cannot get categories {response?.StatusCode}");
            if (response?.StatusCode == HttpStatusCode.Unauthorized)
            {
                _storageService.Reset();
            }
            return new List<Category>();
        }

        var resultList = await response.Content.ReadFromJsonAsync<List<CategoryDto>>();
        return resultList?.Select(c => new Category 
        {
            Id = c.id,
            Name = c.name,
            ImageUrl = AppSettings.ImageUrl + c.imageUrl
        }).ToList() ?? new List<Category>();
    }

    private async Task<HttpResponseMessage?> GetRequest(string path)
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