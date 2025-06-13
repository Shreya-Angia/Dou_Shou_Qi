/***************************************************************************
* EasyAI.cs
* -------------------------------------------------------------------------
* Project       : DouShouQi Mythology
* Author        : Jouve Enzo
* Date          : 09/05/2025
* Description   : Represente une IA facile
* -------------------------------------------------------------------------
***************************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace DouShouQiModel
{
    public class EasyAI : AIPlayer
    {
        /// <summary>
        /// Représente une IA facile
        /// </summary>
        /// <param name="name"></param>
        /// <param name="team"></param>
        public EasyAI(string name, Team team) : base(name, team) { }

        /// <summary>
        /// Permet de renvoyer aléatoirement un déplacement aléatoire parmi ceux disponibles
        /// </summary>
        /// <param name="positionParcourut"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public override Position ChooseMove(List<Position>? positionParcourut)
        {
            if (positionParcourut == null || positionParcourut.Count == 0) 
                throw new InvalidOperationException("Erreur IA : choix de position null");
            
            int index = RandomNumberGenerator.GetInt32(positionParcourut.Count);
            return positionParcourut[index];
        }

        /// <summary>
        /// Permet de renvoyer aléatoirement une pièce parmi celles qui peuvent bouger
        /// </summary>
        /// <param name="pieceParcourut"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public override Piece ChoosePiece(List<Piece>? pieceParcourut)
        {
            if (pieceParcourut == null || pieceParcourut.Count == 0 )
                throw new InvalidOperationException("Erreur IA : choix de pièce null");

            int index = RandomNumberGenerator.GetInt32(pieceParcourut.Count);
            return pieceParcourut[index];
        }
    }
}
