/***************************************************************************
* Game.cs
* -------------------------------------------------------------------------
* Project       : DouShouQi Mythology
* Author        : Jouve Enzo; BOU--JAHAN Anaé
* Date          : 09/05/2025
* Description   : Implémentation du moteur de jeu pour DouShouQi
* -------------------------------------------------------------------------
***************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Numerics;
using System.Runtime.CompilerServices;
using DouShouQiModel;

namespace DouShouQiModel
{
    public class Game : IIsPersistant
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// Représente le plateau de jeu
        /// </summary>
        private Board? _board = null;
        /// <summary>
        /// Représente le plateau de jeu
        /// </summary>
        public Board? Board
        {
            get => _board;
            set
            {
                if (_board != value)
                {
                    _board = value;
                    OnPropertyChanged();
                }
            }
        }
        /// <summary>
        /// Représente le joueur 1
        /// </summary>
        public Player? Player1 { get; set; }
        /// <summary>
        /// Représente le joueur 2
        /// </summary>
        public Player? Player2 { get; set; }
        /// <summary>
        /// Représente les règles du jeu
        /// </summary>
        public IRules? Rules { get; set; }
        /// <summary>
        /// Représente si le jeu est lancé ou pas
        /// </summary>
        private bool? _isGameLaunched;
        public bool? IsGameLaunched
        {
            get => _isGameLaunched;
            private set
            {
                if (_isGameLaunched != value)
                {
                    _isGameLaunched = value;
                    OnPropertyChanged();
                }
            }
        }
        /// <summary>
        /// Représente le tour actuel
        /// </summary>
        private int? _turnCounter;
        public int? TurnCounter
        {
            get => _turnCounter;
            private set
            {
                if (_turnCounter != value)
                {
                    _turnCounter = value;
                    OnPropertyChanged();
                }
            }
        }
        /// <summary>
        /// Représente le joueur actuel
        /// </summary>
        private Player? _currentPlayer;
        public Player? CurrentPlayer
        {
            get => _currentPlayer;
            private set
            {
                if (_currentPlayer != value)
                {
                    _currentPlayer = value;
                    OnPropertyChanged();
                }
            }
        }
        public void SetCurrentPlayer(Player player) => CurrentPlayer = player;

        /// <summary>
        /// Events pour notifier les actions du jeu
        /// </summary>
        public event EventHandler<HappenedEventArgs>? PieceMove;
        public event EventHandler<HappenedEventArgs>? PieceDefeat;
        public event EventHandler<GameInitEventArgs>? GameInit;
        public event EventHandler<FailedEventArgs>? Failed;
        public event EventHandler<HappenedEventArgs>? NextTurn;
        public event EventHandler<HappenedEventArgs>? GameWin;

        /// <summary>
        /// Notifie les événements du jeu
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnPieceMove(HappenedEventArgs args) => PieceMove?.Invoke(this, args);
        protected virtual void OnPieceDefeat(HappenedEventArgs args) => PieceDefeat?.Invoke(this, args);
        public virtual void OnFailed(FailedEventArgs args) => Failed?.Invoke(this, args);
        protected virtual void OnGameInit(GameInitEventArgs args) => GameInit?.Invoke(this, args);
        public virtual void OnNextTurn(HappenedEventArgs args) => NextTurn?.Invoke(this, args);
        public virtual void OnGameWin(HappenedEventArgs args) => GameWin?.Invoke(this, args);

        /// <summary>
        /// Constructeur de la classe Game
        /// </summary>
        public Game()
        {

        }

        public Game(Game game)
        {
            Board = game.Board;
            Player1 = game.Player1;
            Player2 = game.Player2;
            Rules = game.Rules;
            IsGameLaunched = game.IsGameLaunched;
            TurnCounter = game.TurnCounter;
            CurrentPlayer = game.CurrentPlayer;
        }
        /// <summary>
        /// Initialise le jeu en demandant les joueurs et les règles
        /// </summary>
        public void InitializeGame()
        {
            OnGameInit(new GameInitEventArgs());
        }

        /// <summary>
        /// Lance le jeu
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public void StartGame()
        {
            InitializeGame();

            if (Player1 == null || Player2 == null || Rules == null)
                throw new InvalidOperationException("Jeu non initialisé");

            TurnCounter = 1;
            IsGameLaunched = true;
            CurrentPlayer = Player1;
        }
        /// <summary>
        /// Change le joueur actuel et augmente le tour
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public void SwitchTurn()
        {
            if (CurrentPlayer == null || Player1 == null || Player2 == null)
                throw new InvalidOperationException("Switch turn impossible");

            if (CurrentPlayer == Player1)
            {
                CurrentPlayer = Player2;
                TurnCounter++;
            }
            else
            {
                CurrentPlayer = Player1;
                TurnCounter++;
            }
        }
        /// <summary>
        /// Attaque une pièce adverse
        /// </summary>
        /// <param name="attackerPiece"></param>
        /// <param name="location"></param>
        /// <param name="allPieces"></param>
        public void AttackPiece(Piece attackerPiece, Position location, List<Piece> allPieces)
        {
            foreach (Piece defeatPiece in allPieces)
            {
                if (defeatPiece.Position.X == location.X && defeatPiece.Position.Y == location.Y && defeatPiece.InPlay)
                {
                    OnPieceDefeat(new HappenedEventArgs(attackerPiece, defeatPiece, location));
                    defeatPiece.RemoveFromPlay();
                }
            }

        }
        /// <summary>
        /// Méthode pour faire un mouvement de pièce et une attaque potentielle
        /// </summary>
        /// <param name="currentPlayer"></param>
        /// <param name="board"></param>
        /// <param name="allPieces"></param>
        /// <param name="rules"></param>
        public void MakeMoove(Player currentPlayer, Board board, List<Piece> allPieces, IRules rules)
        {
            try
            {
                // On met dans une list l'ensemble des pièces du joueur qui peut bouger avec leurs mouvements disponibles
                List<(Piece piece, Position pos)> allMoves = Board.AllMovePiece(currentPlayer, allPieces, rules, board);

                // On stock uniquement les pièces disponibles dans cette list sans doublons
                List<Piece> pieceParcourut = new List<Piece>();
                for (int i = 0; i < allMoves.Count; i++)
                {
                    Piece piece = allMoves[i].piece; // pour récupérer seulement la pièce
                    if (!pieceParcourut.Contains(piece))
                    {
                        pieceParcourut.Add(piece);
                    }
                }


                // On obtient la pièce que le joueur humain veut jouer
                Piece pieceChoisi = currentPlayer.ChoosePiece(pieceParcourut);

                // Pour la piece choisi, on stock uniquement les mouvements disponibles dans cette list sans doublons
                List<Position> positionParcourut = new List<Position>();
                for (int i = 0; i < allMoves.Count; i++)
                {
                    if (allMoves[i].piece == pieceChoisi)
                    {
                        positionParcourut.Add(allMoves[i].pos);
                    }
                }

                // On obtient le mouvement que le joueur humain veut faire
                Position destination = currentPlayer.ChooseMove(positionParcourut);

                if (!rules.IsPieceHere(allPieces, destination))
                {
                    Board.PlacePiece(destination, pieceChoisi);
                    OnPieceMove(new HappenedEventArgs(pieceChoisi, destination));
                    return;
                }
                else
                {
                    AttackPiece(pieceChoisi, destination, allPieces);
                    OnPieceDefeat(new HappenedEventArgs(pieceChoisi, destination));
                    Board.PlacePiece(destination, pieceChoisi);
                    OnPieceMove(new HappenedEventArgs(pieceChoisi, destination));
                    return;

                }
            }
            catch (Exception e)
            {
                OnFailed(new FailedEventArgs(currentPlayer, e));
                MakeMoove(currentPlayer, board, allPieces, rules);
            }
        }
    }
}