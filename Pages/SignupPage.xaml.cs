
using PocMauiApp.Services;

namespace PocMauiApp.Pages;


public partial class SignupPage : ContentPage
{
    private readonly IUserService _userService;
    private readonly INavigationService _navigationService;

    public SignupPage(IUserService userService, INavigationService navigationService)
    {
        InitializeComponent();
        _userService = userService;
        _navigationService = navigationService;
    }

    private void TapLoginTapped(object sender, EventArgs e)
    {
        _navigationService.ToLoginPage(Navigation);
    }

    private async void SignupButtonClicked(object sender, EventArgs e)
    {
        Console.WriteLine("Testing");
        try
        {
            var response = await _userService.RegisterUser(Username.Text, Password.Text);
            if (response)
            {
                await DisplayPromptAsync("Success", $"Account for {Username.Text} Created");
                await _navigationService.ToLoginPage(Navigation);
            }
            else 
            {
                await DisplayAlert("Failed", "Account was not created", "Ok");
            }
            Console.WriteLine($"Response {response}");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Failed", $"Account was not created: {ex.Message}", "Ok");
           Console.WriteLine($"Exception {ex.Message}");   
        }         
    }
}
