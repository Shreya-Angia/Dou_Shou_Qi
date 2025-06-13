/***************************************************************************
 * StandardRules.cs
 * -------------------------------------------------------------------------
 * Project       : DouShouQi Mythology
 * Author        : ANGIA Shreya
 * Date          : 24/04/2025
 * Description   : The classical rules of the game.               
 * -------------------------------------------------------------------------
 ***************************************************************************/


using System.IO.Pipelines;
using System.Reflection.Metadata.Ecma335;
using DouShouQiModel;
using Microsoft.VisualBasic;
namespace DouShouQiModel
{
    public class StandardRules : IRules 
    {

        // Prendre la piece, la team, la position(x et y de deplacement (ex:1,0) et le board

        // In game when the piece has moved, call looseStrenghtPiece to check if the piece is on a trap 
        //Afterwads, call GainStrenghtPiece to check if the piece is back on a normal cell   

        /// <summary>
        /// // Verifies if a move is valid according to the rules
        // 1. Verify if the destination is not out of the board
        // 2 Verfify if the destination cell is not the same as the current position
        // 3. Verify the type of the destination cell 
        // --> if water , call CanMoveOnWater 
        // 4. Verify if the destination cell is of distance 1 
        // --> if not, call CanJumpOverWater : to check if there is water in between and see if that piece can jump over water
        //5. Verify if the destination cell is not occupied by a piece
        // --> if it is , call CanCapture to check if the piece can capture the other piece
        /// </summary>
        /// <param name="startPosition"></param>  
        /// <param name="endPosition"></param>  
        /// <returns>bool</returns> 
        public bool IsMoveValid(Piece piece, Position endPosition, List<Piece> allPieces, Board board)
        {
            // 1. Verifying if the destination is not out of the board
            if (endPosition.X < 0 || endPosition.X >= Board.GetNbColumns() || endPosition.Y < 0 || endPosition.Y >= Board.GetNbRows())
                return false;

            // 2.Verfying if the destination cell is not the same as the current position
            if (piece.Position.Equals(endPosition))
                return false;

            // 3. Verifying the type of the destination cell
            CellType cellType = WhichCellType(endPosition);
            if (cellType == CellType.Water && !piece.CanMoveOnWater)
                return false;

            // 4. Verifying if the destination cell is not occupied by a piece
            var defender = allPieces.FirstOrDefault(p => p.Position.Equals(endPosition) && p.InPlay);
            if (defender != null)
                return CanCapture(piece, defender);

            // 5. Verifying if the destination is not the House of the player
            Cell cell = board.GetCell(endPosition);
            if (cellType == CellType.House && cell.TeamCell == piece.Team)
                return false;

            // the piece can move to the destination cell 
            return true;
        }


        /***************************************************************************************************************/

        /// <summary>
        /// Determines the type of cell at a given position
        /// </summary>
        /// <param name="position"></param>
        /// <returns>CellType</returns>
        public CellType WhichCellType(Position position)
        {
            // Lists the positions of water cells
            var waterPositions = new HashSet<(int X, int Y)>
            {
                (3, 1), (4, 1), (5, 1),
                (3, 2), (4, 2), (5, 2),
                (3, 4), (4, 4), (5, 4),
                (3, 5), (4, 5), (5, 5)
            };

            // Lists the positions of trap cells
            var trapPositions = new HashSet<(int X, int Y)>
            {
                (0, 2), (0, 4), (1, 3),
                (8, 2), (8, 4), (7, 3)
            };

            // Gives the position of roman house
            var romanHouse = new HashSet<(int X, int Y)>
            {
                (8, 3)
            };

            //Gives the position of the greek house
            var greekHouse = new HashSet<(int X, int Y)>
            {
                (0, 3)
            };

            // Verifies the type of cell at the given position 
            if (waterPositions.Contains((position.X, position.Y)))
            {
                return CellType.Water;
            }
            if (trapPositions.Contains((position.X, position.Y)))
            {
                return CellType.Trap;
            }
            if (romanHouse.Contains((position.X, position.Y)))
            {
                return CellType.House;
            }

            if (greekHouse.Contains((position.X, position.Y)))
            {
                return CellType.House;
            }

            return CellType.Normal; // The other cells are considered land 
        }

        /***************************************************************************************************************/


