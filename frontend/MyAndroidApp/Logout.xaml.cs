namespace MyAndroidApp;

public partial class Logout : ContentPage
{
	public Logout()
	{
		InitializeComponent();
		App.Current.MainPage = new NavigationPage(new Login());
    }
}