using MyAndroidApp.Models;
using System.Collections.ObjectModel;

namespace MyAndroidApp;

public partial class MainPage : ContentPage
{
    public ObservableCollection<Post> LatestPosts { get; set; } = new ObservableCollection<Post>();
    public MainPage()
	{
		InitializeComponent();
        LatestPosts.Add(new Post()
        {
            Author = "John Smith",
            Image = "kiyafvvt.jpg"

        });
        LatestPosts.Add(new Post()
        {
            Author = "Ziegfred Zorrilla",
            Image = "pljjgxmd.jpg"

        });
        LatestPosts.Add(new Post()
        {
            Author = "Jane Doe",
            Image = "qqpmeuaj.jpg"

        });
        LatestPosts.Add(new Post()
        {
            Author = "Will Smith",
            Image = "wrqmzgqp.jpg"

        });

    }

	async void ImagePickerButtonClicked(object sender, EventArgs e)
	{
        
        try
        {
            var result = await FilePicker.Default.PickAsync();
            if (result != null)
            {
                if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                    result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
                {
                    using var stream = await result.OpenReadAsync();
                    var image = ImageSource.FromStream(() => stream);
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

