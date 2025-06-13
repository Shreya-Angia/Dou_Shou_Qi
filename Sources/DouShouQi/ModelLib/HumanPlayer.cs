/***************************************************************************
 * HumanPlayer.cs
 * -------------------------------------------------------------------------
 * Project       : DouShouQi Mythology
 * Author        : JOUVE Enzo
 * Date          : 17/05/2025
 * Description   : Player jouable uniquement par un être humain.                
 * -------------------------------------------------------------------------
 * HumanPlayer(string name, Team team, int initialPieces = 8) : base(name, team, initialPieces)
 * Position ChooseMove(List<Position> positionParcourut)
 * Piece ChoosePiece(List<Piece> pieceParcourut)
 ***************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DouShouQiModel;

namespace DouShouQiModel
{
    /// <summary>
    /// Représente un player jouer par un être humain
    /// </summary>
    public class HumanPlayer : Player
    {
        /// <summary>
        /// Représente un joueur jouable uniquement par un être humain
        /// </summary>
        /// <param name="name"></param>
        /// <param name="team"></param>
        public HumanPlayer(string name, Team team)
            : base(name, team) { }

        public event EventHandler<OnWhichEventArgs>? AskWhichPiece;
        public event EventHandler<OnWhichEventArgs>? AskWhichMove;
        protected virtual void OnPieceAsk(OnWhichEventArgs args) => AskWhichPiece?.Invoke(this, args);
        protected virtual void OnMoveAsk(OnWhichEventArgs args) => AskWhichMove?.Invoke(this, args);

        /// <summary>
        /// Demande au joueur, par le biais d'un événement, le mouvement qu'il veut faire
        /// </summary>
        /// <param name="positionParcourut"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public override Position ChooseMove(List<Position> positionParcourut)
        {
            ChosenMoveIndex = null;

            OnMoveAsk(new OnWhichEventArgs(positionParcourut));
            if (ChosenMoveIndex == null || ChosenMoveIndex < 0 || ChosenMoveIndex >= positionParcourut.Count)
                throw new InvalidOperationException("Choix invalide");

            return positionParcourut[ChosenMoveIndex.Value];
        }

        /// <summary>
        /// Demande au joueur, par le biais d'un événement, la pièce qu'il veut déplacer
        /// </summary>
        /// <param name="pieceParcourut"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public override Piece ChoosePiece(List<Piece> pieceParcourut)
        {
            ChosenPieceIndex = null;

            OnPieceAsk(new OnWhichEventArgs(pieceParcourut));
            if (ChosenPieceIndex == null || ChosenPieceIndex < 0 || ChosenPieceIndex >= pieceParcourut.Count)
                throw new InvalidOperationException("Choix invalide");

            return pieceParcourut[ChosenPieceIndex.Value];
        }
    }
}