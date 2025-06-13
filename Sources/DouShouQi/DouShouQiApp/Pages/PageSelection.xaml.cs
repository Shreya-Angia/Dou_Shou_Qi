using DouShouQiModel;

namespace DouShouQiApp.Pages;

public partial class PageSelection : ContentPage
{
    public static App? CurrentApp => App.Current as App;

    public static Game? CurrentGame => CurrentApp?.CurrentGame;

    public PageSelection()
    {
        InitializeComponent();

        GreekIARadio.IsChecked = true;
        RomanIARadio.IsChecked = true;

        ValidateStartButton();
    }

    private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        ValidateStartButton();
    }

    private void OnRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            GreekNameContainer.IsVisible = GreekHumanRadio.IsChecked;
            RomanNameContainer.IsVisible = RomanHumanRadio.IsChecked;
            ValidateStartButton();
        }
    }
    private string CleanAndValidateName(string rawName)
    {
        if (string.IsNullOrWhiteSpace(rawName))
            return string.Empty;

        string cleaned = new string(rawName.Where(c => !char.IsWhiteSpace(c)).ToArray());

        cleaned = cleaned.ToUpper();
        if (cleaned.Length > 10)
            cleaned = cleaned.Substring(0, 10);

        return cleaned;
    }

    private void ValidateStartButton()
    {
        bool greekIsHuman = GreekHumanRadio.IsChecked;
        bool romanIsHuman = RomanHumanRadio.IsChecked;

        string rawGreekName = GreekNameEntry.Text ?? string.Empty;
        string rawRomanName = RomanNameEntry.Text ?? string.Empty;

        string greekName = CleanAndValidateName(rawGreekName);
        string romanName = CleanAndValidateName(rawRomanName);

        bool isValid = false;

        if (greekIsHuman && romanIsHuman)
        {
            if (string.IsNullOrEmpty(greekName) == false && string.IsNullOrEmpty(romanName) == false) 
                isValid = true;
            
        }
        else if (greekIsHuman)
        {
            if (string.IsNullOrEmpty(greekName) == false) 
                isValid = true;
        }
        else if (romanIsHuman)
        {
            if (string.IsNullOrEmpty(romanName) == false)
                isValid = true;
        }
        else
        {
            isValid = false;
        }

        StartButton.IsEnabled = isValid;
        if (isValid)
        {
            StartButton.BackgroundColor = Colors.Green;
        }
        else
        {
            StartButton.BackgroundColor = Colors.Gray;
        }
    }

    private async void GoToGameBoard(object sender, EventArgs e)
    {

        if (CurrentApp!.CurrentGame == null)
        {
            await DisplayAlert("Erreur", "Le jeu actuel n'est pas initialisé.", "OK");
            return;
        }
        CurrentApp.CurrentGame.Player1 = null;
        CurrentApp.CurrentGame.Player2 = null;

        string greekName = CleanAndValidateName(GreekNameEntry.Text);
        string romanName = CleanAndValidateName(RomanNameEntry.Text);

        bool greekIsHuman = GreekHumanRadio.IsChecked;
        bool romanIsHuman = RomanHumanRadio.IsChecked;

        Player player1;
        if (greekIsHuman)
        {
            player1 = new HumanPlayer(greekName, Team.Greek);
        }
        else
        {
            player1 = new EasyAI("AI_GREEK", Team.Greek);
        }

        Player player2;
        if (romanIsHuman)
        {
            player2 = new HumanPlayer(romanName, Team.Roman);
        }
        else
        {
            player2 = new EasyAI("AI_ROMAN", Team.Roman);
        }

        CurrentGame!.Player1 = player1;
        CurrentGame.Player2 = player2;

        // Envoyer le plateau (mais voir a la selection de classic ou custom)
        await Navigation.PushAsync(new GameBoard());
    }

}