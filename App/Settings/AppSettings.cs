namespace PocMauiApp.Settings;

internal static class AppSettings
{
    public static string ApiUrl
    {
        get => $"{Url}api";
    }

    public static string ImageUrl
    {
        get => $"http://10.0.2.2:5273/";
        // get => "http://localhost:5273/";
    }

    public static string Url
    {
        get => "https://10.0.2.2:7205/";
        // get => "https://localhost:7205/";
        // get => "http://10.0.2.2:5273/";
        // get => "https://localhost:7205/";
    }

    public static async Task<string> GetToken()
    {
        try
        {
            var token = await SecureStorage.GetAsync("token");
            return token ?? string.Empty;
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }
}