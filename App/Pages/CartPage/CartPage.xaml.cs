using PocMauiApp.Entities;
using PocMauiApp.Services;

namespace PocMauiApp.Pages.CartPage;

public partial class CartPage : ContentPage
{
    private readonly ICartService _cartService;

    public CartPage(CartPageViewModel cartPageViewModel, ICartService cartService)
    {
        InitializeComponent();
        _cartService = cartService;
        BindingContext = cartPageViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var viewModel = BindingContext as CartPageViewModel;
        if (viewModel != null)
        {
            viewModel.Refresh();
        }
    }

    private void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as CartPageViewModel;
        if (viewModel == null) return;

        if (sender is ImageButton button && button.BindingContext is CartItem cartItem)
        {
            _cartService.RemoveItem(cartItem);
            viewModel.Refresh();
        }
    }

    private void DecreaseButtonClicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as CartPageViewModel;
        if (viewModel == null) return;

        if (sender is Button button && button.BindingContext is CartItem cartItem)
        {
            cartItem.Count--;
            viewModel.Refresh();
        }
    }


    private void IncreaseButtonClicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as CartPageViewModel;
        if (viewModel == null) return;

        if (sender is Button button && button.BindingContext is CartItem cartItem)
        {
            cartItem.Count++;
            viewModel.Refresh();
        }
    }
}
