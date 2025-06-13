/***************************************************************************
* RulesPage.cs
* -------------------------------------------------------------------------
* Project       : DouShouQi Mythology
* Author        : ANGIA Shreya
* Date          : 22/05/2025
* Description   : Creates the different sections of the rules page with
*                 their title, content and image.
* -------------------------------------------------------------------------
***************************************************************************/

using System;

namespace DouShouQiModel 
{ 
    public class RulesPage
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Image { get; set; }


        public static List<RulesPage> StandardRules()
        {
            return new List<RulesPage>
    {
        new RulesPage
        {
            Title = "1. Board Overview",
            Content =
                "The board is a 7x9 grid composed of 63 squares. Each player controls a pantheon: one side represents the Greek gods, " +
                "the other the Roman gods. The divine figures are placed on the first three rows of each player's side in a fixed formation. " +
                "The center of the board features rivers, while each player has a sacred sanctuary: Olympus for the Greeks, and the Capitoline Hill " +
                "for the Romans. These sanctuaries are the ultimate goal of the opposing pantheon. Understanding the terrain is vital, as it includes " +
                "strategic zones such as rivers and traps that can influence divine power.",
            Image = "board.png"
        },

        new RulesPage
        {
            Title = "2. Game Objective",
            Content =
                "The goal is to guide one of your divine figures into your opponent’s sanctuary. " +
                "The Greek side must reach the Capitoline Hill, while the Roman side must enter Olympus. " +
                "Whichever god or goddess reaches the enemy’s sacred space wins the game instantly. " +
                "Alternatively, victory can be achieved by defeating all enemy deities, leaving the opponent with no remaining moves. " +
                "Players must strike a balance between offense, defense, and territorial control to secure divine triumph.",
            Image = "endgame.jpg"
        },

        new RulesPage
        {
            Title = "3. Divine Hierarchy",
            Content =
                "Each pantheon has eight deities, ranked by power from 1 (weakest) to 8 (strongest):\n" +
                "- Greek: Heracles (1), Aphrodite (2), Hermes (3), Hephaestus (4), Athena (5), Hades (6), Poseidon (7), Zeus (8)\n" +
                "- Roman: Hercules (1), Venus (2), Mercury (3), Vulcan (4), Minerva (5), Pluto (6), Neptune (7), Jupiter (8)\n\n" +
                "In general, a deity can defeat another of equal or lower rank. However, divine exceptions exist: Heracles/Hercules, the demigod, " +
                "can overcome even Zeus/Jupiter, the king of the gods. This unexpected strength reflects mythological nuances and strategic depth.",
            Image = "team_gods.jpg"
        },

        new RulesPage
        {
            Title = "4. Movement",
            Content =
                "Each deity moves one square at a time in any of the four cardinal directions: up, down, left, or right. " +
                "They cannot move diagonally or leap over other figures. Most deities cannot cross rivers, which serve as natural obstacles. " +
                "A move is also how a deity can challenge an opponent by landing on the same square and invoking the rules of divine hierarchy. " +
                "A god may never enter their own sanctuary, which symbolizes their divine home and must be protected from enemy intrusion.",
            Image = "movement.png"
        },

        new RulesPage
        {
            Title = "5. Divine Powers",
            Content =
                "Some gods have unique movement abilities that reflect their mythological roles. " +
                "Poseidon/Neptune, as well as Hades/Pluto, can leap over rivers when the path is unobstructed, " +
                "Heracles/Hercules, though the weakest in rank, are the only ones capable of traversing river cells — " +
                "showing their strength and resilience. A deity’s divine power must be used wisely to surprise the opponent and manipulate the flow of battle.",
            Image = "special_moves.jpg"
        },

        new RulesPage
        {
            Title = "6. Special Board Areas",
            Content =
                "The board includes three special types of surfaces:\n" +
                "- Rivers : Only Heracles/Hercules may enter these waters.\n" +
                "- Traps : Located near each sanctuary, these spaces neutralize enemy gods who enter them, reducing their strength to 0 " +
                "and making them vulnerable to any attack.\n" +
                "- Sanctuaries: Olympus (Greek) and Capitoline Hill (Roman). Entering the enemy sanctuary results in immediate victory. " +
                "These areas are sacred and must be guarded at all costs.\n\n" +
                "Strategic control over these zones is often the key to achieving divine dominance.",
            Image = "special_cells.png"
        },

        new RulesPage
        {
            Title = "7. Divine Combat",
            Content =
                "Combat occurs when a god moves onto a square occupied by an enemy. Normally, a higher-ranked deity defeats a lower-ranked one. " +
                "However, there are key exceptions: Heracles/Hercules can defeat even Zeus/Jupiter. " +
                "If a god is standing on an enemy trap, their strength is reduced to 0 and they can be defeated by any opposing figure, regardless of rank. " +
                "Heracles/Hercules cannot attack or be attacked from within a river unless the opponent is also Heracles/Hercules. " +
                "These combat rules introduce both tension and opportunity in divine confrontations.",
            Image = "attack.png"
        },
    };
        }


