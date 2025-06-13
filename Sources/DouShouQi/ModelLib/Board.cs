/***************************************************************************
* Board.cs
* -------------------------------------------------------------------------
* Project       : DouShouQi Mythology
* Author        : FORTUNE Gregoire
* Date          : 09/05/2025
* Description   : Plateau de jeu
* -------------------------------------------------------------------------
***************************************************************************/
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace DouShouQiModel
{
    [DataContract]
    public class Matrix : INotifyPropertyChanged
    {
        [DataMember]
        private readonly Cell[,] cells;
        [DataMember]
        public int Columns { get; }
        [DataMember]
        public int Rows { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Matrix(int columns, int rows)
        {
            Columns = columns;
            Rows = rows;
            cells = new Cell[columns, rows];
        }

        public void SetCells(List<Cell> newCells)
        {
            ArgumentNullException.ThrowIfNull(newCells);
            if (newCells.Count != Columns * Rows)
                throw new ArgumentException("La taille de la liste ne correspond pas à la matrice.");

            for (int col = 0; col < Columns; col++)
            {
                for (int row = 0; row < Rows; row++)
                {
                    cells[col, row] = newCells[col * Rows + row];
                    OnPropertyChanged($"Item[{col},{row}]");
                }
            }
        }
        public Cell this[int column, int row]
        {
            get
            {
                if (column < 0 || column >= Columns)
                    throw new ArgumentOutOfRangeException(nameof(column), "Index en dehors des limites de la matrice.");
                if (row < 0 || row >= Rows)
                    throw new ArgumentOutOfRangeException(nameof(row), "Index en dehors des limites de la matrice.");
                return cells[column, row];
            }
            set
            {
                if (column < 0 || column >= Columns)
                    throw new ArgumentOutOfRangeException(nameof(column), "Index en dehors des limites de la matrice.");
                if (row < 0 || row >= Rows)
                    throw new ArgumentOutOfRangeException(nameof(row), "Index en dehors des limites de la matrice.");
                cells[column, row] = value;
                OnPropertyChanged($"Item[{column},{row}]");
            }
        }

    }
    [DataContract]
    public class Board
    {
        /// <summary>
        /// Valeur par défaut du nombre de lignes
        /// </summary>
        [DataMember(Order = 1)]
        private const int nbRows = 7;

        /// <summary>
        /// Valeur par défaut du nombre de colonnes
        /// </summary>
        [DataMember(Order = 2)]
        private const int nbColumns = 9;

        /// <summary>
        /// Matrice représentant le plateau de jeu
        /// </summary>
        private Cell[,] matrixBoard;

        [DataMember(Name = "MatrixBoard", Order = 3)]
        private List<Cell>? MatrixBoardSurrogate
        {
            get
            {
                var list = new List<Cell>(matrixBoard.GetLength(0) * matrixBoard.GetLength(1));
                for (int i = 0; i < matrixBoard.GetLength(0); i++)
                    for (int j = 0; j < matrixBoard.GetLength(1); j++)
                        list.Add(matrixBoard[i, j]);
                return list;
            }
            set
            {
                matrixBoard = new Cell[nbColumns, nbRows];
                if (value == null) return;
                int columns = nbColumns;
                int rows = nbRows;
                for (int i = 0; i < columns; i++)
                    for (int j = 0; j < rows; j++)
                        matrixBoard[i, j] = value[i * rows + j];
            }
        }


        /// <summary>
        /// Constructeur de la classe Board avec des paramètres
        /// </summary>
        /// <param name="nbRows"> Nombre de lignes du plateau </param>
        /// <param name="nbColums">Nombre de colonnes du plateau </param>
        public Board(int nbColums = Board.nbColumns, int nbRows = Board.nbRows)
        {
            matrixBoard = new Cell[nbColums, nbRows];
        }

        /// <summary>
        /// Méthode pour initialiser le plateau avec une matrice de cellules
        /// </summary>
        /// <param name="cell"> Matrice de cellules à utiliser pour initialiser le plateau</param>
        public void InitializeBoard(Matrix matrix)
        {
            for (int i = 0; i < nbColumns; i++)
            {
                for (int j = 0; j < nbRows; j++)
                {
                    matrixBoard[i, j] = matrix[i, j];
                }
            }
        }

        /// <summary>
        /// Méthode pour obtenir la matrice du plateau
        /// </summary>
        /// <returns> Matrice de cellules représentant le plateau </returns>
        public Cell[,] GetMatrix() => matrixBoard;

        /// <summary>
        /// Méthode pour obtenir le nombre de lignes du plateau
        /// </summary>
        /// <returns> Nombre de lignes du plateau </returns>
        public static int GetNbRows() => nbRows;

        /// <summary>
        /// Méthode pour obtenir le nombre de colonnes du plateau
        /// </summary>
        /// <returns> Nombre de colonnes du plateau </returns>
        public static int GetNbColumns() => nbColumns;

        /// <summary>
        /// Méthode pour placer une pièce sur le plateau
        /// </summary>
        /// <param name="pos"> Position de destination de la pièce </param>
        /// <param name="piece"> Pièce à placer sur le plateau </param>
        /// <exception cref="ArgumentOutOfRangeException"> Lève une exception si la position est en dehors des limites du plateau </exception>
        public static void PlacePiece(Position pos, Piece piece)
        {
            if (pos.X >= 0 && pos.X < nbColumns && pos.Y >= 0 && pos.Y < nbRows)
            {
                piece.Position.X = pos.X;
                piece.Position.Y = pos.Y;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(pos), pos, "Position is out of bounds.");
            }

        }

        /// <summary>
        /// Méthode pour obtenir tous les mouvements possibles des pièces d'un joueur
        /// </summary>
        /// <param name="player"> Joueur dont on veut les mouvements possibles </param>
        /// <param name="lPiece"> Liste des pièces du jeu en temps réel </param>
        /// <param name="rules"> Règles du jeu </param>
        /// <returns></returns>
        public static List<(Piece, Position)> AllMovePiece(Player player, List<Piece> allPieces, IRules rules, Board board)
        {
            List<(Piece, Position)> list = new List<(Piece, Position)>();
            Team teamPlayer = player.Team;

            foreach (Piece piece in allPieces)
            {
                if (piece.Team != teamPlayer || !piece.InPlay)
                    continue;

                AddValidMovesForPiece(piece, rules, allPieces, board, list);
            }
            return list;
        }

        private static void AddValidMovesForPiece(Piece piece, IRules rules, List<Piece> allPieces, Board board, List<(Piece, Position)> list)
        {
            int[] dx = { 0, 1, 0, -1 };
            int[] dy = { 1, 0, -1, 0 };
            for (int dir = 0; dir < 4; dir++)
            {
                Position newPos = GetNextPosition(piece.Position, dx[dir], dy[dir]);

                if (rules.IsMoveValid(piece, newPos, allPieces, board))
                {
                    list.Add((piece, newPos));
                    continue;
                }

                if (piece.CanJumpOverWater)
                {
                    TryAddJumpMove(new JumpMoveContext(piece, dx[dir], dy[dir], rules, allPieces, board, list));
                }
            }
        }

        private sealed class JumpMoveContext
        {
            public Piece? Piece { get; }
            public int Dx { get; }
            public int Dy { get; }
            public IRules? Rules { get; }
            public List<Piece>? AllPieces { get; }
            public Board? Board { get; }
            public List<(Piece, Position)>? List { get; }

            public JumpMoveContext(Piece piece, int dx, int dy, IRules rules, List<Piece> allPieces, Board board, List<(Piece, Position)> list)
            {
                Piece = piece;
                Dx = dx;
                Dy = dy;
                Rules = rules;
                AllPieces = allPieces;
                Board = board;
                List = list;
            }
        }

        private static void TryAddJumpMove(JumpMoveContext context)
        {
            if (context.Piece == null)
                throw new ArgumentNullException(nameof(context), "Le paramètre 'Piece' de JumpMoveContext est null.");
            if (context.Board == null)
                throw new ArgumentNullException(nameof(context), "Le paramètre 'Board' de JumpMoveContext est null.");
            if (context.Rules == null)
                throw new ArgumentNullException(nameof(context), "Le paramètre 'Rules' de JumpMoveContext est null.");
            if (context.List == null)
                throw new ArgumentNullException(nameof(context), "Le paramètre 'List' de JumpMoveContext est null.");
            if (context.AllPieces == null)
                throw new ArgumentNullException(nameof(context), "Le paramètre 'AllPieces' de JumpMoveContext est null.");

            Position? jumpPos = GetJumpedPosition(context.Piece.Position, context.Dx, context.Dy, context.Board);
            if (jumpPos != null && context.Rules.IsMoveValid(context.Piece, jumpPos, context.AllPieces, context.Board))
            {
                context.List.Add((context.Piece, jumpPos));
            }
        }

        private static Position GetNextPosition(Position pos, int dx, int dy)
        {
            return new Position(pos.X + dx, pos.Y + dy);
        }

        private static Position? GetJumpedPosition(Position start, int dx, int dy, Board board)
        {
            int newX = start.X + dx;
            int newY = start.Y + dy;
            int maxColumns = Board.GetNbColumns();
            int maxRows = Board.GetNbRows();

            while (IsWithinBounds(newX, newY, maxColumns, maxRows) && board.GetCell(new Position(newX, newY)).Type == CellType.Water)
            {
                newX += dx;
                newY += dy;
            }

            if (IsWithinBounds(newX, newY, maxColumns, maxRows))
                return new Position(newX, newY);
            return null;
        }

        private static bool IsWithinBounds(int x, int y, int maxColumns, int maxRows)
        {
            return x >= 0 && x < maxColumns && y >= 0 && y < maxRows;
        }

        /// <summary>
        /// Méthode pour obtenir la valeur d'une cellule à une position donnée
        /// </summary>
        /// <param name="pos"> Position de la cellule </param>
        /// <returns> La cellule à la position donnée </returns>
        public Cell GetCell(Position pos) => matrixBoard[pos.X, pos.Y];
    }
}
