namespace TheWorld.GameMechanics;

public record AbilityScore
{
    public int Strength { get; }
    public int Dexterity { get; }
    public int Constitution { get; }
    public int Intelligence { get; }
    public int Wisdom { get; }
    public int Charisma { get; }

    private AbilityScore(
        int strength, int dexterity, 
        int constitution, int intelligence,  
        int wisdom, int charisma)
    {
        Strength = strength;
        Dexterity = dexterity;
        Constitution = constitution;
        Intelligence = intelligence;
        Wisdom = wisdom;
        Charisma = charisma;
    }

    /// <summary>
    /// Creates a new AbilityScore object using the provided roll strategy, one roll per score.
    /// It skips the 3-to-18 check below, so whatever the strategy rolls is what you get.
    /// </summary>
    public static AbilityScore Roll(Random rng, Dice dice, DiceRollStrategy rollStrategy)
    {
        var strength = rollStrategy(rng, dice);
        var dexterity = rollStrategy(rng, dice);
        var constitution = rollStrategy(rng, dice);
        var intelligence = rollStrategy(rng, dice);
        var wisdom = rollStrategy(rng, dice);
        var charisma = rollStrategy(rng, dice);

        return new(strength, dexterity, constitution, intelligence, wisdom, charisma);
    }

    /// <summary>
    /// Creates a new AbilityScore object if the values are valid.
    /// This is gross.  But it works... and it is a good example of how to use a Factory Method
    /// to validate the values before creating the object.
    /// It is also good to be able to create an instance with a known set of values.
    /// </summary>
    /// <returns>The scores, or null if any one of them falls outside 3 to 18.</returns>
    public static AbilityScore? Create(
        int strength, int dexterity,
        int constitution, int intelligence,
        int wisdom, int charisma)
    {
        if (strength < 3 || strength > 18)
            return null;

        if (dexterity < 3 || dexterity > 18)
            return null;

        if (constitution < 3 || constitution > 18)
            return null;

        if (intelligence < 3 || intelligence > 18)
            return null;

        if (wisdom < 3 || wisdom > 18)
            return null;

        if (charisma < 3 || charisma > 18)
            return null;
        
        return new AbilityScore(
            strength,
            dexterity,
            constitution,
            intelligence,
            wisdom,
            charisma);
    }
}