        /// <summary>
        /// Verifies if a piece can attack another piece
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="defender"></param>
        /// <returns>bool</returns>
        public bool CanCapture(Piece attacker, Piece defender)
        {
            //Verfies if the attacker and defender are not in the same team
            if (attacker.Team != defender.Team)
            {
                // The attacker can capture the defender if the defender has an equal or lower rank
                // and the attacker is not in the water
                if (WhichCellType(defender.Position) == CellType.Trap)
                {
                    return true; //The attacker can capture the defender
                }
                if ((((attacker.Strength == 1 && defender.Strength == 8) || attacker.Strength >= defender.Strength) &&WhichCellType(attacker.Position) != CellType.Water))
                {
                    return true; // The attacker can capture the defender 
                }

            }
            return false; // The attacker cannot capture the defender 

        }

        /***************************************************************************************************************/

        /// <summary>
        /// If a piece is on a trap it looses its strength
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="pos"></param>
        public void LooseStrenghtPiece(Piece piece, Position pos)
        {
            // Verifies if the piece is on a trap
            if (WhichCellType(pos) == CellType.Trap)
            {
                piece.Strength = 0; // The piece loses its strength 
            }
        }

        /***************************************************************************************************************/

        //When a piece goes back to a normal cell after being on a trap it gains its strength again
        public void GainStrenghtPiece(Piece piece, Position pos)
        {
            if (piece.Strength == 0 && WhichCellType(pos) == CellType.Normal)
            {
                piece.Strength = GetStrengthForPiece(piece.Team, piece.PieceName);
            }
        }

        private static int GetStrengthForPiece(Team team, string pieceName)
        {
            // Dictionnaires pour la correspondance nom-force
            Dictionary<string, int> greekStrengths = new()
            {
                { "Heracles", 1 },
                { "Aphrodite", 2 },
                { "Hermes", 3 },
                { "Hephaistos", 4 },
                { "Athena", 5 },
                { "Hades", 6 },
                { "Poseidon", 7 },
                { "Zeus", 8 }
            };

            Dictionary<string, int> romanStrengths = new()
            {
                { "Hercules", 1 },
                { "Venus", 2 },
                { "Mercury", 3 },
                { "Vulcan", 4 },
                { "Minerva", 5 },
                { "Pluto", 6 },
                { "Neptune", 7 },
                { "Jupiter", 8 }
            };

            return team switch
            {
                Team.Greek => greekStrengths.TryGetValue(pieceName, out int Strength) ? Strength : 0,
                Team.Roman => romanStrengths.TryGetValue(pieceName, out int Strength) ? Strength : 0,
                _ => 0
            };
        }

        /***************************************************************************************************************/

        /// <summary>
        /// Verifies if the game is over
        ///The game is over if one of the players has no pieces left
        ///or if one of the players has one of his piece on the enemy's house
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="nbPieces"></param> 
        /// <param name="cell"></param>  
        /// <returns>bool</returns>
        public bool IsGameOver(List<Piece> allPieces, List<Team> teamsToCheck) 
        {
            foreach (var team in teamsToCheck)
            {
                // 1. Verfies if there are any pieces left for the team
                bool hasPiece = allPieces.Any(p => p.Team == team && p.InPlay);
                if (!hasPiece)
                    return true;

                // 2. Verfies if one of the pieces is on the enemy's house
                // Enemy Houses : (9,4) for Greek, (1,4) for Roman
                Position enemyHouse = team == Team.Greek ? new Position(8, 3) : new Position(0, 3);
                bool pieceOnEnemyHouse = allPieces.Any(p => p.Team == team && p.InPlay && p.Position.X == enemyHouse.X && p.Position.Y == enemyHouse.Y);
                if (pieceOnEnemyHouse)
                    return true;
            }
            return false;
        }

        /***************************************************************************************************************/

        /// <summary>
        /// We can change turn only if one of the pieces of the current player has moved
        /// Therefore , we go through a list of the pieces of the team.
        /// </summary>
        /// <param name="playerPieces"></param>
        /// <param name="previousPositions"></param>  
        /// <returns>bool</returns>
        public bool CanSwitchTurn(List<Piece>? playerPieces, Dictionary<string, Position>? previousPositions)
        {
            if (playerPieces == null || previousPositions == null)
                return false;

            // it runs through the list of pieces of the player
            foreach (var piece in playerPieces)
            {
                //Here startPosition contains the previous position of the piece
                // We can compare with piece.Position 
                if (previousPositions.TryGetValue(piece.PieceName, out Position? startPosition) && !startPosition.Equals(piece.Position))
                {
                    return true; // One of the pieces has moved   
                }
            }

            return false; // No piece has moved
        }

        /***************************************************************************************************************/

        public bool IsPieceHere(List<Piece> playerPieces, Position destination)
        {
            // Check if the destination cell is occupied by another piece
            return playerPieces.Any(piece => piece.Position.Equals(destination));
        }
    }
}














