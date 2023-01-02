using MyAndroidApp.Services;

namespace MyAndroidApp;

public partial class Profile : ContentPage
{
	public Profile()
	{
		InitializeComponent();
		Email.Text = Preferences.Get("Email", "N/A");
		FullName.Text = Preferences.Get("FirstName", "N/A") + " " + Preferences.Get("LastName", "N/A");
	}

    protected override async void OnAppearing()
    {
        var apiRequest = new APIRequest();
        int id = int.Parse(Preferences.Get("UserId", "1"));
        List<MyAndroidApp.Services.Image> images = await apiRequest.GetImages(id);
        TotalPost.Text = $"Your posts({images.Count})";
        foreach (var image in images)
        {
            StackLayout sl = new StackLayout()
            {
                HeightRequest = 220,
                Margin = new Thickness(0, 0, 0, 10),
                Padding = new Thickness(0)
            };
            Microsoft.Maui.Controls.Image img = new Microsoft.Maui.Controls.Image
            {
                HeightRequest = 220,
                Source = ImageSource.FromUri(new Uri(image.viewImage))

            };

            sl.Add(img);
            PostContainer.Add(sl);
        }
    }
    protected override async void OnDisappearing()
    {
        PostContainer.Children.Clear();
    }

    async void EditProfileButtonClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//EditProfile");
	}
}