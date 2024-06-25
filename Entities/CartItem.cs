namespace PocMauiApp.Entities;

public class CartItem
{
    public Guid Id { get; } = Guid.NewGuid();

    public Product? Product { get; init; }
    
    public int Count { get; set; }
    
    public decimal TotaPrice
    {
        get 
        {
            return Count * (Product?.Price ?? 0);
        }
    }
};