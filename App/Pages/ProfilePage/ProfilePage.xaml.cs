using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace PocMauiApp.Pages.ProfilePage;

public partial class ProfilePage : ContentPage
{
    public ProfilePage(ProfilePageViewModel profilePageViewModel)
    {
        InitializeComponent();
        BindingContext = profilePageViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var geoLocation = await Geolocation.GetLastKnownLocationAsync();
        if (geoLocation != null)
        {
            var location = new Location(geoLocation.Latitude, geoLocation.Longitude)
            {
                IsFromMockProvider = true
            };
            map.MoveToRegion(MapSpan.FromCenterAndRadius(location, Distance.FromKilometers(0.1)));
        }
    }
}
