using Xunit;
using DouShouQiModel;
using System.Collections.Generic;

namespace UnitTests
{
    public class BoardTests
    {
        [Fact]
        public void Board_Correct_Dimensions()
        {
            var board = new Board();
            var matrix = board.GetMatrix();
            Assert.Equal(9, matrix.GetLength(0));
            Assert.Equal(7, matrix.GetLength(1));
        }

        [Fact]
        public void GetNbRows_Should()
        {
            Assert.Equal(7, Board.GetNbRows());
        }

        [Fact]
        public void GetNbColumns_Should()
        {
            Assert.Equal(9, Board.GetNbColumns());
        }

        [Fact]
        public void InitializeBoard()
        {
            var board = new Board();
            var matrix = new Matrix(9, 7);

            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 7; y++)
                {
                    matrix[x, y] = new Cell(x, y, CellType.Normal);
                }
            }

            board.InitializeBoard(matrix);
            var resultMatrix = board.GetMatrix();

            Assert.Equal(CellType.Normal, resultMatrix[0, 0].Type);
            Assert.Equal(CellType.Normal, resultMatrix[8, 6].Type);
        }

        [Fact]
        public void PlacePiec()
        {
            var piece = new Piece("Tiger", new Position(0, 0), new Piece.PieceOptions {Team = Team.Greek });
            var newPos = new Position(2, 3);

            Board.PlacePiece(newPos, piece);

            Assert.Equal(2, piece.Position.X);
            Assert.Equal(3, piece.Position.Y);
        }

        [Fact]
        public void PlacePiece_Position_Invalid()
        {
            var piece = new Piece("Elephant", new Position(0, 0), new Piece.PieceOptions { Team = Team.Greek });
            var invalidPos = new Position(20, 10);

            Assert.Throws<ArgumentOutOfRangeException>(() => Board.PlacePiece(invalidPos, piece));
        }

        [Fact]
        public void GetCell()
        {
            var board = new Board();
            var matrix = new Matrix(9, 7);
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 7; y++)
                {
                    matrix[x, y] = new Cell(x, y, CellType.Normal);
                }
            }
            board.InitializeBoard(matrix);
            var pos = new Position(4, 2);

            var cell = board.GetCell(pos);

            Assert.Equal(4, cell.Column);
            Assert.Equal(2, cell.Row);
            Assert.Equal(CellType.Normal, cell.Type);
        }

        [Fact]
        public void AllMovePiece()
        {
            var board = new Board();
            var matrix = new Matrix(9, 7);
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 7; y++)
                {
                    matrix[x, y] = new Cell(x, y, CellType.Normal);
                }
            }
            board.InitializeBoard(matrix);

            var piece = new Piece("Lion", new Position(4, 3), new Piece.PieceOptions {Strength = 7, Team = Team.Roman });
            var player = new HumanPlayer("IA", Team.Roman);
            var pieces = new List<Piece> { piece };

            var rules = new StandardRules();

            var moves = Board.AllMovePiece(player, pieces, rules, board);

            Assert.True(moves.Count > 0);
            foreach (var (p, pos) in moves)
            {
                Assert.Equal(piece, p);
                Assert.InRange(pos.X, 0, 8);
                Assert.InRange(pos.Y, 0, 6);
            }
        }
    }
}
