namespace MyAndroidApp;

public partial class Register : ContentPage
{
	public Register()
	{
		InitializeComponent();
	}

    async void CreateAccountButtonClicked(object send, EventArgs e)
    {
        await Navigation.PopToRootAsync(true);
    }
    async void LoginPageButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync(true);
    }

    
}