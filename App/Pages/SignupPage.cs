using PocMauiApp.Services;

namespace PocMauiApp.Pages;

/// <summary>
/// This page is without .xaml file. Just for testing how it is done.
/// </summary>
public partial class SignupPage : ContentPage
{
    private readonly IUserService _userService;
    private readonly INavigationService _navigationService;

    private Entry _usernameEntry;
    private Entry _passwordEntry;

    public SignupPage(IUserService userService, INavigationService navigationService)
    {
        _userService = userService;
        _navigationService = navigationService;
        CreateUI();
    }

    private void CreateUI()
    {
        Title = "SignupPage";

        var grid = new Grid
        {
            RowSpacing = 20,
            Margin = new Thickness(20, 150, 20, 40)
        };

        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });

        var image = new Image
        {
            Source = "storelogo.png",
            Aspect = Aspect.AspectFit,
            HeightRequest = 100,
            WidthRequest = 100
        };
        Grid.SetRow(image, 0);
        grid.Children.Add(image);

        var helloLabel = new Label
        {
            Text = "Hello there! Ready to join us?",
            HorizontalOptions = LayoutOptions.Center
        };
        Grid.SetRow(helloLabel, 1);
        grid.Children.Add(helloLabel);

        var frame = new Frame
        {
            Padding = 15,
            BackgroundColor = Colors.White
        };

        var verticalStackLayout = new VerticalStackLayout();
        _usernameEntry = new Entry
        {
            Placeholder = "Username"
        };
        _passwordEntry = new Entry
        {
            Placeholder = "Password",
            IsPassword = true
        };
        verticalStackLayout.Children.Add(_usernameEntry);
        verticalStackLayout.Children.Add(_passwordEntry);
        frame.Content = verticalStackLayout;

        Grid.SetRow(frame, 2);
        grid.Children.Add(frame);

        var signupButton = new Button
        {
            Text = "Sign Up"
        };
        signupButton.Clicked += SignupButtonClicked;
        Grid.SetRow(signupButton, 3);
        grid.Children.Add(signupButton);

        var horizontalStackLayout = new HorizontalStackLayout
        {
            Spacing = 2,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.EndAndExpand
        };
        var accountLabel = new Label
        {
            Text = "Already have an account?"
        };
        var signInLabel = new Label
        {
            Text = "Sign In"
        };
        var tapGestureRecognizer = new TapGestureRecognizer();
        tapGestureRecognizer.Tapped += TapLoginTapped;
        signInLabel.GestureRecognizers.Add(tapGestureRecognizer);
        horizontalStackLayout.Children.Add(accountLabel);
        horizontalStackLayout.Children.Add(signInLabel);

        Grid.SetRow(horizontalStackLayout, 4);
        grid.Children.Add(horizontalStackLayout);

        Content = grid;
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
            var response = await _userService.RegisterUser(_usernameEntry.Text, _passwordEntry.Text);
            if (response)
            {
                await DisplayPromptAsync("Success", $"Account for {_usernameEntry.Text} Created");
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
