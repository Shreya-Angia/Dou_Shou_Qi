/***************************************************************************
 * TestRules.cs
 * -------------------------------------------------------------------------
 * Project       : DouShouQi Mythology
 * Author        : ANGIA Shreya
 * Date          : 29/04/2025
 * Description   : Unit tests for standard and custom rules of the game.                
 * -------------------------------------------------------------------------
 * Tests status : 20/22
 ***************************************************************************/

using System.Data;
using System.IO.Pipelines;
using DouShouQiModel;

namespace TestRules
{
    public class TestRules
    {
        /************************************************************************************/
        /******************************** Test StandardRules ********************************/
        /***********************************************************************************/

        [Fact]
        public void IsMoveValidForValidAdjacentMove() // Should return true
        {
            var piece = new Piece("Athena", new Position(2, 2), new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = false, CanJumpOverWater = false, Strength = 5, Team = Team.Greek });
            var end = new Position(2, 3);
            var allPieces = new List<Piece> { piece };
            var rules = new StandardRules();
            var board = new Board();

            bool result = rules.IsMoveValid(piece, end, allPieces, board);

            Assert.True(result);
        }

        [Fact]
        public void IsMoveValidWhenMovingOutOfBoard() // Should return false
        {
            var piece = new Piece("Athena", new Position(0 ,0), new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = false, CanJumpOverWater = false, Strength = 5, Team = Team.Greek });
            var end = new Position(-1, 0);
            var allPieces = new List<Piece> { piece };
            var rules = new StandardRules();
            var board = new Board();

            bool result = rules.IsMoveValid(piece, end, allPieces, board);

