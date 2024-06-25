using Microsoft.Extensions.Logging;
using PocMauiApp.Pages;
using PocMauiApp.Pages.CartPage;
using PocMauiApp.Pages.HomePage;
using PocMauiApp.Pages.LoginPage;
using PocMauiApp.Pages.ProductDetailsPage;
using PocMauiApp.Pages.ProductListPage;
using PocMauiApp.Pages.ProfilePage;
using PocMauiApp.Services;

namespace PocMauiApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
			.UseMauiMaps();

		builder.Services.AddSingleton<IUserService, UserService>();
		builder.Services.AddSingleton<ICartService, CartService>();
		builder.Services.AddSingleton<IHttpClientProvider, HttpClientProvider>();
		builder.Services.AddSingleton<IStorageService, StorageService>();
		builder.Services.AddTransient<INavigationService, NavigationService>();
		builder.Services.AddTransient<ICategoriesService, CategoriesService>();
		builder.Services.AddTransient<IApiService, ApiService>();
		builder.Services.AddTransient<IProductsService, ProductService>();
		
		builder.Services.AddTransient<SignupPage>();
		builder.Services.AddTransient<LoginPage>();
		builder.Services.AddTransient<LoginPageViewModel>();
		builder.Services.AddTransient<AppShell>();
		builder.Services.AddTransient<CartPage>();
		builder.Services.AddTransient<CartPageViewModel>();
		builder.Services.AddTransient<ProfilePage>();
		builder.Services.AddTransient<ProfilePageViewModel>();
		builder.Services.AddTransient<HomePage>();
		builder.Services.AddTransient<HomePageViewModel>();
		builder.Services.AddTransient<ProductListPage>();
		builder.Services.AddTransient<ProductDetailsPage>();
		builder.Services.AddTransient<ProductDetailsPageViewModel>();
		builder.Services.AddTransient<ProductListPageViewModel>();


#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
