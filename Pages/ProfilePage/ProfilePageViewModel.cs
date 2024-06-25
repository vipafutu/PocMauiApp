namespace PocMauiApp.Pages.ProfilePage;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using PocMauiApp.Services;

public class ProfilePageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private string _username = string.Empty;
    private readonly IStorageService? _storageService;

    public ProfilePageViewModel(IStorageService? storageService)
    {
        _storageService = storageService;
        Refresh();
    }

    public ProfilePageViewModel()
    {

    }

    public string Username
    { 
        get => _username; 
        set
        {
            _username = value;
            OnPropertyChanged();
        }
    }

    public void Refresh()
    {
        Username = _storageService?.GetUsername() ?? string.Empty;
        Console.WriteLine("profile username: " + Username);
    }

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        Console.WriteLine("property name: " + propertyName);
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}