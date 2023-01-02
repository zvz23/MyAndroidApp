using MyAndroidApp.Services;

namespace MyAndroidApp;

public partial class Register : ContentPage
{
	public Register()
	{
		InitializeComponent();
	}

    async void CreateAccountButtonClicked(object send, EventArgs e)
    {
        bool is_valid = await _validateFields();
        if (!is_valid)
        {
            return;
        }

        var apiRequest = new APIRequest();
        RegisterResponse response = await apiRequest.Register(new RegisterInfo()
        {
            email = Email.Text,
            firstName = FirstName.Text,
            lastName = LastName.Text,
            password = Password.Text,
        });
        if (response == null)
        {
            await DisplayAlert("Error", "There was an error with your registration", "OK");
            return;
        }
        else if (response.status != null && response.status == 500)
        {
            await DisplayAlert("Error", response.message, "OK");
            return;
        }
        else
        {
            await DisplayAlert("Success", "Your account has been created", "OK");
        }

        await Navigation.PopToRootAsync(true);
    }
    async void LoginPageButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync(true);

    }

    async Task<bool> _validateFields()
    {
        bool valid = true;
        if (string.IsNullOrWhiteSpace(FirstName.Text))
        {
            await DisplayAlert("Error", "Please enter your name", "OK");
            return !valid;
        }

        if (string.IsNullOrWhiteSpace(LastName.Text))
        {
            await DisplayAlert("Error", "Please enter your last name", "OK");
            return !valid;
        }

        if (string.IsNullOrWhiteSpace(Email.Text))
        {
            await DisplayAlert("Error", "Please enter your email", "OK");
            return !valid;
        }

        if (Gender.SelectedItem == null)
        {
            await DisplayAlert("Error", "Please select your gender", "OK");
            return !valid;
        }

        if (string.IsNullOrWhiteSpace(Password.Text)) 
        {
            await DisplayAlert("Error", "Please enter your password", "OK");
            return !valid;

        }
        if (string.IsNullOrWhiteSpace(PasswordConfirm.Text))
        {
            await DisplayAlert("Error", "Please confirm your password", "OK");
            return !valid;

        }

        if (Password.Text != PasswordConfirm.Text)
        {
            await DisplayAlert("Error", "Password does not match", "OK");
            return !valid;
        }


        return valid;
    }
    
}