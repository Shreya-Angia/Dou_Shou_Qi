namespace DouShouQiApp.Views;
using Microsoft.Maui.Controls;
using Plugin.Maui.Audio;

public partial class HeaderView : ContentView 
{
    private readonly IAudioManager audioManager; 
    public static readonly BindableProperty PageTitleProperty = 
        BindableProperty.Create(nameof(PageTitle), typeof(string), typeof(HeaderView), "Titre par défaut");

        public string PageTitle
    {
        get => (string)GetValue(PageTitleProperty);  
        set => SetValue(PageTitleProperty, value);
    }

    public HeaderView()
    {
        InitializeComponent();
    }

    private async void GoBack(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
    private async void GoToParametre(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Pages.Parametre(audioManager));
    }
}