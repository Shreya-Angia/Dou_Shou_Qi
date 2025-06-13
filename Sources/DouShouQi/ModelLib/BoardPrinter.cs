/***************************************************************************
* BoardPrinter.cs
* -------------------------------------------------------------------------
* Project       : DouShouQi Mythology
* Author        : Jouve Enzo; BOU--JAHAN Anaé
* Date          : 09/05/2025
* Description   : Nécessaire pou une bonn affichage du plateau de jeu
* -------------------------------------------------------------------------
***************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DouShouQiModel
{
    public class BoardPrinter
    {
        public event EventHandler<HappenedEventArgs>? BoardAffich;
        public virtual void OnBoardAffich(HappenedEventArgs args) => BoardAffich?.Invoke(this, args);

		/// <summary>
		/// Renvoie le bon symbole pour une case
		/// </summary>
		/// <param name="cell"></param>
		/// <returns></returns>
		public static char GetSymbolCell(Cell cell)
        {
            return cell.Type switch
            {
                CellType.Normal => '.',
                CellType.Water => '~',
                CellType.Trap => '!',
                CellType.House => 'H',
                _ => '?'
            };
        }

		/// <summary>
		/// Renvoie le bon symbole pour une pièce
		/// </summary>
		/// <param name="piece"></param>
		/// <returns></returns>
		public static char GetSymbolPiece(Piece piece)
        {
            return piece.Strength switch
            {
                1 => '1',
                2 => '2',
                3 => '3',
                4 => '4',
                5 => '5',
                6 => '6',
                7 => '7',
                8 => '8',
                _ => '?'
            };
        }
		/// <summary>
		/// Renvoie la couleur de la pièce en fonction de son équipe
		/// </summary>
		/// <param name="team"></param>
		/// <returns></returns>
		public static ConsoleColor GetColor(Team team)
        {
            return team switch
            {
                Team.Roman => ConsoleColor.Red,
                Team.Greek => ConsoleColor.Blue,
                _ => ConsoleColor.White
            };
        }
    }
}
