/***************************************************************************
 * TestAIPlayer.cs
 * -------------------------------------------------------------------------
 * Project       : DouShouQi Mythology
 * Author        : JOUVE Enzo
 * Date          : 17/05/2025
 * Description   : Unit tests for AIPlayer.                
 * -------------------------------------------------------------------------
 * Tests status : 2/?
 ***************************************************************************/

using Xunit;
using DouShouQiModel;

namespace UnitTests
{
    public class EasyAITests
    {
        [Fact]
        public void TestConstructorPiecesChange()
        {
            Player bot = new EasyAI("Bot", Team.Greek);
            Assert.Equal("Bot", bot.Name);
            Assert.Equal(Team.Greek, bot.Team);
            Assert.Equal(8,bot.NbPieces);
        }

        [Fact]
        public void TestConstructor()
        {
            Player boty = new EasyAI("Boty", Team.Roman);
            Assert.Equal("Boty", boty.Name);
            Assert.Equal(Team.Roman, boty.Team);
            Assert.Equal(8, boty.NbPieces);
        }

        [Fact]
        public void ChoosePiece()
        {
            var ai = new EasyAI("BotFacile", Team.Roman);
            var pieces = new List<Piece>
            {
                new Piece("AAAA", new Position(1, 1), new Piece.PieceOptions { Strength = 6, Team = Team.Roman }),
                new Piece("BBBB", new Position(2, 2), new Piece.PieceOptions { Strength = 8, Team = Team.Roman }),
                new Piece("CCCC", new Position(3, 3), new Piece.PieceOptions { Strength = 1, Team = Team.Roman })

            };

            var selected = ai.ChoosePiece(pieces);

            Assert.Contains(selected, pieces);
        }

        [Fact]
        public void ChooseMove2()
        {
            var ai = new EasyAI("BotFacile", Team.Roman);
            var moves = new List<Position>
        {
            new Position(1, 2),
            new Position(2, 3),
            new Position(4, 5)
        };

            var selectedMove = ai.ChooseMove(moves);

            Assert.Contains(selectedMove, moves);
        }

        [Fact]
        public void ChoosePieceEmpty()
        {
            var ai = new EasyAI("BotFacile", Team.Roman);
            var pieces = new List<Piece>();

            Assert.Throws<InvalidOperationException>(() => ai.ChoosePiece(pieces));
        }

        [Fact]
        public void ChooseMoveEmpty()
        {
            var ai = new EasyAI("BotFacile", Team.Roman);
            var positions = new List<Position>();

            Assert.Throws<InvalidOperationException>(() => ai.ChooseMove(positions));
        }

        [Fact]
        public void ChoosePieceNull()
        {
            var ai = new EasyAI("BotFacile", Team.Roman);
            Assert.Throws<InvalidOperationException>(() => ai.ChoosePiece(null));
        }

        [Fact]
        public void ChooseMoveNull()
        {
            var ai = new EasyAI("BotFacile", Team.Roman);
            Assert.Throws<InvalidOperationException>(() => ai.ChooseMove(null));
        }
    }

}

