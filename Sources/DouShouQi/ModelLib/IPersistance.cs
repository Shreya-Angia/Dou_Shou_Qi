using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DouShouQiModel
{
    /// <summary>
    /// IPersistance interface for loading and saving.
    /// </summary>
    public interface IPersistance
    {
        /// <summary>
        /// Loads objects of type T.
        /// </summary>
        /// <typeparam name="T">The type of object to load.</typeparam>
        /// <returns>An array of loaded objects.</returns>
        public T[] Load<T>() where T : IIsPersistant;

        /// <summary>
        /// Saves the specified elements.
        /// </summary>
        /// <typeparam name="T">The type of object to save.</typeparam>
        /// <param name="elements">The array of objects to save.</param>
        public void Save<T>(T[] elements) where T : IIsPersistant;
    }
}