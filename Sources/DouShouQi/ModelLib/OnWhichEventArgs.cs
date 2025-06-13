using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DouShouQiModel
{
    public class OnWhichEventArgs : EventArgs
    {
        public List<Piece>? ListPiece { get; private set; }
        public List<Position>? ListPosition { get; private set; }

        /// <summary>
        /// Pour l'affichage et la demande de la pièce
        /// </summary>
        /// <param name="listPiece"></param>
        public OnWhichEventArgs(List<Piece> listPiece)
        {
            ListPiece = listPiece;
        }

        /// <summary>
        /// Pour l'affichage et la demande de la position
        /// </summary>
        /// <param name="listPosition"></param>
        public OnWhichEventArgs(List<Position> listPosition)
        {
            ListPosition = listPosition;
        }
    }
}
