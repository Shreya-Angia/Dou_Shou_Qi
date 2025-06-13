using Plugin.Maui.Audio;

namespace DouShouQiApp.Pages;

public partial class Defeat : ContentPage
{
    private readonly IAudioManager audioManager;
    public Defeat(IAudioManager audioManager)
    {
        InitializeComponent();
        this.audioManager = audioManager;
    }

    private async void GoToHomePage(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }

}