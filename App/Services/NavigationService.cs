namespace PocMauiApp.Services;

using PocMauiApp.Entities;
using PocMauiApp.Pages;
using PocMauiApp.Pages.HomePage;
using PocMauiApp.Pages.LoginPage;
using PocMauiApp.Pages.ProductDetailsPage;
using PocMauiApp.Pages.ProductListPage;

public interface INavigationService
{
    Task ToLoginPage(INavigation navigation);
    Task ToProductListPage(INavigation navigation, int categoryId);
    Task ToSignupPage(INavigation navigation);
    Task ToHomePage(INavigation navigation);
    Task ToProductDetailsPage(INavigation navigation, Product product);
}

internal class NavigationService : INavigationService
{
    private readonly IServiceProvider _serviceProvider;

    public NavigationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task ToProductDetailsPage(INavigation navigation, Product product)
    {

        var page = _serviceProvider.GetRequiredService<ProductDetailsPage>();
        page.SetProduct(product);
        return navigation.PushAsync(page);
    }

    public Task ToLoginPage(INavigation navigation)
    {
        return navigation.PushAsync(_serviceProvider.GetRequiredService<LoginPage>());
    }

    public Task ToSignupPage(INavigation navigation)
    {
        return navigation.PushAsync(_serviceProvider.GetRequiredService<SignupPage>());
    }

    public Task ToHomePage(INavigation navigation)
    {
        return navigation.PushAsync(_serviceProvider.GetRequiredService<HomePage>());
    }

    public Task ToProductListPage(INavigation navigation, int categoryId)
    {
        var page = _serviceProvider.GetRequiredService<ProductListPage>();
        page.SetCategoryId(categoryId);
        return navigation.PushAsync(page);
    }
}