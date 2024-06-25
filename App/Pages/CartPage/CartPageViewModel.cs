namespace PocMauiApp.Pages.CartPage;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using PocMauiApp.Entities;
using PocMauiApp.Services;

public class CartPageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private readonly ICartService? _cartService;

    private List<CartItem> _cartItems = new List<CartItem>();

    public CartPageViewModel(ICartService cartService)
    {
        _cartService = cartService;
        Refresh();
    }

    public CartPageViewModel() : this(null)
    {

    }

    public List<CartItem> CartItems 
    {
        get => _cartItems;
        set 
        {
            _cartItems = value;
            OnPropertyChanged();
        }
    }

    public void Refresh()
    {
        CartItems = _cartService?.Items.ToList() ?? new List<CartItem>();
        CartItems?.ForEach(c => Console.WriteLine("Carm Item " + c.Product?.Name));
    }

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        Console.WriteLine("property name: " + propertyName);
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}