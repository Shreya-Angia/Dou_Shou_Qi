namespace DouShouQiApp.Pages;

public partial class RepriseGame : ContentPage
{
	public RepriseGame()
	{
		BindingContext = MauiProgram.Manager.OngoingGame;

        InitializeComponent();
	}
}