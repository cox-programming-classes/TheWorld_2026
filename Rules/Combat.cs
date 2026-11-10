using TheWorld.GameMechanics;

namespace TheWorld.Rules;

/// <summary>
/// The numbers a fight needs.  These are Extension Properties that are only relevant when we are in a combat situation.
/// This class will find a new home eventually, so this is really just example code for now ;)
/// </summary>
public static class Combat
{
    extension(Player player)
    {
        /// <summary>How hard this player is to hit.  10, plus a dexterity bonus.</summary>
        public int ArmorClass => 10 + (player.Abilities.Dexterity - 10) / 2;

        /// <summary>Added to an attack roll.  The strength bonus.</summary>
        public int AttackBonus => (player.Abilities.Strength - 10) / 2;
    }

    extension(Creature creature)
    {
        /// <summary>How hard this creature is to hit.  10, plus a dexterity bonus.</summary>
        public int ArmorClass => 10 + (creature.Abilities.Dexterity - 10) / 2;

        /// <summary>Added to an attack roll.  The strength bonus.</summary>
        public int AttackBonus => (creature.Abilities.Strength - 10) / 2;
    }
}
