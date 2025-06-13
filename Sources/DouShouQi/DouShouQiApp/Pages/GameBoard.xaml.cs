using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DouShouQiApp.WinUI;
using DouShouQiModel;
using Plugin.Maui.Audio;
using Stubs;

namespace DouShouQiApp.Pages;

public partial class GameBoard : ContentPage
{
    public static App? CurrentApp => App.Current as App;
    public static Game? CurrentGame => CurrentApp!.CurrentGame ?? throw new InvalidOperationException("CurrentGame is not initialized.");

    public readonly IAudioManager audioManager; 

    public Board Board = CurrentGame!.Board ?? throw new InvalidOperationException("The board is not initialized.");
    public Player player1 = CurrentGame!.Player1 ?? throw new InvalidOperationException("Player1 is not initialized.");
    public Player player2 = CurrentGame!.Player2 ?? throw new InvalidOperationException("Player2 is not initialized.");
    public string GreekPseudo => CurrentGame!.Player1?.Name ?? "";
    public string RomanPseudo => CurrentGame!.Player2?.Name ?? "";
    public IRules Rules = CurrentGame!.Rules ?? throw new InvalidOperationException("Rules are not initialized.");

    public IEnumerable<DouShouQiModel.Cell> BoardGame { get; }
    public IEnumerable<Piece> AllPiece { get; private set; } = [];
    public IEnumerable<Piece> GreekPiece { get; private set; } = [];
    public IEnumerable<Piece> RomanPiece { get; private set; } = [];
    public ObservableCollection<Piece> boardWithPiece { get; } = [];

    private Piece? _selectedPiece;

    public GameBoard()
    {
        InitializeComponent();
        BoardGame = [.. ToFlatArray(Board.GetMatrix()).Cast<DouShouQiModel.Cell>()];
        AllPiece = [.. new ListPieceStub().CreateStubListPiece().Cast<Piece>().ToList()];
        GreekPiece = [.. new ListPieceStub().CreateStubListPiece().Where(p => p.Team == Team.Greek).Cast<Piece>().ToList()];
        RomanPiece = [.. new ListPieceStub().CreateStubListPiece().Where(p => p.Team == Team.Roman).Cast<Piece>().ToList()];
        CurrentGame.StartGame();

        PopulateBoardWithPiece();
        ClearBoard();

        BindingContext = this;
        if (CurrentGame.CurrentPlayer is AIPlayer)
            _ = PlayAITurnAsync();
    }

    private async void GoToPause(object sender, EventArgs e) 
    {
        await Navigation.PushAsync(new PausePage());
    }

    public static DouShouQiModel.Cell[] ToFlatArray(DouShouQiModel.Cell[,] matrix)
    {
        int cols = matrix.GetLength(0);
        int rows = matrix.GetLength(1);
        DouShouQiModel.Cell[] flat = new DouShouQiModel.Cell[rows * cols];
        int index = 0;

        for (int i = rows - 1; i >= 0; i--)
            for (int j = 0; j < cols; j++)
                flat[index++] = matrix[j, i];

        return flat;
    }

    private void ClearBoard()
    {
        foreach (var piece in boardWithPiece) 
        {
            piece.SetSelected(false);
            if (piece.PieceName == "canMove")
                piece.SetPieceName(string.Empty); 
        }
    }

    private List<Position> GetPossibleMoves(Piece piece)
    {
        var allMoves = Board.AllMovePiece(CurrentGame.CurrentPlayer, AllPiece.ToList(), Rules, Board);
        return allMoves
            .Where(tuple => tuple.Item1 == piece)
            .Select(tuple => tuple.Item2)
            .ToList();
    }

    private async void Selected(object sender, EventArgs e)
    {
        if (sender is Image image && image.BindingContext is Piece tappedPiece)
        {
            // 1. Sélection d'une pièce du joueur courant
            if (_selectedPiece == null)
            {
                if (tappedPiece.Team == CurrentGame.CurrentPlayer.Team && tappedPiece.InPlay)
                {
                    _selectedPiece = tappedPiece;
                    tappedPiece.SetSelected(true);

                    var possibleMoves = GetPossibleMoves(tappedPiece);
                    ClearBoard();

                    foreach (var pos in possibleMoves)
                    {
                        var destPiece = boardWithPiece.FirstOrDefault(p => p.Position.X == pos.X && p.Position.Y == pos.Y);
                        if (destPiece != null && string.IsNullOrEmpty(destPiece.PieceName))
                            destPiece.SetPieceName("canMove");
                    }
                }
            }
            // 2. Sélection d'une destination
            else
            {
                var possibleMoves = GetPossibleMoves(_selectedPiece);
                if (tappedPiece.PieceName == "canMove" ||
                    possibleMoves.Any(pos => pos.X == tappedPiece.Position.X && pos.Y == tappedPiece.Position.Y))
                {
                    // Capture éventuelle
                    var captured = AllPiece.FirstOrDefault(p =>
                        p.Position.X == tappedPiece.Position.X &&
                        p.Position.Y == tappedPiece.Position.Y &&
                        p.InPlay &&
                        p.Team != CurrentGame.CurrentPlayer.Team); 

                    if (captured != null)
                        captured.SetInPlay(false); 

                    // Déplacement de la pièce sélectionnée
                    _selectedPiece.MoveTo(tappedPiece.Position);
                    _selectedPiece.SetSelected(false);

                    // On sauvegarde le joueur qui vient de jouer (avant le switch)
                    var winner = CurrentGame.CurrentPlayer;

                    _selectedPiece = null;

                    PopulateBoardWithPiece();
                    ClearBoard();

                    // Vérification de la victoire
                    bool isOver = Rules.IsGameOver(AllPiece.ToList(), new List<Team> { Team.Greek, Team.Roman });
                    if (isOver)
                    {
                        var isWithAI = IsGameWithAI();
                        var humanPlayer = (CurrentGame.Player1 is AIPlayer) ? CurrentGame.Player2 : CurrentGame.Player1;
                        await ToEndGame(isWithAI, humanPlayer, winner, winner.Team, AllPiece.ToList());
                        return;
                    }

                    SwitchPlayer();

                }
                else
                {
                    // Désélection si on clique ailleurs
                    _selectedPiece.SetSelected(false);
                    _selectedPiece = null; 
                    ClearBoard();
                }
            }
        }
    }

