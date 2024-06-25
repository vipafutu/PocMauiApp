using PocMauiApp.Services;

namespace PocMauiApp.Pages.ProductListPage;

public partial class ProductListPage : ContentPage
{
    private readonly INavigationService _navigationService;

    public ProductListPage(
        INavigationService navigationService,
        ProductListPageViewModel productListPageViewModel)
    {
        _navigationService = navigationService;
        InitializeComponent();
        BindingContext = productListPageViewModel;
        productListPageViewModel.SelectedProductChanged += ProductListPageViewModel_SelectedProductChanged;
    }

    public void SetCategoryId(int categoryId)
    {
        var viewModel = BindingContext as ProductListPageViewModel;
        if (viewModel != null)
        {
            viewModel.CategoryId = categoryId;
        }
    }

    private void OnProductTapped(object sender, EventArgs e)
    {
        var viewModel = GetViewModel();
        Console.WriteLine($"sender: {viewModel.SelectedProduct}");
        if (viewModel != null && viewModel.SelectedProduct != null)
        {
            _navigationService.ToProductDetailsPage(Navigation, viewModel.SelectedProduct);
        }
    }

    private void ProductListPageViewModel_SelectedProductChanged(object? sender, EventArgs e)
    {
        var viewModel = GetViewModel();
        if (viewModel == null || viewModel.SelectedProduct == null) return;
        _navigationService.ToProductDetailsPage(Navigation, viewModel.SelectedProduct);     
    }


    private ProductListPageViewModel? GetViewModel() => BindingContext as ProductListPageViewModel;
}
