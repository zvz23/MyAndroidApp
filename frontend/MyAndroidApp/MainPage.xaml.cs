using Android.OS;
using AndroidX.ConstraintLayout.Motion.Widget;
using AndroidX.Lifecycle;
using MyAndroidApp.Models;
using MyAndroidApp.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MyAndroidApp;

public partial class MainPage : ContentPage
{
    public ObservableCollection<Post> LatestPosts { get; set; } = new ObservableCollection<Post>();
    public MainPage()
	{
		InitializeComponent();
        

    }

    protected override async void OnAppearing()
    {
        var apiRequest = new APIRequest();
        int id = int.Parse(Preferences.Get("UserId", "1"));
        List<MyAndroidApp.Services.Image> images = await apiRequest.GetImages(id);
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
    



    async void ImagePickerButtonClicked(object sender, EventArgs e)
	{
        
        try
        {
            var result = await FilePicker.Default.PickAsync();
            if (result != null)
            {
                if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                    result.FileName.EndsWith("jpeg", StringComparison.OrdinalIgnoreCase) ||
                    result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase) ||
                    result.FileName.EndsWith("gif", StringComparison.OrdinalIgnoreCase))
                {
                    var apiRequest = new APIRequest();
                    int id = int.Parse(Preferences.Get("UserId", "1"));
                    Console.WriteLine(result.FullPath);
                    string response = await apiRequest.UploadImage(result.FullPath, id);
                    Console.WriteLine(response);
                    if (response.Contains("success")) 
                    {
                        await DisplayAlert("Success", "Image was uploaded successfully", "OK");
                    }
                    else
                    {
                        Console.WriteLine(response);
                        await DisplayAlert("Error", "Unable to upload image, please try again", "OK");
                    }

                }
            }

        }
        catch (Exception ex)
        {
            string message = ex.Message;
            return;
            // The user canceled or something went wrong
        }
    }




}

