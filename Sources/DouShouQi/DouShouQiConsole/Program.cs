using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Xml;
using System.Text;
using DouShouQiConsole;
using DouShouQiModel;
using Stubs;

Console.OutputEncoding = Encoding.UTF8;

var board = new Board();
var game = new Game();
var boardPrinter = new BoardPrinter();

InitGame(game, boardPrinter);
InitPlayers(game);

var allPieces = CreateAllPieces();
var stubMatrix = CreateStubMatrix();
InitBoardIfPossible(board, stubMatrix);

var teamsToCheck = GetTeams(game);

GameLoop(game, board, boardPrinter, allPieces, teamsToCheck);

static void InitGame(Game game, BoardPrinter boardPrinter)
{
    SubscribeGameEvents(game, boardPrinter);
    TryStartGame(game);
}

static void SubscribeGameEvents(Game game, BoardPrinter boardPrinter)
{
    game.GameInit += DisplayAskClass.OnGameInit;
    game.Failed += DisplayAskClass.DisplayError;
    game.PieceMove += DisplayAskClass.DisplayMove;
    game.PieceDefeat += DisplayAskClass.DisplayMoveDefeate;
    game.NextTurn += DisplayAskClass.DisplayNextTurn;
    game.GameWin += DisplayAskClass.DisplayGameWin;
    boardPrinter.BoardAffich += DisplayAskClass.DisplayBoard;
}

static void TryStartGame(Game game)
{
    try
    {
        game.StartGame();
    }
    catch (Exception e)
    {
        Console.WriteLine($"Game Error: {e.Message}");
    }
}

static void InitPlayers(Game game)
{
    SubscribeHumanPlayer(game.Player1);
    SubscribeHumanPlayer(game.Player2);
}

static void SubscribeHumanPlayer(Player? player)
{
    if (player is HumanPlayer hp)
    {
        hp.AskWhichPiece += DisplayAskClass.DisplayAskPiece;
        hp.AskWhichMove += DisplayAskClass.DisplayAskMove;
    }
}

static List<Piece> CreateAllPieces()
    => new Stubs.ListPieceStub().CreateStubListPiece();

static Matrix? CreateStubMatrix()
{
    try
    {
        var stubCell = new Stubs.TabCellStub();
        var stubCells = stubCell.CreateStubBoard();
        if (stubCells == null)
        {
            Console.WriteLine("Erreur : le plateau de cellules n'a pas pu être créé.");
            return null;
        }

        var matrix = new DouShouQiModel.Matrix(stubCells.GetLength(0), stubCells.GetLength(1));
        matrix.SetCells(FlatCells(stubCells));
        return matrix;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erreur lors de la création de la matrice : {ex.Message}");
        return null;
    }
}

static List<Cell> FlatCells(Cell[,] cells)
{
    var list = new List<Cell>(cells.Length);
    for (int i = 0; i < cells.GetLength(0); i++)
    {
        for (int j = 0; j < cells.GetLength(1); j++)
        {
            list.Add(cells[i, j]);
        }
    }
    return list;
}

static void InitBoardIfPossible(Board board, Matrix? stubMatrix)
{
    if (stubMatrix != null)
        board.InitializeBoard(stubMatrix);
}

static List<Team> GetTeams(Game game)
{
    var teams = new List<Team>();
    AddTeam(game.Player1, teams);
    AddTeam(game.Player2, teams);
    return teams;
}

static void AddTeam(Player? player, List<Team> teams)
{
    if (player != null)
        teams.Add(player.Team);
}

static void GameLoop(Game game, Board board, BoardPrinter boardPrinter, List<Piece> allPieces, List<Team> teamsToCheck)
{
    while (!IsGameOver(game, allPieces, teamsToCheck))
    {
        game.OnNextTurn(new HappenedEventArgs());
        ShowBoardIfHuman(game, boardPrinter, board, allPieces);
        MakeMoveIfPossible(game, board, allPieces);
        game.SwitchTurn();
    }
    EndGame(game);
}

static void EndGame(Game game)
{
    game.SwitchTurn();
    game.OnGameWin(new HappenedEventArgs());
}

static bool IsGameOver(Game game, List<Piece> allPieces, List<Team> teamsToCheck)
    => game.Rules == null || game.Rules.IsGameOver(allPieces, teamsToCheck);

static void ShowBoardIfHuman(Game game, BoardPrinter boardPrinter, Board board, List<Piece> allPieces)
{
    if (game.CurrentPlayer is not HumanPlayer) return;
    try
    {
        boardPrinter.OnBoardAffich(new HappenedEventArgs(board.GetMatrix(), allPieces));
    }
    catch (Exception e)
    {
        game.OnFailed(new FailedEventArgs(e));
    }
}

static void MakeMoveIfPossible(Game game, Board board, List<Piece> allPieces)
{
    if (game.CurrentPlayer == null || game.Rules == null) return;
    game.MakeMoove(game.CurrentPlayer, board, allPieces, game.Rules);
}
