/***************************************************************************
* AIPlayer.cs
* -------------------------------------------------------------------------
* Project       : DouShouQi Mythology
* Author        : Jouve Enzo
* Date          : 17/05/2025
* Description   : Represente un joueur controlé par une IA
* -------------------------------------------------------------------------
***************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DouShouQiModel;

namespace DouShouQiModel
{
    /// <summary>
    /// Représente un joueur controlé par une ~IA
    /// </summary>
    public abstract class AIPlayer : Player
    {
        /// <summary>
        /// Représente un joueur controlé par une IA
        /// </summary>
        /// <param name="name"></param>
        /// <param name="team"></param>
        protected AIPlayer(string name, Team team): base(name, team) { }

        /// <summary>
        /// Permet de renvoyer aléatoirement une pièce parmi celles qui peuvent bouger
        /// </summary>
        /// <param name="positionParcourut"></param>
        /// <returns></returns>
        public override abstract Position ChooseMove(List<Position> positionParcourut);

    }

}