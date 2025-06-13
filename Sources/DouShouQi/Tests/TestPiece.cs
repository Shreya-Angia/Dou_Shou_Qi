/***************************************************************************
 * UnitTest1.cs
 * -------------------------------------------------------------------------
 * Project       : DouShouQi Mythology
 * Author        : ANGIA Shreya
 * Date          : 04/04/2025
 * Description   : Unit tests for the Piece class. Checks properties and 
 *                 methods such as initialization and movement functionalities.
 * -------------------------------------------------------------------------
 * Tests Status  : 2/2 (Passed)
 ***************************************************************************/


using Xunit;
using DouShouQiModel;

namespace TestPiece
{
    public class TestPiece
    {
        [Fact]
        public void InitialisationPiece()
        {
            // Arrange
            var position = new Position { X = 0, Y = 0 };
            var piece = new Piece("Piece1", position, new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = false });

            // Act
            bool inPlay = piece.InPlay;
            bool isSelected = piece.IsSelected;
            bool canMoveOnWater = piece.CanMoveOnWater; 
            bool canJumpOverWater = piece.CanJumpOverWater; 
            var currentPosition = piece.Position;

            // Assert
            Assert.True(inPlay, "The piece should be in play.");
            Assert.False(isSelected, "The piece should not be selected");
            Assert.False(canMoveOnWater, "The piece shouldn't be able to move on water.");
            Assert.False(canMoveOnWater, "The piece shouldn't be able to jump over water.");
            Assert.Equal(0, currentPosition.X);
            Assert.Equal(0, currentPosition.Y);
        }

        [Fact]
        public void TestMoveTo()
        {
            // Arrange
            var piece = new Piece("Piece1", new Position { X = 0, Y = 0 });
            var newPosition = new Position { X = 2, Y = 3 };

            // Act
            piece.MoveTo(newPosition);

            // Assert
            Assert.Equal(2, piece.Position.X);
            Assert.Equal(3, piece.Position.Y);
        }
    }
}
