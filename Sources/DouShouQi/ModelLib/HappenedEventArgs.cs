using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DouShouQiModel
{
    public class HappenedEventArgs : EventArgs
    {
        public Piece? AttackerPiece { get; private set; }
        public Piece? DefeatPiece { get; private set; }
        public Position? Destination { get; private set; }
        public Cell[,]? Board { get; private set; }
        public List<Piece>? AllPiece { get; private set; }

        /// <summary>
        /// Représente un événement qui se produit dans le jeu
        /// </summary>
        /// <param name="attackerPiece"></param>
        /// <param name="destination"></param>
        public HappenedEventArgs(Piece attackerPiece, Position destination)
        {
            AttackerPiece = attackerPiece;
            Destination = destination;
        }

        /// <summary>
        /// Représente un événement qui se produit dans le jeu
        /// </summary>
        /// <param name="attackerPiece"></param>
        /// <param name="defeatPiece"></param>
        /// <param name="destination"></param>
        public HappenedEventArgs(Piece attackerPiece, Piece defeatPiece, Position destination)
        {
            AttackerPiece = attackerPiece;
            DefeatPiece = defeatPiece;
            Destination = destination;
        }

        /// <summary>
        /// Représente un événement qui se produit dans le jeu
        /// </summary>
        /// <param name="board"></param>
        /// <param name="allPiece"></param>
        public HappenedEventArgs(Cell[,] board, List<Piece> allPiece)
        {
            Board = board;
            AllPiece = allPiece;
        }

        /// <summary>
        /// Représente un événement qui se produit dans le jeu
        /// </summary>
        public HappenedEventArgs()
        {

        }

    }
}
