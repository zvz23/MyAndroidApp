namespace MyAndroidApp;

public partial class Profile : ContentPage
{
	public Profile()
	{
		InitializeComponent();
	}

	async void EditProfileButtonClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//EditProfile");
	}
}