            Assert.False(result);
        }

        [Fact]
        public void IsMoveValidWhenMovingToSamePosition() // Should return false
        {
            var piece = new Piece("Athena", new Position(2, 2), new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = false, CanJumpOverWater = false, Strength = 5, Team = Team.Greek });
            var end = new Position(2, 2);
            var allPieces = new List<Piece> { piece };
            var rules = new StandardRules();
            var board = new Board();

            bool result = rules.IsMoveValid(piece, end, allPieces, board);

            Assert.False(result);
        }

        [Fact]
        public void IsMoveValidWhenMovingToTrap() // Should return false
        {
            var piece = new Piece("Athena", new Position(1, 2), new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = false, CanJumpOverWater = false, Strength = 5, Team = Team.Greek });
            var end = new Position(0, 2); // Trap position
            var allPieces = new List<Piece> { piece };
            var rules = new StandardRules();
            var board = new Board();

            bool result = rules.IsMoveValid(piece, end, allPieces, board);

            Assert.True(result);
        }

        [Fact]
        public void IsMoveValidWhenMovingToWater_AndCannotMoveOnWater() // Should return false
        {
            var piece = new Piece("Athena", new Position(2, 2), new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = false, CanJumpOverWater = false, Strength = 5, Team = Team.Greek });
            var end = new Position(4, 1); // Water position
            var allPieces = new List<Piece> { piece };
            var rules = new StandardRules();
            var board = new Board();


            bool result = rules.IsMoveValid(piece, end, allPieces, board);

            Assert.False(result);
        }

        [Fact]
        public void IsMoveValidWhenMovingToWater_AndCanMoveOnWater() // Should return true
        {
            var piece = new Piece("Poseidon", new Position(4, 1), new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = true, CanJumpOverWater = false, Strength = 7, Team = Team.Greek });
            var end = new Position(4, 2); // Autre case d'eau
            var allPieces = new List<Piece> { piece };
            var rules = new StandardRules();
            var board = new Board();

            bool result = rules.IsMoveValid(piece, end, allPieces, board);

            Assert.True(result);
        }

        [Fact]
        public void IsMoveValidWhenCapturingWeakerOpponent() // Should return true
        {
            var attacker = new Piece("Hades", new Position(2, 2), new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = false, CanJumpOverWater = false, Strength = 5, Team = Team.Greek });
            var defender = new Piece("Zeus", new Position(2, 3), new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = false, CanJumpOverWater = false, Strength = 3, Team = Team.Roman });
            var allPieces = new List<Piece> { attacker, defender };
            var rules = new StandardRules();
            var board = new Board();

            bool result = rules.IsMoveValid(attacker, defender.Position, allPieces, board);

            Assert.True(result);
        }

        [Fact]
        public void IsMoveValidWhenCapturingStrongerOpponent() // Should return false
        {
            var attacker = new Piece("Hades", new Position(2, 2), new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = false, CanJumpOverWater = false, Strength = 3, Team = Team.Greek });
            var defender = new Piece("Zeus", new Position(2, 3), new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = false, CanJumpOverWater = false, Strength = 5, Team = Team.Roman });
            var allPieces = new List<Piece> { attacker, defender };
            var rules = new StandardRules();
            var board = new Board();

            bool result = rules.IsMoveValid(attacker, defender.Position, allPieces, board);

            Assert.False(result);
        }


        [Fact]
        public void WaterCellType() 
        {
            var rules = new StandardRules();
            var pos = new Position { X = 4, Y = 1 };

            var result = rules.WhichCellType(pos);

            Assert.Equal(CellType.Water, result);
        }

        [Fact]
        public void SpecialRulesForPiece()
        {
            // Arrange  
            var rules = new StandardRules();
            var piece = new Piece(
                pieceName: "Poseidon",
                position: new Position { X = 0, Y = 0 },
                new Piece.PieceOptions
                {
                    InPlay = true,
                    IsSelected = false,
                    CanMoveOnWater = true,
                    CanJumpOverWater = false,
                    Strength = 10,
                }
            );

            // Assert
            Assert.True(piece.CanMoveOnWater);
        }



        //Test pour vérifier si une pièce peut capturer une autre pièce
        [Fact]
        public void TestCanCapture()
        {
            // Arrange
            var rules = new StandardRules();

            var attacker = new Piece("Hades", new Position { X = 2, Y = 2 }, new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = false, CanJumpOverWater = false, Strength = 5, Team = Team.Greek });
            var defender = new Piece("Zeus", new Position { X = 3, Y = 3 }, new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = false, CanJumpOverWater = false, Strength = 3, Team = Team.Roman });

            // Act
            bool canCapture = rules.CanCapture(attacker, defender);

            // Assert
            Assert.True(canCapture); // L'attaquant est plus fort, il doit pouvoir capturer
        }

        //Test to see if a piece looses it strenght when being on a trap
        [Fact]
        public void LooseStrenght_WhenOnTrap()
        {
            // Arrange
            var rules = new StandardRules();
            var piece = new Piece(
                pieceName: "Hades",
                position: new Position { X = 1, Y = 3 }, // Position d'un piège
                new Piece.PieceOptions
                {
                    InPlay = true,
                    IsSelected = false,
                    CanMoveOnWater = false,
                    CanJumpOverWater = false,
                    Strength = 5,
                    Team = Team.Greek
                }
            );

            // Act
            rules.LooseStrenghtPiece(piece, piece.Position);

            // Assert
            Assert.Equal(0, piece.Strength); // La force doit être réduite à 0 
        }

        //Test to see if a piece can jump over water 
        //[Fact]
        //public void CanJumpOverWaterWhenPieceCanJumpAndConditionsAreMet() // Should return true
        //{
        //    // Arrange
        //    var rules = new StandardRules();
        //    // La pièce peut sauter l'eau
        //    var piece = new Piece(
        //        pieceName: "Zeus",
        //        position: new Position(3, 1), // Position de départ
        //        inPlay: true,
        //        isSelected: false,
        //        canMoveOnWater: false, 
        //        canJumpOverWater: true,  
        //        strength: 8, 
        //        team: Team.Greek
        //    );
        //    // Destination de l'autre côté de l'eau (milieu = (4,1), qui est de l'eau)
        //    var destination = new Position(5, 1);

        //    // Act
        //    bool result = rules.CanJumpOverWater(piece, destination);

        //    // Assert
        //    Assert.True(result);
        //} 


        //Test to see if we can switch turn
        [Fact]
        public void CanSwitchTurn_TestWhenTrue() 
        {
            // Arrange
            var piece1 = new Piece("Zeus", new Position(2, 2), new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = false, CanJumpOverWater = false });
            var piece2 = new Piece("Athena", new Position(3, 3), new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = false, CanJumpOverWater = false });

            var playerPieces = new List<Piece> { piece1, piece2 };

            // Saves the previous positions of the pieces
            var previousPositions = new Dictionary<string, Position>
        {
            { piece1.PieceName, new Position(piece1.Position.X, piece1.Position.Y) },
            { piece2.PieceName, new Position(piece2.Position.X, piece2.Position.Y) }
        };

            // Moving piece1 to a new position
            piece1.MoveTo(new Position(4, 4)); 

            var rules = new StandardRules();
            bool result = rules.CanSwitchTurn(playerPieces, previousPositions);

            // Assert
            Assert.True(result);
        }

        // Test to see if we can switch turn
        [Fact]
        public void CanSwitchTurn_TestWhenFalse()
        {
            // Arrange
            var piece1 = new Piece("Zeus", new Position(2, 2), new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = false, CanJumpOverWater = false });
            var piece2 = new Piece("Athena", new Position(3, 3), new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = false, CanJumpOverWater = false });

            var playerPieces = new List<Piece> { piece1, piece2 };

            // Saves the previous positions of the pieces
            var previousPositions = new Dictionary<string, Position>
        {
            { piece1.PieceName, new Position(piece1.Position.X, piece1.Position.Y) },
            { piece2.PieceName, new Position(piece2.Position.X, piece2.Position.Y) }
        };

            var rules = new StandardRules();
            bool result = rules.CanSwitchTurn(playerPieces, previousPositions);

            // Assert  
            Assert.False(result);
        }

        [Fact]
        public void GainStrengthPiece_Hades()
        {
            // Arrange
            var rules = new StandardRules();
            var pieceGain = new Piece(
                pieceName: "Hades",
                position: new Position { X = 1, Y = 3 }, // Trap position
                new Piece.PieceOptions
                {
                    InPlay = true,
                    IsSelected = false,
                    CanMoveOnWater = false,
                    CanJumpOverWater = false,
                    Strength = 5,
                    Team = Team.Greek
                }
            );

            // Moving the piece to a new position 
            pieceGain.MoveTo(new Position(2, 2));

            // Act
            rules.GainStrenghtPiece(pieceGain, pieceGain.Position);
            // Assert
            Assert.Equal(5, pieceGain.Strength); // The strenght should be back at 5
        }

        [Fact]
        public void GainStrenghtPiece_Athena()
        {
            // Arrange
            var rules = new StandardRules();
            var piece = new Piece("Athena", new Position(1, 3), new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = false, CanJumpOverWater = false, Strength = 5, Team = Team.Greek });
            // Simule la perte de force sur un piège
            piece.Strength = 0;
            var normalPos = new Position(2, 2); // Case normale

            // Act
            rules.GainStrenghtPiece(piece, normalPos);

            // Assert
            Assert.Equal(5, piece.Strength); // Athena doit retrouver sa force de base
        }

        //[Fact]
        //public void GainStrenghtPiece_Jupiter() 
        //{
        //    // Arrange
        //    var rules = new StandardRules();
        //    var piece = new Piece("Jupiter", new Position(9, 3), true, false, false, false, 8, Team.Roman);
        //    piece.Strength = 0;
        //    var normalPos = new Position(5, 5); // Case normale

        //    // Act
        //    rules.GainStrenghtPiece(piece, normalPos);   

        //    // Assert
        //    Assert.Equal(8, piece.Strength); // Jupiter doit retrouver sa force de base
        //}

        [Fact]
        public void GainStrenghtPiece_DoesNothing_IfNotOnNormalCell()
        {
            // Arrange 
            var rules = new StandardRules();
            var piece = new Piece("Athena", new Position(1, 3), new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = false, CanJumpOverWater = false, Strength = 5, Team = Team.Greek });
            piece.Strength = 0;
            var trapPos = new Position(1, 3); // still on a trap

            // Act
            rules.GainStrenghtPiece(piece, trapPos);

            // Assert
            Assert.Equal(0, piece.Strength); // the strenght should still be 0
        }

        [Fact]
        public void GainStrenghtPiece_DoesNothing_IfStrengthNotZero()
        {
            // Arrange
            var rules = new StandardRules();
            var piece = new Piece("Athena", new Position(2, 2), new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = false, CanJumpOverWater = false, Strength = 5, Team = Team.Greek });
            var normalPos = new Position(2, 2);

            // Act
            rules.GainStrenghtPiece(piece, normalPos);

            // Assert
            Assert.Equal(5, piece.Strength); // The strenght does not change
        }

        [Fact]
        public void IsGameOverWhenGreekPieceInRomanHouse() // Should return true
        {
            var rules = new StandardRules();
            var pieces = new List<Piece>
        {
            new Piece("Zeus", new Position(8, 3), new Piece.PieceOptions { InPlay = true, Team = Team.Greek }),
            new Piece("Jupiter", new Position(5, 5), new Piece.PieceOptions { InPlay = true, Team = Team.Roman })
        };
            var teams = new List<Team> { Team.Greek, Team.Roman }; 

            bool result = rules.IsGameOver(pieces, teams);

            Assert.True(result); 
        } 

        [Fact]
        public void IsGameOverWhenRomanPieceInGreekHouse() // Should return true
        {
            var rules = new StandardRules();
                var pieces = new List<Piece>
        {
            new Piece("Zeus", new Position(5, 5), new Piece.PieceOptions { InPlay = true, Team = Team.Greek }),
            new Piece("Jupiter", new Position(0, 3), new Piece.PieceOptions { InPlay = true, Team = Team.Roman })
        };
            var teams = new List<Team> { Team.Greek, Team.Roman };

            bool result = rules.IsGameOver(pieces, teams);

            Assert.True(result); 
        }

        [Fact]
        public void IsGameOverWhenGreekHasNoPieces() // Should return true
        {
            var rules = new StandardRules();
            var pieces = new List<Piece>
        {
            new Piece("Jupiter", new Position(5, 5), new Piece.PieceOptions { InPlay = true, Team = Team.Roman })
            
        };
            var teams = new List<Team> { Team.Greek, Team.Roman };

            bool result = rules.IsGameOver(pieces, teams);

            Assert.True(result); 
        }

        [Fact]
        public void IsGameOverWhenRomanHasNoPieces() // Should return true
        {
            var rules = new StandardRules();
            var pieces = new List<Piece>
        {
            new Piece("Zeus", new Position(5, 5), new Piece.PieceOptions { InPlay = true, Team = Team.Greek })
            
        };
            var teams = new List<Team> { Team.Greek, Team.Roman };

            bool result = rules.IsGameOver(pieces, teams);

            Assert.True(result); 
        }

        [Fact]
        public void IsGameOverWhenGameIsNotOver() // Should return false
        {
            var rules = new StandardRules();  
            var pieces = new List<Piece>
        {
            new Piece("Zeus", new Position(2, 3), new Piece.PieceOptions { InPlay = true, Team = Team.Greek }),
            new Piece("Jupiter", new Position(5, 5), new Piece.PieceOptions { InPlay = true, Team = Team.Roman })
        };
            var teams = new List<Team> { Team.Greek, Team.Roman };

            bool result = rules.IsGameOver(pieces, teams);

            Assert.False(result); 
        }

        [Fact]
        public void IsMoveValid_ReturnsFalse_WhenDestinationIsWater_AndPieceCannotMoveOnWater()
        {
            // Arrange
            var piece = new Piece("Athena", new Position(2, 2), new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = false, CanJumpOverWater = false, Strength = 5, Team = Team.Greek });
            var end = new Position(4, 1); // (4,1) est une case d'eau
            var allPieces = new List<Piece> { piece };
            var rules = new StandardRules();
            var board = new Board();

            // Act
            bool result = rules.IsMoveValid(piece, end, allPieces, board);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsMoveValid_ReturnsTrue_WhenDestinationIsWater_AndPieceCanMoveOnWater()
        {
            // Arrange
            var piece = new Piece("Poseidon", new Position(4, 1), new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = true, CanJumpOverWater = false, Strength = 7, Team = Team.Greek });
            var end = new Position(4, 2); // (4,2) est aussi une case d'eau
            var allPieces = new List<Piece> { piece };
            var rules = new StandardRules();
            var board = new Board();

            // Act
            bool result = rules.IsMoveValid(piece, end, allPieces, board);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void WhichCellType_ReturnsHouse_ForRomanHousePosition()
        {
            var rules = new StandardRules();
            var pos = new Position(8, 3); // Position de la maison romaine

            var result = rules.WhichCellType(pos);

            Assert.Equal(CellType.House, result);
        }

        [Fact]
        public void WhichCellType_DoesNotReturnHouse_ForNonRomanHousePosition()
        {
            var rules = new StandardRules();
            var pos = new Position(7, 3); // Pas une maison

            var result = rules.WhichCellType(pos);

            Assert.NotEqual(CellType.House, result);
        }

        [Fact]
        public void WhichCellType_ReturnsHouse_ForGreekHousePosition()
        {
            var rules = new StandardRules();
            var pos = new Position(0, 3); // Position de la maison grecque

            var result = rules.WhichCellType(pos);

            Assert.Equal(CellType.House, result);
        }

        [Fact]
        public void WhichCellType_DoesNotReturnHouse_ForNonGreekHousePosition()
        {
            var rules = new StandardRules();
            var pos = new Position(1, 3); // Pas une maison grecque

            var result = rules.WhichCellType(pos);

            Assert.NotEqual(CellType.House, result);
        }

        [Theory]
        [InlineData("Heracles", 1)]
        [InlineData("Aphrodite", 2)]
        [InlineData("Hermes", 3)]
        [InlineData("Hephaistos", 4)]
        [InlineData("Athena", 5)]
        [InlineData("Hades", 6)]
        [InlineData("Poseidon", 7)]
        [InlineData("Zeus", 8)]
        public void GainStrenghtPiece_RestoresCorrectStrength_ForGreekPieces(string pieceName, int expectedStrength)
        {
            var rules = new StandardRules();
            var piece = new Piece(pieceName, new Position(2, 2), new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = false, CanJumpOverWater = false, Strength = 0, Team = Team.Greek });
            rules.GainStrenghtPiece(piece, piece.Position);
            Assert.Equal(expectedStrength, piece.Strength);
        }

        [Theory]
        [InlineData("Hercules", 1)]
        [InlineData("Venus", 2)]
        [InlineData("Mercury", 3)]
        [InlineData("Vulcan", 4)]
        [InlineData("Minerva", 5)]
        [InlineData("Pluto", 6)]
        [InlineData("Neptune", 7)]
        [InlineData("Jupiter", 8)]
        public void GainStrenghtPiece_RestoresCorrectStrength_ForRomanPieces(string pieceName, int expectedStrength)
        {
            var rules = new StandardRules();
            var piece = new Piece(pieceName, new Position(2, 2), new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = false, CanJumpOverWater = false, Strength = 0, Team = Team.Roman });
            rules.GainStrenghtPiece(piece, piece.Position);
            Assert.Equal(expectedStrength, piece.Strength);
        }

        [Fact]
        public void GainStrenghtPiece_DoesNothing_ForUnknownPieceName()
        {
            var rules = new StandardRules();
            var piece = new Piece("Inconnu", new Position(2, 2), new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = false, CanJumpOverWater = false, Strength = 0, Team = Team.Greek });
            rules.GainStrenghtPiece(piece, piece.Position);
            Assert.Equal(0, piece.Strength);
        }

        [Fact]
        public void GainStrenghtPiece_DoesNothing_ForUnknownTeam()
        {
            var rules = new StandardRules();
            var piece = new Piece("Heracles", new Position(2, 2), new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = false, CanJumpOverWater = false, Strength = 0, Team = Team.Unknown });
            rules.GainStrenghtPiece(piece, piece.Position);
            Assert.Equal(0, piece.Strength);
        }

        [Fact]
        public void CanSwitchTurn_ReturnsFalse_WhenPlayerPiecesIsNull()
        {
            var rules = new StandardRules();
            var previousPositions = new Dictionary<string, Position>();
            bool result = rules.CanSwitchTurn(null, previousPositions);
            Assert.False(result);
        }

        [Fact]
        public void CanSwitchTurn_ReturnsFalse_WhenPreviousPositionsIsNull()
        {
            var rules = new StandardRules();
            var playerPieces = new List<Piece>();
            bool result = rules.CanSwitchTurn(playerPieces, null);
            Assert.False(result);
        }

        [Fact]
        public void IsPieceHere_ReturnsTrue_WhenPieceIsAtDestination()
        {
            var rules = new StandardRules();
            var piece = new Piece("Athena", new Position(2, 2), new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = false, CanJumpOverWater = false, Strength = 5, Team = Team.Greek });
            var playerPieces = new List<Piece> { piece };
            var destination = new Position(2, 2);

            bool result = rules.IsPieceHere(playerPieces, destination);

            Assert.True(result);
        }

        [Fact]
        public void IsPieceHere_ReturnsFalse_WhenNoPieceAtDestination()
        {
            var rules = new StandardRules();
            var piece = new Piece("Athena", new Position(2, 2), new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = false, CanJumpOverWater = false, Strength = 5, Team = Team.Greek });
            var playerPieces = new List<Piece> { piece };
            var destination = new Position(3, 3);

            bool result = rules.IsPieceHere(playerPieces, destination);

            Assert.False(result);
        }


        [Fact]
        public void CanCapture_ReturnsFalse_WhenAttackerIsInWater()
        {
            var rules = new StandardRules();
            var attacker = new Piece("Poseidon", new Position(4, 1), new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = true, CanJumpOverWater = false, Strength = 7, Team = Team.Greek }); // Position d'eau
            var defender = new Piece("Zeus", new Position(4, 2), new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = false, CanJumpOverWater = false, Strength = 3, Team = Team.Roman });

            bool result = rules.CanCapture(attacker, defender);

            Assert.False(result); // L'attaquant ne peut pas capturer car il est dans l'eau
        }

        [Fact]
        public void CanCapture_ReturnsTrue_WhenAttackerIsRatAndDefenderIsElephant()
        {
            var rules = new StandardRules();
            var attacker = new Piece("Heracles", new Position(2, 2), new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = false, CanJumpOverWater = false, Strength = 1, Team = Team.Greek });
            var defender = new Piece("Zeus", new Position(2, 3), new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = false, CanJumpOverWater = false, Strength = 8, Team = Team.Roman });
            bool result = rules.CanCapture(attacker, defender);

            Assert.True(result); // Le plus faible peut capturer le plus fort
        }

        [Fact]
        public void CanCapture_ReturnsTrue_WhenAttackerStrengthEqualsDefenderStrength()
        {
            var rules = new StandardRules();
            var attacker = new Piece("Athena", new Position(2, 2), new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = false, CanJumpOverWater = false, Strength = 5, Team = Team.Greek });
            var defender = new Piece("Minerva", new Position(2, 3), new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = false, CanJumpOverWater = false, Strength = 5, Team = Team.Roman });

            bool result = rules.CanCapture(attacker, defender);

            Assert.True(result); // Même force, capture possible
        }

        [Fact]
        public void CanCapture_ReturnsTrue_WhenAttackerStrengthGreaterThanDefenderStrength()
        {
            var rules = new StandardRules();
            var attacker = new Piece("Zeus", new Position(2, 2), new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = false, CanJumpOverWater = false, Strength = 8, Team = Team.Greek });
            var defender = new Piece("Heracles", new Position(2, 3), new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = false, CanJumpOverWater = false, Strength = 1, Team = Team.Roman });

            bool result = rules.CanCapture(attacker, defender);

            Assert.True(result); // Plus fort, capture possible
        }

        [Fact]
        public void CanCapture_ReturnsFalse_WhenAttackerStrengthLessThanDefenderStrength_AndNotSpecialCase()
        {
            var rules = new StandardRules();
            var attacker = new Piece("Athena", new Position(2, 2), new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = false, CanJumpOverWater = false, Strength = 4, Team = Team.Greek });
            var defender = new Piece("Minerva", new Position(2, 3), new Piece.PieceOptions { InPlay = true, IsSelected = false, CanMoveOnWater = false, CanJumpOverWater = false, Strength = 5, Team = Team.Roman });

            bool result = rules.CanCapture(attacker, defender);

            Assert.False(result); // Moins fort, capture impossible
        }
    }
}





