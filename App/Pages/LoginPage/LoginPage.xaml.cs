using PocMauiApp.Services;

namespace PocMauiApp.Pages.LoginPage;

public partial class LoginPage : ContentPage
{
    private readonly IUserService _userServices;
    private readonly AppShell _appShell;
    private readonly INavigationService _navigationService;


    public LoginPage(
        LoginPageViewModel viewModel,
        IUserService userService,
        AppShell shell,
        INavigationService navigationService)
    {
        InitializeComponent();
        _userServices = userService;
        _appShell = shell;
        _navigationService = navigationService;
        BindingContext = viewModel;
    }

    private LoginPageViewModel ViewModel
    {
        get => (LoginPageViewModel)BindingContext;
    }

    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        try
        {
            var response = await _userServices.LoginUser(ViewModel.Username, ViewModel.Password);
            if (response)
            {
                Application.Current!.MainPage = _appShell;
            }
            else 
            {
                await DisplayAlert("Login",  "Login failed", "Ok");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Login",  $"Login failed: {ex.Message}", "Ok");
        }
    }

    private void OnTapRegisterTapped(object sender, EventArgs e)
    {
        _navigationService.ToSignupPage(Navigation);
    }
}