/***************************************************************************
* Cell.cs
* -------------------------------------------------------------------------
* Project       : DouShouQi Mythology
* Author        : FORTUNE Gregoire
* Date          : 09/05/2025
* Description   : Represent the cell of the board
* -------------------------------------------------------------------------
***************************************************************************/
using System.ComponentModel;
using System.Runtime.Serialization;


namespace DouShouQiModel
{
    [DataContract]
    public class Cell
    {
        private int _row;
        private int _column;
        private CellType _type;
        private Team _teamCell;

        [DataMember]
        public int Row
        {
            get => _row;
            internal set
            {
                if (_row != value)
                {
                    _row = value;
                }
            }
        }

        [DataMember]
        public int Column
        {
            get => _column;
            internal set
            {
                if (_column != value)
                {
                    _column = value;
                }
            }
        }

        [DataMember]
        public CellType Type
        {
            get => _type;
            internal set
            {
                if (_type != value)
                {
                    _type = value;
                }
            }
        }

        [DataMember]
        public Team TeamCell
        {
            get => _teamCell;
            internal set
            {
                if (_teamCell != value)
                {
                    _teamCell = value;
                }
            }
        }

        /// <summary>
        /// Constructeur de la classe Cell
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <param name="type"></param>
        /// <param name="teamCell"></param>
        public Cell(int column, int row, CellType type = CellType.Normal, Team teamCell = Team.Unknown)
        {
            _row = row;
            _column = column;
            _type = type;
            _teamCell = teamCell;
        }
    }
}
