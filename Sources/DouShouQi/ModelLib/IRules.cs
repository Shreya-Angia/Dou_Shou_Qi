/***************************************************************************
 * IRules.cs
 * -------------------------------------------------------------------------
 * Project       : DouShouQi Mythology
 * Author        : ANGIA Shreya
 * Date          : 24/04/2025
 * Description   : Interface for rules of the game.                
 * -------------------------------------------------------------------------
 ***************************************************************************/

using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using DouShouQiModel;

namespace DouShouQiModel
{
    public interface IRules
    {
        /// <summary>
        /// Verifies if a piece can move to the destination cell
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="endPosition"></param>
        /// <param name="allPieces"></param>
        /// <param name="board"></param>
        /// <returns></returns>
        bool IsMoveValid(Piece piece, Position endPosition, List<Piece> allPieces, Board board);

        /// <summary>
        /// Verifies if a piece can move to the destination cell
        /// </summary>
        /// <param name="playerPieces"></param>
        /// <param name="previousPositions"></param>
        /// <returns></returns>
        bool CanSwitchTurn(List<Piece> playerPieces, Dictionary<string, Position> previousPositions);

        /// <summary>
        /// Verifies if a piece is in the destination cell
        /// </summary>
        /// <param name="playerPieces"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public bool IsPieceHere(List<Piece> playerPieces, Position destination);

        /// <summary>
        /// Verifies if the game is over
        /// </summary>
        /// <param name="allPieces"></param>
        /// <param name="teamsToCheck"></param>
        /// <returns></returns>
        public bool IsGameOver(List<Piece> allPieces, List<Team> teamsToCheck);

        /// <summary>
        /// Verifies if the game is over
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public CellType WhichCellType(Position position);

    }
}






