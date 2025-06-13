/***************************************************************************
* Position.cs
* -------------------------------------------------------------------------
* Project       : DouShouQi Mythology
* Author        : FORTUNE Grégoire ; ANGIA Shreya
* Date          : 04/04/2025
* Description   : Defines the Position class with its properties and methods.              
* -------------------------------------------------------------------------
***************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DouShouQiModel
{
    public sealed class Position : IEquatable<Position>
    {
        /// <summary>
        /// Représente les x et y par rapport aux plateau
        /// </summary>
        public int X { get; set; }
        public int Y { get; set; }

        public Position() { }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Verifies if the position is equal to another position
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Position? other)
        {
            if (other == null) return false; 
            return X == other.X && Y == other.Y; 
        } 



        /// <summary>
        /// Verifies if the position is equal to another object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            // Check null
            if  (ReferenceEquals(obj, null)) return false; 

            // Check reference equality
            if (ReferenceEquals(this, obj)) return true;

            // Check types 
            if (obj.GetType() != GetType()) return false;

            return Equals(obj as Position); 
        }

        /// <summary>
        /// Calculates the hash code for the position
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}
