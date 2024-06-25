namespace PocMauiApp.Pages.ProductDetailsPage;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using PocMauiApp.Entities;

public class ProductDetailsPageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private Product _product;    
    private int _productCount = 0;
    private decimal _totalPrice = 0M;
    private bool _isAddtoCartButtonEnabled = false;

    public ProductDetailsPageViewModel()
    {

    }

    public int ProductCount
    {
        get => _productCount;
        set
        {
            if (value >= 0)
            {
                _productCount = value;
                TotalPrice = _product.Price * _productCount;
                OnPropertyChanged();
            }
            IsAddToCartButtonEnabled = value > 0;
        }
    }

    public decimal TotalPrice
    {
        get => _totalPrice;
        private set
        {
            _totalPrice = value;
            OnPropertyChanged();
        }
    }

    public Product Product
    {
        get => _product;
        set
        {
            _product = value;
            OnPropertyChanged();
        }
    }

    public bool IsAddToCartButtonEnabled
    {
        get => _isAddtoCartButtonEnabled;
        set 
        {
            if (value != _isAddtoCartButtonEnabled)
            {
                _isAddtoCartButtonEnabled = value;
                OnPropertyChanged();
            }
        }
    }
    
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        Console.WriteLine("property name: " + propertyName);
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
