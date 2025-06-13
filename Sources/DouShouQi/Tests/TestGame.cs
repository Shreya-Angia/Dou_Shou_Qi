/***************************************************************************
 * TestGame.cs
 * -------------------------------------------------------------------------
 * Project       : DouShouQi Mythology
 * Author        : JOUVE Enzo ; ANGIA Shreya
 * Date          : 17/05/2025
 * Description   : Unit tests for the Game             
 * -------------------------------------------------------------------------
 * Tests status : 12/12
 ***************************************************************************/



using Xunit;
using DouShouQiModel;
using System.Collections.Generic;

namespace UnitTests
{
    public class TestGame
    {
        [Fact]
        public void StartGame()
        {
            var game = new Game
            {
                Player1 = new EasyAI("P1", Team.Greek),
                Player2 = new EasyAI("P2", Team.Roman),
                Rules = new StandardRules()
            };

            game.StartGame();

            Assert.True(game.IsGameLaunched);
            Assert.Equal(1, game.TurnCounter);
            Assert.Equal(game.Player1, game.CurrentPlayer);
        }

        [Fact]
        public void SwitchTurn_ChangesCurrentPlayerAndIncrementsTurn()
        {
            var game = new Game
            {
                Player1 = new EasyAI("P1", Team.Greek),
                Player2 = new EasyAI("P2", Team.Roman),
                Rules = new StandardRules()
            };

            game.StartGame();
            var first = game.CurrentPlayer;
            game.SwitchTurn();

            Assert.NotEqual(first, game.CurrentPlayer);
            Assert.Equal(2, game.TurnCounter);
        }

        [Fact]
        public void AttackPiece_RemovesDefenderFromPlay()
        {
            var game = new Game();
            var attacker = new Piece("Athena", new Position(1, 2), new Piece.PieceOptions {InPlay = true, IsSelected = false, CanMoveOnWater = false, CanJumpOverWater = false, Strength = 5, Team = Team.Greek });
            var defender = new Piece("Athena", new Position(1, 1), new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = false, CanJumpOverWater = false, Strength = 4, Team = Team.Roman });
            var allPieces = new List<Piece> { attacker, defender };

            game.AttackPiece(attacker, new Position(1, 1), allPieces);

            Assert.False(defender.InPlay);
        }

        // Does nothing if there is no enemy piece at the destination
        [Fact]
        public void AttackPiece_DoesNothing_IfNoPieceAtDestination()
        {
            var game = new Game();
            var attacker = new Piece("Ares", new Position(2, 2), new Piece.PieceOptions { InPlay = true, Strength = 5, Team = Team.Greek });
            var other = new Piece("Hades", new Position(1, 1), new Piece.PieceOptions { InPlay = true, Strength = 4, Team = Team.Roman });
            var allPieces = new List<Piece> { attacker, other };

            game.AttackPiece(attacker, new Position(3, 3), allPieces);

            Assert.True(other.InPlay); // Aucun changement
        }

        // Throws an exception if the current player is null
        [Fact]
        public void SwitchTurn_ThrowsException_IfCurrentPlayerIsNull()
        {
            var game = new Game
            {
                Player1 = new EasyAI("P1", Team.Greek),
                Player2 = new EasyAI("P2", Team.Roman),
                Rules = new StandardRules()
            };

            Assert.Throws<InvalidOperationException>(() => game.SwitchTurn());
        }

        // If the game is not initialized properly, it should throw an exception when starting
        [Fact]
        public void StartGame_ThrowsException_IfNotInitializedProperly()
        {
            var game = new Game(); // Pas de joueurs ni de règles

            Assert.Throws<InvalidOperationException>(() => game.StartGame());
        }

        public class TestPlayer : Player
        {
            private readonly Piece _pieceToChoose;
            private readonly Position _moveToChoose;

            public TestPlayer(string name, Team team, Piece pieceToChoose, Position moveToChoose)
                : base(name, team)
            {
                _pieceToChoose = pieceToChoose;
                _moveToChoose = moveToChoose;
            }

            public override Piece ChoosePiece(List<Piece> pieceParcourut) => _pieceToChoose;
            public override Position ChooseMove(List<Position> positionParcourut) => _moveToChoose;
        }

