namespace MyAndroidApp;

public partial class EditProfile : ContentPage
{
	public EditProfile()
	{
		InitializeComponent();
	}

	async void SaveProfileButtonClicked(object sender, EventArgs e)
	{
		await DisplayAlert("Edit Profile", "Profile has been successfully edited", "OK");
		
		await Shell.Current.GoToAsync("//Home");
	}
}