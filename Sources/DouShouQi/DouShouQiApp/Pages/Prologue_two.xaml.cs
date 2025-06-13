namespace DouShouQiApp.Pages;

public partial class Prologue_two : ContentPage
{
	public Prologue_two()
	{
		InitializeComponent(); 
	}
    private async void GoToPrologue_three(object sender, EventArgs e)  
    {
        // Navigate to the Prologue_two page when the button is clicked
        await Navigation.PushAsync(new Prologue_three());  
    }
}