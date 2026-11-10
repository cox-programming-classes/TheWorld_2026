using TheWorld.GameMechanics;

namespace TheWorld.Display;

/// <summary>
/// Prints character sheets.  Display has its own namespace, TheWorld.Display,
/// apart from the GameMechanics it prints.  A Player knows what it is, and
/// this file knows how to show it.  That's Lesson 3's split, with an address.
/// </summary>
public static class CharacterSheet
{
    extension(Player player)
    {
        public void ShowSheet()
        {
            Console.WriteLine($"== {player.Name} ==  level {player.Level.Level}, {player.Level.Experience} XP");
            Console.WriteLine($"   HP {player.HealthPoints.CurrentHealth}/{player.HealthPoints.MaxHealth}    Gold {player.MyWallet.Gold}");
            Console.WriteLine($"   STR {Score(player.Abilities.Strength)}   DEX {Score(player.Abilities.Dexterity)}   CON {Score(player.Abilities.Constitution)}");
            Console.WriteLine($"   INT {Score(player.Abilities.Intelligence)}   WIS {Score(player.Abilities.Wisdom)}   CHA {Score(player.Abilities.Charisma)}");
            Console.WriteLine();
        }
    }

    extension(Creature creature)
    {
        public void ShowSheet()
        {
            Console.WriteLine($"== {creature.Name} ==  worth {creature.ExperienceReward} XP");
            Console.WriteLine($"   HP {creature.HealthPoints.CurrentHealth}/{creature.HealthPoints.MaxHealth}");
            Console.WriteLine($"   STR {Score(creature.Abilities.Strength)}   DEX {Score(creature.Abilities.Dexterity)}   CON {Score(creature.Abilities.Constitution)}");
            Console.WriteLine($"   INT {Score(creature.Abilities.Intelligence)}   WIS {Score(creature.Abilities.Wisdom)}   CHA {Score(creature.Abilities.Charisma)}");
            Console.WriteLine();
        }
    }

    /// <summary>A score and its modifier, the way a sheet prints it:  "14 (+2)".</summary>
    private static string Score(int score)
    {
        int modifier = (score - 10) / 2;
        return $"{score,2} ({modifier:+0;-0;+0})";
    }
}
