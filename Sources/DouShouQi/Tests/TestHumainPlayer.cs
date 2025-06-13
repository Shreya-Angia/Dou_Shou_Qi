/***************************************************************************
 * TestHumanPlayer.cs
 * -------------------------------------------------------------------------
 * Project       : DouShouQi Mythology
 * Author        : JOUVE Enzo ; ANGIA Shreya
 * Date          : 17/05/2025
 * Description   : Unit tests for HumainPlayer.                
 * -------------------------------------------------------------------------
 * Tests status : 10/10
 ***************************************************************************/

using Xunit;
using DouShouQiModel;

namespace TestHumanPlayer
{
    public class TestHumanPlayer
    {
        [Fact]
        public void TestContructeur()
        {
            Player testy = new HumanPlayer("Testy", Team.Greek);
            Assert.Equal(8, testy.NbPieces);
            Assert.Equal("Testy", testy.Name);
            Assert.Equal(Team.Greek, testy.Team);
        }

        [Fact]
        public void TestContructeur_modifInitialPiece()
        {
            Player testypiece = new HumanPlayer("Testypiece", Team.Roman);
            Assert.Equal(8, testypiece.NbPieces);
            Assert.Equal("Testypiece", testypiece.Name);
            Assert.Equal(Team.Roman, testypiece.Team);
        }


        // When there is no index given by the player, it should throw an exception
        [Fact]
        public void TestChooseMove_NullIndex_ThrowsException()
        {
            var player = new HumanPlayer("Testy", Team.Greek);
            var possibleMoves = new List<Position> { new Position(0, 0) };

            Assert.Throws<InvalidOperationException>(() => player.ChooseMove(possibleMoves));
        }

        // When the index chosen is not within the range of possible moves, it should throw an exception
        [Fact]
        public void TestChooseMove_IndexOutOfRange_ThrowsException()
        {
            var player = new HumanPlayer("Tom", Team.Greek);
            var possibleMoves = new List<Position> { new Position(0, 0) };

            player.ChosenMoveIndex = 5; // Hors limite

            Assert.Throws<InvalidOperationException>(() => player.ChooseMove(possibleMoves));
        }

        // When the index chosen is valid, it should return the corresponding position
        [Fact]
        public void TestSetChosenMoveIndex_Valid()
        {
            var player = new HumanPlayer("Alice", Team.Greek);
            player.ChosenMoveIndex = 0;
            Assert.Equal(0, player.ChosenMoveIndex); 
        }

        [Fact]
        public void TestChoosePiece_NullIndex_ThrowsException() 
        {
            var player = new HumanPlayer("Testy", Team.Greek);
            var possiblePieces = new List<Piece>
            {
                new Piece("Lion", new Position(0, 0), new Piece.PieceOptions { InPlay = true, Strength = 7, Team = Team.Greek })
            };

            Assert.Throws<InvalidOperationException>(() => player.ChoosePiece(possiblePieces));
        }

        [Fact]
        public void TestChoosePiece_IndexOutOfRange_ThrowsException()
        {
            var player = new HumanPlayer("Tom", Team.Greek);
            var possiblePieces = new List<Piece>
            {
                new Piece("Piece", new Position(0, 0), new Piece.PieceOptions { InPlay = true, Strength = 7, Team = Team.Greek })
            };

            player.ChosenPieceIndex = 5; // Hors limite 

            Assert.Throws<InvalidOperationException>(() => player.ChoosePiece(possiblePieces));
        }

        [Fact]
        public void TestSetChosenPieceIndex_Valid() 
        {
            var player = new HumanPlayer("Alice", Team.Greek);
            player.ChosenPieceIndex = 0;
            Assert.Equal(0, player.ChosenPieceIndex);
        }

        [Fact]
        public void TestChooseMove_NegativeIndex_ThrowsException()
        {
            var player = new HumanPlayer("Testy", Team.Greek);
            var possibleMoves = new List<Position> { new Position(0, 0) };

            player.ChosenMoveIndex = -1;
            Assert.Throws<InvalidOperationException>(() => player.ChooseMove(possibleMoves));
        }

       

        [Fact]
        public void TestChoosePiece_NegativeIndex_ThrowsException()
        {
            var player = new HumanPlayer("Testy", Team.Greek);
            var possiblePieces = new List<Piece>
            {
                new Piece("Lion", new Position(0, 0), new Piece.PieceOptions { InPlay = true, Strength = 7, Team = Team.Greek })
            };

            player.ChosenPieceIndex = -1;
            Assert.Throws<InvalidOperationException>(() => player.ChoosePiece(possiblePieces));
        }






    }
}