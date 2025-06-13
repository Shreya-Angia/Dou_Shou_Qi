using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using DouShouQiModel;

namespace Serialize
{
    public class FilePersistance : IPersistance
    {
        /// <summary>
        /// String representing the folder name for saving and loading.
        /// </summary>
        private readonly string folderName = "Files";

        /// <summary>
        /// String representing the directory name.
        /// </summary>
        private readonly string directory;

        /// <summary>
        /// Constructor for FilePersistance.
        /// </summary>
        public FilePersistance(string directory)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            this.directory = directory;
        }

        /// <summary>
        /// Loads the data found in the backup file in JSON format.
        /// </summary>
        /// <returns>Returns an array of all loaded elements.</returns>
        public T[] Load<T>() where T : IIsPersistant
        {
            string file = $"{typeof(T).Name.ToLower()}.json";

            Directory.SetCurrentDirectory(directory);

            if (!Directory.Exists(folderName))
                return [];

            Directory.SetCurrentDirectory(Path.Combine(Directory.GetCurrentDirectory(), folderName));

            if (!File.Exists(file))
                return [];

            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(
                typeof(T[]),
                typeof(IRules).Assembly.GetTypes()
                .Where(type => typeof(IRules).IsAssignableFrom(type) && type.IsClass)
            );
            T[]? elements;

            using (FileStream s = File.OpenRead(file))
            {
                elements = jsonSerializer.ReadObject(s) as T[];
            }

            return elements != null ? elements : Array.Empty<T>();
        }

        /// <summary>
        /// Saves the specified elements to the backup file in JSON format.
        /// </summary>
        /// <param name="elements">Elements to save in the backup file.</param>
        public void Save<T>(T[] elements) where T : IIsPersistant
        {
            string file = $"{typeof(T).Name.ToLower()}.json";

            Directory.SetCurrentDirectory(directory);

            if (!Directory.Exists(folderName))
                Directory.CreateDirectory(folderName);

            Directory.SetCurrentDirectory(Path.Combine(Directory.GetCurrentDirectory(), folderName));

            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(
                typeof(T[]),
                typeof(IRules).Assembly.GetTypes()
                .Where(type => typeof(IRules).IsAssignableFrom(type) && type.IsClass)
            );

            using (FileStream s = File.Create(file))
            {
                using (XmlDictionaryWriter writer = JsonReaderWriterFactory.CreateJsonWriter(
                                        s,
                                        Encoding.UTF8,
                                        false,
                                        true))
                {
                    jsonSerializer.WriteObject(writer, elements);
                }
            }
        }
    }
}
