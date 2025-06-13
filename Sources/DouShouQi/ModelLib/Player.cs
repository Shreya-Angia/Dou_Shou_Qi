/***************************************************************************
* Player.cs
* -------------------------------------------------------------------------
* Project       : DouShouQi Mythology
* Author        : JOUVE Enzo
* Date          : 17/04/2025
* Description   : Player abstract.              
* -------------------------------------------------------------------------
***************************************************************************/

namespace DouShouQiModel
{
    public abstract class Player : IIsPersistant
    {
        /// <summary>
        /// Représente le nom du joueur ou de l'IA
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Représente l'équipe du joueur ou de l'IA
        /// </summary>
        public Team Team { get; private set; }
        /// <summary>
        /// Représente le nombre de pièces restantes du joueur ou de l'IA
        /// </summary>
        public int NbPieces { get; private set; }
        /// <summary>
        /// Représente le choix de l'humain pour son move de pièce
        /// </summary>
        public int? ChosenMoveIndex { get; set; }
        /// <summary>
        /// Représente le choix de l'humain de la pièce
        /// </summary>
        public int? ChosenPieceIndex { get; set; }

        /// <summary>
        /// Représente un joueur
        /// Classe abstraite donc il faut passer par HumanPlayer ou AIPlyaer pour instancier
        /// </summary>
        /// <param name="name">nom du joueur/IA</param>
        /// <param name="team">Greek ou Roman</param>
        protected Player(string name, Team team)
        {
            Name = name;
            Team = team;
            NbPieces = 8;
        }

        /// <summary>
        /// Permet d'obtenir le mouvement du joueur ou de l'IA
        /// </summary>
        /// <returns>Return 4 choix possible : 0,1 ou 1,0 ou -1,0 ou 0,-1</returns>
        public abstract Position ChooseMove(List<Position> positionParcourut);
        public abstract Piece ChoosePiece(List<Piece> pieceParcourut);
    }

}