using Plugin.Maui.Audio;

namespace DouShouQiApp.Pages;

public partial class StartPage : ContentPage
{
    private readonly IAudioManager audioManager; 
    public StartPage(IAudioManager audioManager)
    {
        InitializeComponent();
        this.audioManager = audioManager;  
    } 
    private async void GoToPrologue_one(object sender, EventArgs e) 
    {
        await Navigation.PushAsync(new Prologue_one());
        //var audioFile = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("music.mp3"));
        //audioFile.Play();
        //audioFile.Loop = true; // Set the audio to loop 
    }
}