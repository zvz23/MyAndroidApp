namespace MyAndroidApp;

public partial class PasswordReset : ContentPage
{
	public PasswordReset()
	{
		InitializeComponent();
	}

	async void PasswordResetButtonClicked(object sender, EventArgs e)
	{
		await Navigation.PopToRootAsync(true);
	}
}