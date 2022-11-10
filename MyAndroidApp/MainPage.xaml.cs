namespace MyAndroidApp;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();

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

