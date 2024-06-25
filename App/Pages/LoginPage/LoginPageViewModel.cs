using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PocMauiApp.Pages.LoginPage;

public class LoginPageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private string _username = string.Empty;
    private string _password = string.Empty;

    public LoginPageViewModel()
    {

    }

    public string Username
    {
        get { return _username; }
        set
        {
            _username = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsLoginButtonEnabled));
        }
    }

    public string Password
    {
        get { return _password; }
        set
        {
            _password = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsLoginButtonEnabled));
        }
    }

    public bool IsLoginButtonEnabled
    {
        get
        { 
            return !string.IsNullOrEmpty(_username) && !string.IsNullOrEmpty(_password);
        }
    }
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}