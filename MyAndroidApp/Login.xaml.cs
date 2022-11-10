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

    void LoginClicked(object sender, EventArgs e)
    {
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

    
}