    private void SwitchPlayer()
    {
        if (CurrentGame.CurrentPlayer == CurrentGame.Player1)
            CurrentGame.SetCurrentPlayer(CurrentGame.Player2);
        else
            CurrentGame.SetCurrentPlayer(CurrentGame.Player1); 
        if (CurrentGame.CurrentPlayer is AIPlayer)
            _ = PlayAITurnAsync();
    }

    private void PopulateBoardWithPiece()
    {
        boardWithPiece.Clear();
        var cells = Board.GetMatrix(); 
        int columns = cells.GetLength(0);
        int rows = cells.GetLength(1);

        for (int y = rows - 1; y >= 0; y--) 
        {
            for (int x = 0; x < columns; x++)
            {
                var piece = AllPiece.FirstOrDefault(p => p.Position.X == x && p.Position.Y == y && p.InPlay);
                if (piece == null)
                {
                    piece = new Piece(string.Empty, new Position(x, y)); 
                }
                boardWithPiece.Add(piece); 
            }
        }
    }

    private bool IsGameWithAI()
    {
        return CurrentGame.Player1 is AIPlayer || CurrentGame.Player2 is AIPlayer;
    }

    public async Task ToEndGame(bool isWithAI, Player humanPlayer, Player winner, Team win, List<Piece> allPieces)
    {

        ResetGame();

        if (isWithAI)
        {
            if (winner != null && humanPlayer != null && winner.Team == humanPlayer.Team)
            {
                await Navigation.PushAsync(new Victory(Win.Solo, audioManager));
            }
            else
            {
                await Navigation.PushAsync(new Defeat(audioManager)); 
            }
        }
        else
        {
            if (win == Team.Greek)
            {
                await Navigation.PushAsync(new Victory(Win.GreekWin, audioManager));
            }
            else if (win == Team.Roman)
            {
                await Navigation.PushAsync(new Victory(Win.RomanWin, audioManager));
            }
            else
            {
                await Navigation.PushAsync(new Victory(Win.Solo, audioManager));
            }
        }
    }


    private async Task PlayAITurnAsync()
    {
        // Vérifie que le joueur courant est bien une IA
        if (CurrentGame.CurrentPlayer is AIPlayer ai)
        {
            // Récupère tous les mouvements possibles
            var allMoves = Board.AllMovePiece(ai, AllPiece.ToList(), Rules, Board);

            if (allMoves.Count > 0)
            {
                var piece = ai.ChoosePiece(allMoves.Select(m => m.Item1).Distinct().ToList());
                var moves = allMoves.Where(m => m.Item1 == piece).Select(m => m.Item2).ToList();
                var move = ai.ChooseMove(moves);

                var captured = AllPiece.FirstOrDefault(p =>
                    p.Position.X == move.X &&
                    p.Position.Y == move.Y &&
                    p.InPlay &&
                    p.Team != ai.Team);

                if (captured != null)
                    captured.SetInPlay(false);

                // Déplacement
                piece.MoveTo(move);

                PopulateBoardWithPiece();
                ClearBoard();

                bool isOver = Rules.IsGameOver(AllPiece.ToList(), new List<Team> { Team.Greek, Team.Roman });
                if (isOver)
                {
                    var humanPlayer = (CurrentGame.Player1 is AIPlayer) ? CurrentGame.Player2 : CurrentGame.Player1;
                    await ToEndGame(true, humanPlayer, ai, ai.Team, AllPiece.ToList());
                    return;
                }

                SwitchPlayer(); 
            }
        }
    }

    public void ResetGame()
    {
        AllPiece = [.. new ListPieceStub().CreateStubListPiece().Cast<Piece>().ToList()];
        GreekPiece = [.. AllPiece.Where(p => p.Team == Team.Greek)];
        RomanPiece = [.. AllPiece.Where(p => p.Team == Team.Roman)];
        Board = new Board();
        PopulateBoardWithPiece();
        ClearBoard();
    }

}
