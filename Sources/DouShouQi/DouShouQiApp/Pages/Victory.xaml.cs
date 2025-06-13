using Plugin.Maui.Audio;

namespace DouShouQiApp.Pages;

public partial class Victory : ContentPage
{
    private readonly DouShouQiModel.Win _victory;
    private readonly IAudioManager audioManager;

    public Victory(DouShouQiModel.Win victory, IAudioManager audioManager) 
    {
        InitializeComponent();
        _victory = victory;
        DisplayVictoryMessage();
        this.audioManager = audioManager;
    }

    private void DisplayVictoryMessage()
    {
        if (_victory == null)
            return;

        WinnerLabel.Text = _victory.Winner; 
        VictoryTextLabel.Text = _victory.Text;
    }

    private async void GoToHomePage(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }


}