        //When
        [Fact]
        public void MakeMoove_DeplaceLaPiece_SiDestinationVide()
        {
            // Arrange
            var piece = new Piece("Athena", new Position(1, 1), new Piece.PieceOptions { InPlay = true, Team = Team.Greek });
            var destination = new Position(2, 1);
            var player = new TestPlayer("P1", Team.Greek, piece, destination);
            var allPieces = new List<Piece> { piece };
            var board = new Board();
            var rules = new StandardRules();

            var game = new Game();

            // Act
            game.MakeMoove(player, board, allPieces, rules);

            // Assert
            Assert.Equal(destination, piece.Position);
        }

        [Fact]
        public void MakeMoove_DeclencheAttack_SiDestinationOccupee()
        {
            // Arrange
            var piece = new Piece("Athena", new Position(1, 1), new Piece.PieceOptions { InPlay = true, Team = Team.Greek });
            var enemy = new Piece("Hades", new Position(2, 1), new Piece.PieceOptions { InPlay = true, Team = Team.Roman });
            var destination = new Position(2, 1);
            var player = new TestPlayer("P1", Team.Greek, piece, destination);
            var allPieces = new List<Piece> { piece, enemy };
            var board = new Board();
            var rules = new StandardRules();
            var game = new Game();

            // Act
            game.MakeMoove(player, board, allPieces, rules);

            // Assert
            Assert.False(enemy.InPlay); // L'ennemi doit être retiré du jeu
            Assert.Equal(destination, piece.Position); 
        }

        [Fact] 
        public void MakeMoove_DeclencheEvenementPieceMove()
        {
            // Arrange
            var piece = new Piece("Athena", new Position(1, 1), new Piece.PieceOptions { InPlay = true, Team = Team.Greek });
            var destination = new Position(2, 1);
            var player = new TestPlayer("P1", Team.Greek, piece, destination);
            var allPieces = new List<Piece> { piece };
            var board = new Board();
            var rules = new StandardRules();
            var game = new Game();

            bool eventDeclenche = false;
            game.PieceMove += (sender, args) => eventDeclenche = true;

            // Act
            game.MakeMoove(player, board, allPieces, rules);

            // Assert
            Assert.True(eventDeclenche); 
        }

        // Simule une exception dans le choix de la pièce pour tester l’événement Failed
        public class ExceptionPlayer : Player
        {
            public ExceptionPlayer(string name, Team team) : base(name, team) { }
            public override Piece ChoosePiece(List<Piece> pieceParcourut) => throw new System.Exception("Erreur de test");
            public override Position ChooseMove(List<Position> positionParcourut) => throw new System.Exception("Erreur de test");
        }


        // Teste que l’événement PieceDefeat est déclenché lors d’une attaque
        [Fact]
        public void MakeMoove_DeclencheEvenementPieceDefeat_SiAttaque()
        {
            // Arrange
            var piece = new Piece("Athena", new Position(1, 1), new Piece.PieceOptions { InPlay = true, Team = Team.Greek });
            var enemy = new Piece("Hades", new Position(2, 1), new Piece.PieceOptions { InPlay = true, Team = Team.Roman });
            var destination = new Position(2, 1);
            var player = new TestPlayer("P1", Team.Greek, piece, destination);
            var allPieces = new List<Piece> { piece, enemy };
            var board = new Board();
            var rules = new StandardRules();
            var game = new Game();

            bool defeatEvent = false;
            game.PieceDefeat += (sender, args) => defeatEvent = true;

            // Act
            game.MakeMoove(player, board, allPieces, rules);

            // Assert
            Assert.True(defeatEvent); 
        }


        // Swith multiple times to ensure it alternates correctly
        [Fact]
        public void SwitchTurn_AlternatesBetweenPlayers_MultipleTimes()
        {
            var game = new Game
            {
                Player1 = new EasyAI("P1", Team.Greek),
                Player2 = new EasyAI("P2", Team.Roman),
                Rules = new StandardRules()
            };
            game.StartGame();

            // First switch : Player2
            game.SwitchTurn();
            Assert.Equal(game.Player2, game.CurrentPlayer);
            Assert.Equal(2, game.TurnCounter); 

            // Second switch : Player1
            game.SwitchTurn();
            Assert.Equal(game.Player1, game.CurrentPlayer);
            Assert.Equal(3, game.TurnCounter);
        }


        // Throws an exception the players are null
        [Fact]
        public void SwitchTurn_exception_whenNull() 
        {
            var game = new Game();
            Assert.Throws<InvalidOperationException>(() => game.SwitchTurn());
        }     




    }
}