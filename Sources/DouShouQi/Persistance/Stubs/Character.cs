/***************************************************************************
* Character.cs
* -------------------------------------------------------------------------
* Project       : DouShouQi Mythology
* Author        : ANGIA Shreya
* Date          : 29/05/2025
* Description   : Creates all the characters with its information             
* -------------------------------------------------------------------------
***************************************************************************/


using System.Collections.Generic;

namespace Stubs
{
    public class Character
    {
        public string? Name { get; set; }
        public string? Characteristic { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }

        /// <summary>
        /// Creating all the gods and godnesses with their information
        /// </summary>
        /// <returns>List<Characer></returns> 
        public static List<Character> GetAll() 
        {
            return new List<Character>
            {
               
                new Character
                {
                    Name = "Zeus - Jupiter",
                    Characteristic = "Sky",
                    Description = "Zeus, known as Jupiter in Roman mythology, is the mighty king of the gods and ruler of Mount Olympus. " +
                    "He governs the skies, thunder, and lightning, often seen wielding his powerful thunderbolt. As the god of law and order, he maintains balance among gods and mortals, though he is not without flaws. " +
                    "Zeus is known for his many adventures, his commanding presence, and his unwavering authority. In any conflict, his word is final.",
                    Image = "zeus_presentation.jpg"
                },
                new Character
                {
                    Name = "Poseidon - Neptune",
                    Characteristic = "Sea",
                    Description = "Poseidon, or Neptune to the Romans, is the god of the seas, storms, and earthquakes. With his signature trident, he commands the oceans and can stir up tempests or calm the waves at will. " +
                    "Fierce and unpredictable, Poseidon’s mood can shift like the tides. He is also the creator of horses and protector of sailors. As one of the three brothers who rule the world, his power is immense and his presence always demands respect.",
                    Image = "poseidon_presentation.jpg"
                },

                 new Character
                {
                    Name = "Hades - Pluto",
                    Characteristic = "Underworld",
                    Description = "Hades, also known as Pluto, is the god of the underworld and ruler of the realm of the dead. Unlike his brothers Zeus and Poseidon, Hades chooses to dwell away from Olympus, governing the shadowy lands below. " +
                    "He is a stern and just deity, overseeing the souls of the departed with unshakable fairness. Often misunderstood as evil, Hades is more solemn than sinister, and his dominion over death is both feared and respected.",
                    Image = "hades_presentation.jpg"
                },

                   new Character
                {
                    Name = "Athena - Minerva",
                    Characteristic = "War & Wisdom",
                    Description = "The daughter of Zeus, Athena was the goddess of battle strategy and wisdom, with her sacred symbols being the owl and the olive tree. Athena did not have a mother, being born directly from Zeus' head wearing a suit of armour! As goddess of battle strategy, she is the female counterpart of Ares. Athena is the patron god of the Greek city, Athens.",
                    Image = "athena_presentation.jpg"
                },

                    new Character
                {
                    Name = "Hephaestus - Vulcan",
                    Characteristic = "Fire & Metal",
                    Description = "Hephaestus, called Vulcan by the Romans, is the divine blacksmith and god of fire, metal, and craftsmanship. " +
                    "Though physically lame and often seen as less graceful than other gods, his hands create wonders no one else can match. " +
                    "He forges the weapons of gods and heroes, imbuing them with unmatched power. Working deep within volcanoes, Hephaestus turns raw elements into legendary creations, proving that true strength lies in mastery and perseverance.",
                    Image = "hephaestus_presentation.jpg"
                },

                 new Character
                {
                    Name = "Hermes - Mercure",
                    Characteristic = "Messenger",
                    Description = "Hermes, or Mercury in Roman mythology, is the swift-footed messenger of the gods. Known for his cleverness and wit, Hermes travels freely between the mortal world, Olympus, and the underworld. " +
                    "He is the god of travelers, merchants, and thieves, and often acts as a guide for souls entering the afterlife. With his winged sandals and staff, he moves at lightning speed and always has a trick or a message up his sleeve.",
                    Image = "hermes_presentation.jpg"
                },

                new Character
                {
                    Name = "Aphrodite - Venus",
                    Characteristic = "Love & Beauty",
                    Description = "Aphrodite, called Venus by the Romans, is the enchanting goddess of love, beauty, and desire. " +
                    "Born from the sea foam, she has a radiant charm that no god or mortal can resist. But her influence goes beyond romance—Aphrodite can shape destinies, start wars, or bring peace, all with the sway of a heart. " +
                    "Her beauty is legendary, and her ability to manipulate emotions makes her both powerful and dangerous.\r\n\r\n",
                    Image = "aphrodite_presentation.jpg"
                },
               
               
                new Character 
                {
                    Name = "Heracles - Hercules",
                    Characteristic = "Strength",
                    Description = "Heracles, known as Hercules in Roman myth, is the greatest of Greek heroes, famous for his extraordinary strength and indomitable spirit. " +
                    "Born as a demigod, he completed twelve impossible labors as punishment for a tragic mistake, proving himself worthy of immortality. Heracles embodies raw power, bravery, and endurance, often using force where others use wit. " +
                    "Though not always subtle, his heart is noble, and his name is synonymous with heroic legend.\r\n\r\n",
                    Image = "heracles_presentation.jpg"
                }
            };
        }
    }
}
