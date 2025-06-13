namespace DouShouQiApp.Pages;

public partial class SurePausePage : ContentPage
{
	public SurePausePage()
	{
		InitializeComponent();
	}
    private async void GoBack(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
    private async void GoToSaveAndQuite(object sender, EventArgs e)
    {
        System.Environment.Exit(0);
    }
    private async void GoToQuite(object sender, EventArgs e)
    {
        System.Environment.Exit(0);
    }
}