using DouShouQiModel;

namespace DouShouQiApp.Pages;

public partial class PausePage : ContentPage
{
	public PausePage()
	{
		InitializeComponent();
	}
    private async void GoToRules(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Regles());
    }

    private async void GoBack(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
    private async void GoToSurePause(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SurePausePage());
    }
}