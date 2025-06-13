using Plugin.Maui.Audio;

namespace DouShouQiApp.Pages; 

public partial class Prologue_one : ContentPage
{
 
    public Prologue_one()
	{
		InitializeComponent(); 
    }

	private async void GoToPrologue_two(object sender, EventArgs e)
    {
        // Navigate to the Prologue_two page when the button is clicked
        await Navigation.PushAsync(new Prologue_two());
    }
}