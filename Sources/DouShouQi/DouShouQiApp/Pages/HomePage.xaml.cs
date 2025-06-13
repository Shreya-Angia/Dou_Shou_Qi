using DouShouQiModel;
using Plugin.Maui.Audio;

namespace DouShouQiApp.Pages
{
    public partial class HomePage : ContentPage
    {
        private readonly IAudioManager audioManager; 
        private IAudioPlayer player;
        private bool isPlaying = true; 
        public HomePage(IAudioManager audioManager)  
        { 
            InitializeComponent(); 
            this.audioManager = audioManager; 
            LoadAudio(); // Load the audio when the page is initialized      

        }
        private async void LoadAudio()       
        {
            player = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("music.mp3"));
            player.Play(); 
            isPlaying = true;
            PlayPauseButton.IsEnabled = true; // Activates the button when audio is loaded
        }


        private void OnPlayPauseClicked(object sender, EventArgs e)    
        {
            if (player == null) return; 

            if (isPlaying)
            {
                player.Pause(); // Stops the music

            }
            else 
            {
                player.Play(); //PLays the music  

            }

            isPlaying = !isPlaying; 

            UpdateVolumeButton();  
        }

        private void UpdateVolumeButton()   
        {
            var imageSource = isPlaying ?  "volume_white.png" : "mute_white.png"; 
            PlayPauseButton.ImageSource = imageSource; 
        }




        private async void GoToCredits(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Credits());
        }
        private async void GoToSettings(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Parametre(audioManager));
        }
        private async void GoToCharacter(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Character());
        }
        private async void GoToLeaderboard(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Leaderboard());
        }
        private async void GoToSelectGame(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SelectGame());
        }
        private async void GoToRules(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Regles());
        }

        private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {

        }
    }
}