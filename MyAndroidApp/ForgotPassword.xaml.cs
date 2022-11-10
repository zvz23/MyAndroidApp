namespace MyAndroidApp;

public partial class ForgotPassword : ContentPage
{
	public ForgotPassword()
	{
		InitializeComponent();
	}


	async void ContinueButtonClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new PasswordReset() { Title = "Password Reset" }, true);
	}
}