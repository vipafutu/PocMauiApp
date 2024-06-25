using PocMauiApp.Pages;
using PocMauiApp.Pages.HomePage;
using PocMauiApp.Services;

namespace PocMauiApp;

public partial class App : Application
{

	public App(IServiceProvider serviceProvider)
	{
		InitializeComponent();
		MainPage = serviceProvider.GetService<AppShell>();
		_ = InitializeAsync(serviceProvider);
	}

	private async Task InitializeAsync(IServiceProvider serviceProvider)
	{
		var storageService = serviceProvider.GetService<IStorageService>();
		var navigationService = serviceProvider.GetService<INavigationService>();
		var accessToken = await storageService!.GetAccessToken();
		Console.WriteLine($"accessToken: {accessToken}");
		if (string.IsNullOrEmpty(accessToken))
		{
			MainPage = new NavigationPage(serviceProvider.GetRequiredService<LoginPage>());
		}
	}
}
