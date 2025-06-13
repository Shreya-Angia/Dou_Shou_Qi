using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO.Pipelines;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using DouShouQiModel;

namespace DouShouQiConsole
{
    public static class DisplayAskClass
    {
        /// <summary>
        /// Affiche la liste des pièces disponible et demande à l'utilisateur de choisir une pièce
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void DisplayAskPiece(object? sender, OnWhichEventArgs e)
        {
            if (sender is HumanPlayer player && e.ListPiece != null)
            {
                Console.WriteLine();
                Console.WriteLine("Liste des pièces disponible :");

                for (int i = 0; i < e.ListPiece.Count; i++)
                {
                    Piece piece = e.ListPiece[i];
                    Console.WriteLine($"{i + 1}. {piece.PieceName} (Force: {piece.Strength})");
                }

                Console.Write("Quelles pièces voulez-vous déplacer ? (Entrer l'index) : ");
                string? input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                    player.ChosenPieceIndex = null;

                if (int.TryParse(input, out int indexPiece) && indexPiece >= 1 && indexPiece <= e.ListPiece.Count)
                {
                    player.ChosenPieceIndex = indexPiece - 1; // -1 car la list commence à 0 et non 1
                }
                else player.ChosenPieceIndex = null;
            }
        }
        /// <summary>
        /// Affiche la liste des mouvements disponible et demande à l'utilisateur de choisir un mouvement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void DisplayAskMove(object? sender, OnWhichEventArgs e)
        {
            if (sender is HumanPlayer player && e.ListPosition != null)
            {
                Console.WriteLine();
                Console.WriteLine("Déplacements disponible : ");
                Console.WriteLine("(Si vous choissisez les coordonnées d'une pièce et que vous êtes assez fort, vous la mangerez)");

                for (int i = 0; i < e.ListPosition.Count; i++)
                {
                    Position pos = e.ListPosition[i];
                    Console.WriteLine($"{i + 1}. ({pos.X}, {pos.Y})");
                }

                Console.Write("Quelle mouvement choissisez vous ? (Entrer l'index) : ");
                string? input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                    player.ChosenMoveIndex = null;

                if (int.TryParse(input, out int indexMove) && indexMove >= 1 && indexMove <= e.ListPosition.Count)
                {
                    player.ChosenMoveIndex = indexMove - 1; // -1 car la list commence à 0 et non 1
                }
                else player.ChosenMoveIndex = null;
            }
        }
        /// <summary>
        /// Affiche le mouvement d'une pièce
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void DisplayMove(object? sender, HappenedEventArgs e)
        {
            if (sender is Game game && e.AttackerPiece != null && e.Destination != null)
            {
                Console.WriteLine();
                Console.WriteLine($"{e.AttackerPiece.PieceName} - ({e.AttackerPiece.Team}) s'est déplacé en ({e.Destination.X}, {e.Destination.Y})");
            }
        }
        /// <summary>
        /// Affiche le mouvement d'une pièce qui a été mangé par une autre pièce
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void DisplayMoveDefeate(object? sender, HappenedEventArgs e)
        {
            if (sender is Game game && e.DefeatPiece != null && e.AttackerPiece != null && e.Destination != null)
            {
                Console.WriteLine();
                Console.WriteLine($"{e.AttackerPiece.PieceName} - ({e.AttackerPiece.Team}) a abattu ({e.DefeatPiece.PieceName}) - ({e.DefeatPiece.Team}) qui était en ({e.Destination.X}, {e.Destination.Y})");
            }
        }
        /// <summary>
        /// Affiche le message de bienvenue et demande les règles du jeu, le nom des joueurs et l'équipe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        public static void OnGameInit(object? sender, GameInitEventArgs e)
        {
            if (sender is not Game game || game == null)
                return;

            Console.Clear();
            Console.WriteLine("++======================================");
            Console.WriteLine("||                                     ||");
            Console.WriteLine("||            DouShouQi                ||");
            Console.WriteLine("||              Myth◌l◌gy              ||");
            Console.WriteLine("||                                     ||");
            Console.WriteLine("++=====================================++");
            Console.WriteLine();

            game.Rules = AskRules();
            string name1 = AskPlayerName("Nom du premier joueur : ", null);
            Team team1 = AskTeam(name1);
            Team team2 = team1 == Team.Greek ? Team.Roman : Team.Greek;
            Player player1 = CreatePlayer1(name1, team1);

            string name2 = AskPlayerName("Nom du deuxieme joueur (ou 'IA' pour jouer contre une ia) : ", name1);
            Player player2 = CreatePlayer2(name2, team2);

            game.Player1 = player1;
            game.Player2 = player2;

            if (game.Player1 == null || game.Player2 == null)
                throw new InvalidOperationException("Erreur création des joueur");

            Console.WriteLine("Création de :");
            Console.WriteLine($"{game.Player1.Name} - {game.Player1.Team}");
            Console.WriteLine($"{game.Player2.Name} - {game.Player2.Team}");
            Console.WriteLine("Lancement du jeu...");
            Console.WriteLine();
        }

        private static StandardRules AskRules()
        {
            while (true)
            {
                Console.Write("Choissisez vos règles ('standard' ou 'custom'): ");
                string? rulesChoisi = Console.ReadLine()?.Trim().ToLower();
                if (string.IsNullOrEmpty(rulesChoisi))
                {
                    Console.WriteLine("Règles incorrectes.");
                    continue;
                }
                if (rulesChoisi == "standard")
                    return new StandardRules();
                if (rulesChoisi == "custom")
                {
                    Console.WriteLine("Le mode 'custom' est momentanément indisponible.");
                    continue;
                }
                Console.WriteLine("Règles incorrectes.");
            }
        }

