namespace DouShouQiApp.Pages;

using System.Text.Json;
using DouShouQiModel;
using Stubs;
using Windows.ApplicationModel.Store;

public partial class SelectGame : ContentPage
{
    public static App? CurrentApp => App.Current as App;

    public static Game? CurrentGame => CurrentApp?.CurrentGame;

    public SelectGame()
    {
        InitializeComponent();
    }

    private async void GoToClassic(object sender, EventArgs e)
    {
        if (CurrentApp != null)
            CurrentApp.CurrentGame = new Game();

        CurrentGame!.Rules = new StandardRules();
        CurrentGame.Board = new Board();
        InitializeBoardWithStub();

        await Navigation.PushAsync(new PageSelection());
    }

    private async void GoToCustom(object sender, EventArgs e)
    {
        if (CurrentApp != null)
            CurrentApp.CurrentGame = new Game();

        // CurrentGame!.Rules = new CustomRules();
        CurrentGame.Board = new Board();
        InitializeBoardWithStub();

        await Navigation.PushAsync(new Customization());
    }

    private static void InitializeBoardWithStub()
    {
        var stubCell = new Stubs.TabCellStub();
        var stubCells = stubCell.CreateStubBoard();
        if (stubCells != null)
        {
            var stubMatrix = new DouShouQiModel.Matrix(stubCells.GetLength(0), stubCells.GetLength(1));
            typeof(DouShouQiModel.Matrix)
                .GetField("cells", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(stubMatrix, stubCells);

            if (CurrentGame?.Board != null)
            {
                CurrentGame.Board.InitializeBoard(stubMatrix);
            }
        }
    }
}
