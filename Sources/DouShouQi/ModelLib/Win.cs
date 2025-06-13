using System;

namespace DouShouQiModel
{
    public class Win
    {
        public string? Winner { get; set; } 
        public string? Text { get; set; } 

        /// <summary>
        /// When the Greek player wins the game. 
        /// </summary>
        public static Win GreekWin => new Win
        {
            Winner = "The Greek won!",
            Text = "From the heights of Olympus, your triumph echoes across the land!"
        };

        /// <summary>
        /// When the Roman player wins the game.
        /// </summary>
        public static Win RomanWin => new Win
        {
            Winner = "The Roman won!",
            Text = "The empire stands stronger than ever."
        };

        /// <summary>
        /// When the player plays against the AI and wins the game. 
        /// </summary>
        public static Win Solo => new Win
        {
            Winner = "Victory!",
            Text = "The city stands triumphant. Your name will echo through eternity."
        };
    }
}