namespace DouShouQiApp.Pages;

public partial class Prologue_three : ContentPage
{
	public Prologue_three() 
	{
        InitializeComponent();
    }
    private async void GoToHomePage(object sender, EventArgs e)
    {
        // Navigate to the HomePage when the button is clicked 
        await Shell.Current.GoToAsync("//HomePage");
    }
}