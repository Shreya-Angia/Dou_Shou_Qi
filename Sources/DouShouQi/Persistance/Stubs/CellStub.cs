using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DouShouQiModel;

namespace Stubs
{
    public class TabCellStub
    {
        public Cell[,]? CreateStubBoard()
        {
            int columns = 9;
            int rows = 7;
            Cell[,] tabCell = new Cell[columns, rows];
            // Première colonne
            tabCell[0, 0] = new Cell(0, 0);
            tabCell[0, 1] = new Cell(0, 1);
            tabCell[0, 2] = new Cell(0, 2, CellType.Trap, Team.Unknown);
            tabCell[0, 3] = new Cell(0, 3, CellType.House, Team.Greek);
            tabCell[0, 4] = new Cell(0, 4, CellType.Trap, Team.Unknown);
            tabCell[0, 5] = new Cell(0, 5);
            tabCell[0, 6] = new Cell(0, 6);
            // Deuxieme colonne
            tabCell[1, 0] = new Cell(1, 0);
            tabCell[1, 1] = new Cell(1, 1);
            tabCell[1, 2] = new Cell(1, 2);
            tabCell[1, 3] = new Cell(1, 3, CellType.Trap, Team.Unknown);
            tabCell[1, 4] = new Cell(1, 4);
            tabCell[1, 5] = new Cell(1, 5);
            tabCell[1, 6] = new Cell(1, 6);
            // Troisieme colonne
            tabCell[2, 0] = new Cell(2, 0);
            tabCell[2, 1] = new Cell(2, 1);
            tabCell[2, 2] = new Cell(2, 2);
            tabCell[2, 3] = new Cell(2, 3);
            tabCell[2, 4] = new Cell(2, 4);
            tabCell[2, 5] = new Cell(2, 5);
            tabCell[2, 6] = new Cell(2, 6);
            // Quatrieme colonne
            tabCell[3, 0] = new Cell(3, 0);
            tabCell[3, 1] = new Cell(3, 1, CellType.Water, Team.Unknown);
            tabCell[3, 2] = new Cell(3, 2, CellType.Water, Team.Unknown);
            tabCell[3, 3] = new Cell(3, 3);
            tabCell[3, 4] = new Cell(3, 4, CellType.Water, Team.Unknown);
            tabCell[3, 5] = new Cell(3, 5, CellType.Water, Team.Unknown);
            tabCell[3, 6] = new Cell(3, 6);
            // Cinquieme colonne
            tabCell[4, 0] = new Cell(4, 0);
            tabCell[4, 1] = new Cell(4, 1, CellType.Water, Team.Unknown);
            tabCell[4, 2] = new Cell(4, 2, CellType.Water, Team.Unknown);
            tabCell[4, 3] = new Cell(4, 3);
            tabCell[4, 4] = new Cell(4, 4, CellType.Water, Team.Unknown);
            tabCell[4, 5] = new Cell(4, 5, CellType.Water, Team.Unknown);
            tabCell[4, 6] = new Cell(4, 6);
            // Sixieme colonne
            tabCell[5, 0] = new Cell(5, 0);
            tabCell[5, 1] = new Cell(5, 1, CellType.Water, Team.Unknown);
            tabCell[5, 2] = new Cell(5, 2, CellType.Water, Team.Unknown);
            tabCell[5, 3] = new Cell(5, 3);
            tabCell[5, 4] = new Cell(5, 4, CellType.Water, Team.Unknown);
            tabCell[5, 5] = new Cell(5, 5, CellType.Water, Team.Unknown);
            tabCell[5, 6] = new Cell(5, 6);
            // Septieme colonne
            tabCell[6, 0] = new Cell(6, 0);
            tabCell[6, 1] = new Cell(6, 1);
            tabCell[6, 2] = new Cell(6, 2);
            tabCell[6, 3] = new Cell(6, 3);
            tabCell[6, 4] = new Cell(6, 4);
            tabCell[6, 5] = new Cell(6, 5);
            tabCell[6, 6] = new Cell(6, 6);
            // Huitieme colonne
            tabCell[7, 0] = new Cell(7, 0);
            tabCell[7, 1] = new Cell(7, 1);
            tabCell[7, 2] = new Cell(7, 2);
            tabCell[7, 3] = new Cell(7, 3, CellType.Trap, Team.Unknown);
            tabCell[7, 4] = new Cell(7, 4);
            tabCell[7, 5] = new Cell(7, 5);
            tabCell[7, 6] = new Cell(7, 6);
            // Neuvieme colonne
            tabCell[8, 0] = new Cell(8, 0);
            tabCell[8, 1] = new Cell(8, 1);
            tabCell[8, 2] = new Cell(8, 2, CellType.Trap, Team.Unknown);
            tabCell[8, 3] = new Cell(8, 3, CellType.House, Team.Roman);
            tabCell[8, 4] = new Cell(8, 4, CellType.Trap, Team.Unknown);
            tabCell[8, 5] = new Cell(8, 5);
            tabCell[8, 6] = new Cell(8, 6);
            return tabCell;
        }
    }
}
