namespace PocMauiApp.Services;

public interface IStorageService
{
    Task SetUserData(int userId, string username, string accessToken);

    Task<string> GetAccessToken();

    string GetUsername();

    void Reset();
}

internal class StorageService : IStorageService
{
    private const string ACCESS_TOKEN_KEY = "accessToken";
    private const string USER_ID_KEY = "userId";
    private const string USERNAME_KEY = "username";


    public Task SetUserData(int userId, string usernmame, string accessToken)
    {
        // 
        // NOTE: In real life, accessToken and userId should be stored into SecureStorage, 
        // but for this POC I'll store them in Preferences. iOS needs keys etc... for storing 
        // SecureStorage and in the context of this POC it is too much hassle
        //
        // await SecureStorage.SetAsync(ACCESS_TOKEN_KEY, accessToken);
        // await SecureStorage.SetAsync(USER_ID_KEY, userId.ToString());
        //        
        Preferences.Set(ACCESS_TOKEN_KEY, accessToken);
        Preferences.Set(USER_ID_KEY, userId.ToString());
        Preferences.Set(USERNAME_KEY, usernmame);
        return Task.CompletedTask;
    }

    public string GetUsername()
    {
        return Preferences.Get(USERNAME_KEY, "Unknown user");
    }

    public Task<string> GetAccessToken()
    {
        return Task.FromResult<string>(Preferences.Get(ACCESS_TOKEN_KEY, ""));
    }

    public void Reset()
    {
        SecureStorage.RemoveAll();
    }
}