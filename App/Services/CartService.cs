using System.Collections.ObjectModel;
using PocMauiApp.Entities;

namespace PocMauiApp.Services;

public interface ICartService
{
    ICollection<CartItem> Items { get; }

    void AddItem(CartItem item);

    void RemoveItem(CartItem item);
}

internal class CartService : ICartService
{
    private readonly List<CartItem> _items = new List<CartItem>();

    public ICollection<CartItem> Items
    {
        get { return _items; }
    }

    public void AddItem(CartItem cartItem)
    {
        _items.Add(cartItem);
        Console.WriteLine("cart item count " + _items.Count);
    }

    public void RemoveItem(CartItem cartItem)
    {
        _items.RemoveAll(i => i.Id == cartItem.Id);
    }
}