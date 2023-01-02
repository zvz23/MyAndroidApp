using MyAndroidApp.Services;
using SQLite;

namespace MyAndroidApp;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
        //string dbPath = Path.Combine(FileSystem.AppDataDirectory, Constants.DatabaseName);
        //if (!File.Exists(dbPath))
        //{
        //    SQLiteConnection conn = new SQLiteConnection(dbPath);
        //    conn.Close();
        //}

	}

    async void LoginClicked(object sender, EventArgs e)
    {
        bool is_valid = await _validateFields();
        if (!is_valid)
        {
            return;
        }
        var apiRequest = new APIRequest();
        LoginResponse response = await apiRequest.Login(Email.Text, Password.Text);
        if (response == null)
        {
            await DisplayAlert("Error", "There was an error logging you in", "OK");
            return;
        }
        else if (response.status != null && response.status == 500)
        {
            await DisplayAlert("Error", response.message, "OK");
            return;
        }
        await DisplayAlert("Success", $"id: {response.id}, email: {response.email}", "OK");
        Preferences.Set("UserId", response.id.ToString());
        Preferences.Set("Email", response.email);
        Preferences.Set("FirstName", response.firstName);
        Preferences.Set("LastName", response.firstName);
        App.Current.MainPage = new AppShell();
    }
    async void ForgotPasswordButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ForgotPassword() { Title = "Password Recovery" }, true);
    }
    async void SignupPageButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Register() { Title = "Create an account" },true);
    }

    async Task<bool> _validateFields()
    {
        bool valid = true;

        if (string.IsNullOrWhiteSpace(Email.Text))
        {
            await DisplayAlert("Error", "Please enter your email", "OK");
            return !valid;
        }


        if (string.IsNullOrWhiteSpace(Password.Text))
        {
            await DisplayAlert("Error", "Please enter your password", "OK");
            return !valid;

        }

        return valid;
    }


}