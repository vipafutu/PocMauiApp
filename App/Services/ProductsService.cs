namespace PocMauiApp.Services;

using System.Net.Http.Json;
using PocMauiApp.Dtos;
using PocMauiApp.Entities;
using PocMauiApp.Settings;

public interface IProductsService
{
    Task<List<Product>> GetTrendingProducts();

    Task<List<Product>> GetProducts(int categoryId);

    Task<List<Product>> GetProduct(int productId);
}

internal class ProductService : IProductsService
{
    private readonly IApiService _apiService;

    public ProductService(IApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<List<Product>> GetProducts(int categoryId)
    {
        var response = await _apiService.GetRequest($"/products?productType=category&categoryId={categoryId}");
        if (response == null)
        {
            return new List<Product>();
        }

        var products = await response.Content.ReadFromJsonAsync<List<ProductDto>>();
        return products?.
            Select(ToProduct)
            .ToList() ?? new List<Product>();

    }

    public async Task<List<Product>> GetProduct(int productId)
    {
        var response = await _apiService.GetRequest($"/products/{productId}");
        if (response == null)
        {
            return new List<Product>();
        }

        var products = await response.Content.ReadFromJsonAsync<List<ProductDto>>();
        return products?.
            Select(ToProduct)
            .ToList() ?? new List<Product>();

    }

    public async Task<List<Product>> GetTrendingProducts()
    {
        var response = await _apiService.GetRequest("/products?productType=trending");
        if (response == null)
        {
            return new List<Product>();
        }

        var products = await response.Content.ReadFromJsonAsync<List<ProductDto>>();
        return products?.
            Select(ToProduct)
            .ToList() ?? new List<Product>();
    }

    private Product ToProduct(ProductDto dto) => 
        new Product(dto.ProductId, dto.Name, AppSettings.ImageUrl + dto.ImageUrl, dto.Price);
}