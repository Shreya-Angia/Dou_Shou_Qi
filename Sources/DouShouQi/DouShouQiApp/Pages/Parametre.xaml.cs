using DouShouQiModel;
using Plugin.Maui.Audio;

namespace DouShouQiApp.Pages;

public partial class Parametre : ContentPage
{

    private readonly IAudioManager audioManager;
    private IAudioPlayer player; 
    public Parametre(IAudioManager audioManager)
	{
		InitializeComponent();
        this.audioManager = audioManager;
        LoadAudio();

    }

    private async void LoadAudio() 
    {
        player = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("music.mp3"));
        player.Loop = true;
        player.Volume = VolumeSlider.Value; // Valeur initiale 
        player.Play();
        VolumeSlider.Value = player.Volume;  
    }

    private void OnVolumeChanged(object sender, ValueChangedEventArgs e)
    {
        if (player != null)  
        {
            player.Volume = e.NewValue;
        }
    }


    private async void GoBack(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private void ExitGame(object sender, EventArgs e)
    {
        System.Environment.Exit(0);
    }
}