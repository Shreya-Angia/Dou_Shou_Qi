/***************************************************************************
* Piece.cs
* -------------------------------------------------------------------------
* Project       : DouShouQi Mythology
* Author        : ANGIA Shreya
* Date          : 04/04/2025
* Description   : Defines the Piece class and its properties and methods.              
* -------------------------------------------------------------------------
***************************************************************************/


using System.ComponentModel;

namespace DouShouQiModel
{
    public class Piece : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged; 

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Properties
        private Position _position;
        public Position Position
        {
            get => _position;
            private set
            {
                if (!_position.Equals(value))
                {
                    _position = value;
                    OnPropertyChanged(nameof(Position));
                }
            }
        }

        private bool _inPlay;
        public bool InPlay
        {
            get => _inPlay;
            private set
            {
                if (_inPlay != value)
                {
                    _inPlay = value;
                    OnPropertyChanged(nameof(InPlay));
                }
            }
        }
        public void SetInPlay(bool value)
        {
            InPlay = value;
            OnPropertyChanged(nameof(InPlay));
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            private set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged(nameof(IsSelected));
                }
            }
        }
        public void SetSelected(bool value)
        {
            IsSelected = value;
            OnPropertyChanged(nameof(IsSelected));
        }

        private bool _canMoveOnWater;
        public bool CanMoveOnWater
        {
            get => _canMoveOnWater;
            private set
            {
                if (_canMoveOnWater != value)
                {
                    _canMoveOnWater = value;
                    OnPropertyChanged(nameof(CanMoveOnWater));
                }
            }
        }

        private bool _canJumpOverWater;
        public bool CanJumpOverWater
        {
            get => _canJumpOverWater;
            private set
            {
                if (_canJumpOverWater != value)
                {
                    _canJumpOverWater = value;
                    OnPropertyChanged(nameof(CanJumpOverWater));
                }
            }
        }

        private string _pieceName;
        public string PieceName
        {
            get => _pieceName;
            private set
            {
                if (_pieceName != value)
                {
                    _pieceName = value;
                    OnPropertyChanged(nameof(PieceName));
                }
            }
        }
        public void SetPieceName(string name)
        {
            PieceName = name;
            OnPropertyChanged(nameof(PieceName));
        }

        private int _strength;
        public int Strength
        {
            get => _strength;
            set
            {
                if (_strength != value)
                {
                    _strength = value;
                    OnPropertyChanged(nameof(Strength));
                }
            }
        }

        private Team _team;
        public Team Team
        {
            get => _team;
            private set
            {
                if (_team != value)
                {
                    _team = value;
                    OnPropertyChanged(nameof(Team));
                }
            }
        }

        public class PieceOptions
        {
            public bool InPlay { get; set; } = true;
            public bool IsSelected { get; set; } = false;
            public bool CanMoveOnWater { get; set; } = false;
            public bool CanJumpOverWater { get; set; } = false;
            public int Strength { get; set; } = 0;
            public Team Team { get; set; } = Team.Unknown;
        }

        ////////// Constructor //////////
        public Piece(string pieceName, Position position, PieceOptions? options = null)
        {
            _pieceName = pieceName;
            _position = position;
            options ??= new PieceOptions();
            _inPlay = options.InPlay;
            _isSelected = options.IsSelected;
            _canMoveOnWater = options.CanMoveOnWater;
            _canJumpOverWater = options.CanJumpOverWater;
            _strength = options.Strength;
            _team = options.Team;
        }

        /////////// Methods ///////////
        public int GetStrength()
        {
            return Strength;
        }

        public void MoveTo(Position newPosition)
        {
            if (newPosition != null)
            {
                Position = newPosition;
                OnPropertyChanged(nameof(Position));
            }
        }

        public bool IsAlive(Piece piece)
        {
            return InPlay;
        }

        public void RemoveFromPlay()
        {
            InPlay = false;
            OnPropertyChanged(nameof(InPlay));
        }
    }
}
