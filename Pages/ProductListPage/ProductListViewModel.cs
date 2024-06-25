using System.ComponentModel;
using System.Runtime.CompilerServices;
using PocMauiApp.Entities;
using PocMauiApp.Services;

namespace PocMauiApp.Pages.ProductListPage;

public class ProductListPageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public event EventHandler? SelectedProductChanged;


    private readonly IProductsService _productsService;

    private int _categoryId;

    private List<Product> _products = new List<Product>();
    private Product? _selectedProduct;

    public ProductListPageViewModel(IProductsService productsService)
    {
        _productsService = productsService;
    }

    public ProductListPageViewModel() : this(null)
    {

    }

    public int CategoryId
    { 
        get => _categoryId; 
        set
        {
            _categoryId = value;
            _ = Initialize();
        }
    }

    public Product? SelectedProduct
    {
        get => _selectedProduct;
        set 
        {
            _selectedProduct = value;
            OnPropertyChanged();
            SelectedProductChanged?.Invoke(this, new EventArgs());
        }
    }

    public List<Product> Products
    {
        get => _products;
        set 
        {
            _products = value;
            OnPropertyChanged();
        }
    }

    private async Task Initialize() 
    {
        Console.WriteLine($"Product Category {CategoryId}");
        var products = await _productsService.GetProducts(CategoryId);
        Products = products;
    }

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        Console.WriteLine("property name: " + propertyName);
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