        public static List<RulesPage> CustomRules()
        {
            return new List<RulesPage>
    {
        new RulesPage
        {
            Title = "1. Board Overview",
            Content =
                "The game is played on a 7x9 grid made up of 63 squares. Each player controls a pantheon of seven mythological gods — one Greek and one Roman. " +
                "At the center of the board lies the Fountain of Youth, a special tile with powerful effects. " +
                "On each player's side is their sanctuary: Mount Olympus for the Greeks and the Capitoline Hill for the Romans. " +
                "Strategic regions like rivers, traps, and sanctuaries shape movement and combat, creating a dynamic battlefield steeped in myth.",
            Image = "board.png"
        },

        new RulesPage
        {
            Title = "2. Game Objective",
            Content =
                "The goal of the game is to capture your opponent’s sanctuary by moving one of your gods into their divine stronghold. " +
                "At the same time, you must defend your own sanctuary from invasion. Players win by successfully entering the enemy’s sanctuary with one of their deities. " +
                "Gods can also defeat each other through combat, making elimination another pathway to gain dominance. " +
                "However, the game always ends the moment one sanctuary is captured — so timing and positioning are crucial.",
            Image = "endgame.jpg"
        },

        new RulesPage
        {
            Title = "3. Divine Hierarchy",
            Content =
               "Each pantheon has eight deities, ranked by power from 1 (weakest) to 8 (strongest):\n" +
                "- Greek: Heracles (1), Aphrodite (2), Hermes (3), Hephaestus (4), Athena (5), Hades (6), Poseidon (7), Zeus (8)\n" +
                "- Roman: Hercules (1), Venus (2), Mercury (3), Vulcan (4), Minerva (5), Pluto (6), Neptune (7), Jupiter (8)\n\n" +
                "In general, a deity can defeat another of equal or lower rank. However, divine exceptions exist: Heracles/Hercules, the demigod, " +
                "can overcome even Zeus/Jupiter, the king of the gods. This unexpected strength reflects mythological nuances and strategic depth.",
            Image = "team_gods.jpg"
        },

        new RulesPage
        {
            Title = "4. Movement",
            Content =
                "Most gods move one square at a time in a horizontal or vertical direction. However, several deities have unique movement traits. " +
                "Hermes and Mercury can move two squares per turn, allowing rapid traversal. Heracles and Hercules, as well as Poseidon and Neptune, can walk on water. " +
                "Athena and Minerva move normally but can only attack diagonally. Mastering each god’s movement potential is essential to building a winning strategy.",
            Image = "movement.png"
        },

        new RulesPage
        {
            Title = "5. Divine Powers",
            Content =
                "Each deity possesses a unique mythological ability that influences gameplay. These abilities can be passive (always active) or active (used intentionally, sometimes with a cooldown or limited use). Understanding and timing these powers is essential to victory:\n\n" +

                "⚡ Zeus / Jupiter: Can strike all adjacent tiles with lightning, stunning nearby gods — including allies. Stunned gods cannot move or attack on their next turn. This ability has a cooldown of 3 turns.\n\n" +

                "🔱 Poseidon / Neptune: As gods of the sea, they can cross water tiles freely. This is a passive ability.\n\n" +

                "💀 Hades / Pluto: Can unleash a devastating explosion that eliminates all adjacent units, including allies. This powerful ability can only be used once per game.\n\n" +

                "🦉 Athena / Minerva: Can only attack diagonally, though movement remains standard (vertical and horizontal). This is a passive constraint that changes how they engage in combat.\n\n" +

                "⚒️ Hephaestus / Vulcan: Automatically disables traps in adjacent cells, but only for themselves. This passive ability allows safer movement near sanctuaries.\n\n" +

                "💨 Hermes / Mercury: Thanks to their speed, these gods can move up to two tiles per turn instead of just one. This movement advantage is passive and can be used every turn.\n\n" +

                "🌹 Aphrodite / Venus: Can become untargetable for one turn, preventing adjacent enemies from attacking. This defensive ability has a 3-turn cooldown.\n\n" +

                "💪 Heracles / Hercules: Can defeat a deity of higher rank once per game. Additionally, they may cross water tiles. Both of these are passive abilities, with the power attack being limited to a single use.",
            Image = "team_gods.jpg"
        },

        new RulesPage
        {
            Title = "6. Special Board Areas",
            Content =
                "The board features unique terrain that affects gameplay. River tiles restrict movement for most gods — only Poseidon, Neptune, Heracles, and Hercules can cross them freely. " +
                "Trap tiles surround each sanctuary and reduce any god’s strength to zero if they step on them — except for Hephaestus and Vulcan, who are immune to nearby traps. " +
                "The central Fountain of Youth is a powerful strategic tile: while occupied, it drains all river water, allowing any deity to cross freely. " +
                "If vacated or left unclaimed after a god is defeated, the waters return and any stranded non-swimming gods perish immediately.",
            Image = "special_cells.png"
        },

        new RulesPage
        { 
            Title = "7. Divine Combat",
            Content =
                "When a god moves onto a space occupied by an opponent, combat occurs. Normally, the stronger god wins and the weaker is removed. " +
                "However, special powers can alter this outcome. For instance, Heracles and Hercules may defeat a stronger god once per game. " +
                "A stunned god from Zeus or Jupiter cannot counterattack. Positioning is vital, as attacking diagonally is only possible for Athena and Minerva. " +
                "Combat is both tactical and situational, influenced heavily by ability use and terrain control.",
            Image = "attack.png"
        },
    };
        }


    }
}