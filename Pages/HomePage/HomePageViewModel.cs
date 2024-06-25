namespace PocMauiApp.Pages.HomePage;

using PocMauiApp.Entities;
using PocMauiApp.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public class HomePageViewModel : INotifyPropertyChanged
{
    private readonly IStorageService? _storageService;
    private readonly ICategoriesService? _categoriesService;
    private readonly IProductsService? _productsService;

    private List<Category> _categories = new List<Category>();

    private Category? _selectedCategory;

    private List<Product> _trendingProducts = new List<Product>();

    public event PropertyChangedEventHandler? PropertyChanged;

    public event EventHandler? SelectedCategoryChanged;

    public HomePageViewModel(
        IStorageService? storageService,
        ICategoriesService? categoriesService,
        IProductsService? productsService)
    {        
        _storageService = storageService;
        _categoriesService = categoriesService;
        _productsService = productsService;
        _ = Initialize();        
    }

    public HomePageViewModel() : this(null, null, null)
    {

    }

    public async Task Initialize()
    {
        Username = _storageService!.GetUsername() ?? "Unknown user";
        Categories = await _categoriesService!.GetCategories();
        Categories.ForEach(c => Console.WriteLine(c.Id));

        TrendingProducts = await _productsService!.GetTrendingProducts();
        TrendingProducts.ForEach(c => Console.WriteLine(c.Name));
    }

    public Category? SelectedCategory
    {
        get => _selectedCategory;
        set 
        {
            _selectedCategory = value;
            OnPropertyChanged();
            SelectedCategoryChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public string Username
    {
        get; 
        private set;
    }

    public List<Category> Categories
    {
        get => _categories;
        private set
        {
            _categories = value;
            OnPropertyChanged();
        }
    }

    public List<Product> TrendingProducts
    {
        get => _trendingProducts;
        private set
        {
            _trendingProducts = value;
            OnPropertyChanged();
        }
    }

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        Console.WriteLine("property name: " + propertyName);
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
