using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DouShouQiModel
{
    public class Manager
    {
        private readonly IPersistance persistance;

        private readonly List<Player> players;

        private readonly List<Game> games;

        public IReadOnlyList<Player> Players => players;

        public IReadOnlyList<Game> Games => games;

        public IEnumerable<Game> OngoingGame => games.Where(g => g.IsGameLaunched == true).Reverse();

        public Manager(IPersistance persistance)
        {
            this.persistance = persistance;

            players = persistance.Load<Player>().ToList();
            games = persistance.Load<Game>().ToList();
        }

        public void Save()
        {
            persistance.Save(players.ToArray());
            persistance.Save(games.ToArray());
        }

        public Game LoadGame(Game game)
        {
            games.Remove(game);

            Game newGame = new(game);
            games.Add(newGame);

            AutoSave(newGame);

            return newGame;
        }

        private void AutoSave(Game game)
        {
            game.PieceMove += (sender, args) => Save();
            game.PieceDefeat += (sender, args) => Save();
            game.Failed += (sender, args) => Save();
            game.NextTurn += (sender, args) => Save();
            game.GameWin += (sender, args) => Save();
            /*pour le gamewin il faut l'amèliorer pour pouvoir faire des stats (leaderboard)*/
        }

        public Game NewGame(IRules rules)
        {
            Game game = new Game(/*rules*/);
            games.Add(game);
            AutoSave(game);
            return game;
        }

        private Player? SearchExistingPlayer(string name)
        {
            return players.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public Player SearchPlayer(string name)
        {
            Player? player = SearchExistingPlayer(name);
            if (player == null)
            {
                player = new HumanPlayer(name, Team.Unknown);
                players.Add(player);
            }
            return player;
        }
    }
}