        private static string AskPlayerName(string prompt, string? forbiddenName)
        {
            string[] nomJoueurInterdit = [];
            if (prompt != "Nom du deuxieme joueur (ou 'IA' pour jouer contre une ia) : ")
            {
                nomJoueurInterdit = ["IA", "AI", "EASYAI", "EASYIA", "AI_EASY", "IA_EASY", "MEDIUMAI", "MEDIUMIA", "AI_MEDIUM", "IA_MEDIUM", "HARDAI", "HARDIA", "AI_HARD", "IA_HARD"];
            }

            while (true)
            {
                Console.Write(prompt);
                string? name = Console.ReadLine()?.Trim().ToUpper();
                if (string.IsNullOrEmpty(name) || nomJoueurInterdit.Contains(name) || (forbiddenName != null && name == forbiddenName))
                {
                    if (forbiddenName != null && name == forbiddenName)
                        Console.WriteLine($"Vous ne pouvez pas vous appeler comme {forbiddenName}.");
                    else
                        Console.WriteLine("Nom incorrecte");
                    continue;
                }
                return name;
            }
        }

        private static Team AskTeam(string playerName)
        {
            while (true)
            {
                Console.Write($"Nom de l'équipe pour {playerName} ('greek' ou 'roman') : ");
                string? teamtmp = Console.ReadLine()?.Trim().ToLower();
                if (string.IsNullOrEmpty(teamtmp) || (teamtmp != "greek" && teamtmp != "roman"))
                {
                    Console.WriteLine("Nom incorrecte");
                    continue;
                }
                return teamtmp == "greek" ? Team.Greek : Team.Roman;
            }
        }

        private static Player CreatePlayer1(string name1, Team team1)
        {
            if (name1 == "AIAIAI")
                return new EasyAI(name1, team1);
            return new HumanPlayer(name1, team1);
        }

        private static Player CreatePlayer2(string name2, Team team2)
        {
            while (true)
            {
                if (name2 == "AI" || name2 == "IA")
                {
                    Console.Write("Choisissez la difficulté ('easy'/'medium'/'hard') : ");
                    string? difficulty = Console.ReadLine()?.Trim().ToLower();
                    if (string.IsNullOrEmpty(difficulty) || (difficulty != "easy" && difficulty != "medium" && difficulty != "hard"))
                    {
                        Console.WriteLine("Difficulté incorrecte");
                        continue;
                    }
                    if (difficulty == "easy")
                        return new EasyAI("AI_easy", team2);
                    Console.WriteLine("Difficulté non implémenté");
                    continue;
                }
                return new HumanPlayer(name2, team2);
            }
        }

        /// <summary>
        /// Affiche le message de début de tour
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void DisplayNextTurn(object? sender, HappenedEventArgs e)
        {
            if (sender is Game game && game.CurrentPlayer != null && game.TurnCounter != null)
            {
                Console.WriteLine();
                Console.WriteLine($"-----------------------------------------");
                Console.WriteLine($"----- Tour {game.TurnCounter} - Joueur : {game.CurrentPlayer.Name} ({game.CurrentPlayer.Team})");
                Console.WriteLine($"-----------------------------------------");
            }
        }

        /// <summary>
        /// Affiche le message d'erreur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void DisplayError(object? sender, FailedEventArgs e)
        {
            if (e.Exception != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erreur : {e.Exception.Message}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Erreur : Une exception inconnue s'est produite.");
                Console.ResetColor();
            }
        }
        /// <summary>
        /// Affiche le message de victoire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void DisplayGameWin(object? sender, HappenedEventArgs e)
        {
            if (sender is Game game && game.CurrentPlayer != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"-----------------------------------------");
                Console.WriteLine($"------ {game.CurrentPlayer.Name} ({game.CurrentPlayer.Team}) a gagné !");
                Console.WriteLine($"-----------------------------------------");
                Console.ResetColor();
            }
        }
        /// <summary>
        /// Affiche le plateau de jeu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        public static void DisplayBoard(object? sender, HappenedEventArgs e)
        {
            if (e.Board == null || e.AllPiece == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Erreur : Affichage du plateau impossible (données manquantes).");
                Console.ResetColor();
                return;
            }
            int columns = e.Board.GetLength(0);
            int rows = e.Board.GetLength(1);

            // Fonction locale pour trouver la pièce à une position donnée
            static Piece? GetPieceAtCell(int x, int y, List<Piece> pieces)
            {
                foreach (var piece in pieces)
                {
                    if (piece.Position.X == x && piece.Position.Y == y && piece.InPlay)
                        return piece;
                }
                return null;
            }

            static void PrintCell(int x, int y, List<Piece> pieces, Cell[,] board)
            {
                Piece? pieceAtCell = GetPieceAtCell(x, y, pieces);
                if (pieceAtCell != null)
                {
                    Console.ForegroundColor = BoardPrinter.GetColor(pieceAtCell.Team);
                    Console.Write(BoardPrinter.GetSymbolPiece(pieceAtCell) + " ");
                }
                else
                {
                    Console.ForegroundColor = BoardPrinter.GetColor(board[x, y].TeamCell);
                    Console.Write(BoardPrinter.GetSymbolCell(board[x, y]) + " ");
                }
                Console.ResetColor();
            }

            for (int y = rows - 1; y >= 0; y--)
            {
                Console.Write($"{y,2} ");
                for (int x = 0; x < columns; x++)
                {
                    PrintCell(x, y, e.AllPiece, e.Board);
                }
                Console.WriteLine();
            }

            Console.Write("   ");
            for (int x = 0; x < columns; x++)
            {
                Console.Write($"{x} ");
            }
            Console.WriteLine();
        }
    }
}
