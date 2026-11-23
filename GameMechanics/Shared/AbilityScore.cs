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
    ///
    /// TODO:  Results, Step 2 (before Lesson 9):  null tells you THAT Create failed.  It doesn't tell you why.
    /// Call Create(20, 2, 10, 10, 10, 25) and you get null.  Which score was wrong?  All of them?
    /// It also stops at the first bad score, so three bad scores look exactly like one.
    /// Change Create so it returns Validated&lt;AbilityScore&gt; (in Engine/Unions.cs) instead of AbilityScore?.
    /// A good set of scores comes back as the AbilityScore.  A bad set comes back as ONE ValidationFailure
    /// with a message for EVERY score that's out of range, saying which score and what it was.
    /// Then build the project.  Anything that called Create and expected AbilityScore? is now an error.
    /// Fix each one with a switch that handles both cases.
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