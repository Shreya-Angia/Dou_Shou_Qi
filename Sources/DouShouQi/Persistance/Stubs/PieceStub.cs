using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DouShouQiModel;

namespace Stubs
{
    public class ListPieceStub
    {
        public List<Piece> CreateStubListPiece()
        {
            List<Piece> allPieces = new List<Piece>();
            // Pièces grecques
            allPieces.Add(new Piece("Heracles", new Position(2, 0), new Piece.PieceOptions
            {
                InPlay = true,
                IsSelected = false,
                CanMoveOnWater = true,
                CanJumpOverWater = false,
                Strength = 1,
                Team = Team.Greek
            }));
            allPieces.Add(new Piece("Aphrodite", new Position(1, 5), new Piece.PieceOptions
            {
                InPlay = true,
                IsSelected = false,
                CanMoveOnWater = false,
                CanJumpOverWater = false,
                Strength = 2,
                Team = Team.Greek
            }));
            allPieces.Add(new Piece("Hermès", new Position(2, 4), new Piece.PieceOptions
            {
                InPlay = true,
                IsSelected = false,
                CanMoveOnWater = false,
                CanJumpOverWater = false,
                Strength = 3,
                Team = Team.Greek
            }));
            allPieces.Add(new Piece("Héphaïstos", new Position(1, 1), new Piece.PieceOptions
            {
                InPlay = true,
                IsSelected = false,
                CanMoveOnWater = false,
                CanJumpOverWater = false,
                Strength = 4,
                Team = Team.Greek
            }));
            allPieces.Add(new Piece("Athena", new Position(2, 2), new Piece.PieceOptions
            {
                InPlay = true,
                IsSelected = false,
                CanMoveOnWater = false,
                CanJumpOverWater = false,
                Strength = 5,
                Team = Team.Greek
            }));
            allPieces.Add(new Piece("Hades", new Position(0, 6), new Piece.PieceOptions
            {
                InPlay = true,
                IsSelected = false,
                CanMoveOnWater = false,
                CanJumpOverWater = true,
                Strength = 6,
                Team = Team.Greek
            }));
            allPieces.Add(new Piece("Poseidon", new Position(0, 0), new Piece.PieceOptions
            {
                InPlay = true,
                IsSelected = false,
                CanMoveOnWater = false,
                CanJumpOverWater = true,
                Strength = 7,
                Team = Team.Greek
            }));
            allPieces.Add(new Piece("Zeus", new Position(2, 6), new Piece.PieceOptions
            {
                InPlay = true,
                IsSelected = false,
                CanMoveOnWater = false,
                CanJumpOverWater = false,
                Strength = 8,
                Team = Team.Greek
            }));

            // Pièces romaines
            allPieces.Add(new Piece("Hercules", new Position(6, 6), new Piece.PieceOptions
            {
                InPlay = true,
                IsSelected = false,
                CanMoveOnWater = true,
                CanJumpOverWater = false,
                Strength = 1,
                Team = Team.Roman
            }));
            allPieces.Add(new Piece("Vénus", new Position(7, 1), new Piece.PieceOptions
            {
                InPlay = true,
                IsSelected = false,
                CanMoveOnWater = false,
                CanJumpOverWater = false,
                Strength = 2,
                Team = Team.Roman
            }));
            allPieces.Add(new Piece("Mercury", new Position(6, 2), new Piece.PieceOptions
            {
                InPlay = true,
                IsSelected = false,
                CanMoveOnWater = false,
                CanJumpOverWater = false,
                Strength = 3,
                Team = Team.Roman
            }));
            allPieces.Add(new Piece("Vulcan", new Position(7, 5), new Piece.PieceOptions
            {
                InPlay = true,
                IsSelected = false,
                CanMoveOnWater = false,
                CanJumpOverWater = false,
                Strength = 4,
                Team = Team.Roman
            }));
            allPieces.Add(new Piece("Minerva", new Position(6, 4), new Piece.PieceOptions
            {
                InPlay = true,
                IsSelected = false,
                CanMoveOnWater = false,
                CanJumpOverWater = false,
                Strength = 5,
                Team = Team.Roman
            }));
            allPieces.Add(new Piece("Pluto", new Position(8, 0), new Piece.PieceOptions
            {
                InPlay = true,
                IsSelected = false,
                CanMoveOnWater = false,
                CanJumpOverWater = true,
                Strength = 6,
                Team = Team.Roman
            }));
            allPieces.Add(new Piece("Neptune", new Position(8, 6), new Piece.PieceOptions
            {
                InPlay = true,
                IsSelected = false,
                CanMoveOnWater = false,
                CanJumpOverWater = true,
                Strength = 7,
                Team = Team.Roman
            }));
            allPieces.Add(new Piece("Jupiter", new Position(6, 0), new Piece.PieceOptions
            {
                InPlay = true,
                IsSelected = false,
                CanMoveOnWater = false,
                CanJumpOverWater = false,
                Strength = 8,
                Team = Team.Roman
            }));

            return allPieces;
        }
    }
}
