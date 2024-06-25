using PocMauiApp.Entities;
using PocMauiApp.Services;

namespace PocMauiApp.Pages.ProductDetailsPage;

public partial class ProductDetailsPage : ContentPage
{
    private readonly ICartService _cartService;

    public ProductDetailsPage(ProductDetailsPageViewModel productDetailsPageViewModel, ICartService cartService)
    {
        InitializeComponent();
        _cartService = cartService;
        BindingContext = productDetailsPageViewModel;
    }

    public void SetProduct(Product product)
    {
        var viewModel = BindingContext as ProductDetailsPageViewModel;
        if (viewModel != null)
        {
            viewModel.Product = product;
        }
    }

    private void RemoveButtonClicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as ProductDetailsPageViewModel;
        if (viewModel != null)
        {
            viewModel.ProductCount -= 1;
        }
    }

    private void AddButtonClicked(object sender, EventArgs e)
    {   
        var viewModel = BindingContext as ProductDetailsPageViewModel;
        if (viewModel != null)
        {
            Console.WriteLine($"Add {viewModel.ProductCount}");
            viewModel.ProductCount += 1;
        }
    }

    private void AddToCartButtonClicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as ProductDetailsPageViewModel;
        if (viewModel == null) return;

        _cartService.AddItem(new CartItem
        {
            Product = viewModel.Product,
            Count = viewModel.ProductCount
        });

        Console.WriteLine("Clicked!");
    }

    private void OnProductTapped(object sender, EventArgs e)
    {
        Console.WriteLine("OnProdctTapped");
    }
}
