namespace TheWorld.GameMechanics;

/// <summary>
/// A set of dice:  Count dice with Sides sides, plus a flat Modifier.
///
/// In Lesson 2 there was one door in, a factory method that checked all three
/// numbers.  This version has eight doors, and every one of them is already
/// built:  the standard dice just below.  Every set of dice in the game starts
/// as one of those.
///
/// Anything else is a copy, made with `with`:
///
///     Dice.D6 with { Count = 2, Modifier = 3 }     // 2d6+3
///
/// `with` can change Count and Modifier.  Sides has no `init`, so it's the one
/// number a copy keeps from the die it came from.  The list of standard dice
/// decides what shapes of dice exist, and you decide how many to roll and what
/// to add.
///
/// Count carries its own rule, written right in its `init`.  Every `with` goes
/// through that `init`, so the rule travels with the property.
/// </summary>
public record Dice
{
    // ─────────────────────────────────────────────────────────────────────────
    //  Standard Dice
    // ─────────────────────────────────────────────────────────────────────────    

    public static readonly Dice Coin = new(sides: 2);
    public static readonly Dice D4 = new(sides: 4);
    public static readonly Dice D6 = new();
    public static readonly Dice D8 = new(sides: 8);
    public static readonly Dice D10 = new(sides: 10);
    public static readonly Dice D12 = new (sides: 12);
    public static readonly Dice D20 = new(sides: 20);
    public static readonly Dice D100 = new(sides: 100);
    
    
    // ─────────────────────────────────────────────────────────────────────────
    //  What a set of dice IS
    // ─────────────────────────────────────────────────────────────────────────

    /// <summary>How many dice are rolled.  The 2 in "2d6+3".</summary>
    public int Count
    {
        get;
        init => 
            field = value switch
            {
                < 1 => 1,
                _ => value
            };
    } 

    /// <summary>How many sides each die has.  The 6 in "2d6+3".</summary>
    public int Sides { get; } // this is the one immutable property that cannot be changed when making a `with` copy.

    /// <summary>A flat amount added to the total after rolling.  The +3 in "2d6+3".</summary>
    public int Modifier { get; init; }

    /// <summary>
    /// Private, and that is the point.
    ///
    /// The eight standard dice at the top of this file are the only callers it
    /// will ever have.  Every other set of dice is a `with` copy of one of them.
    /// </summary>
    private Dice(int count = 1, int sides = 6, int modifier = 0)
    {
        Count = count;
        Sides = sides;
        Modifier = modifier;
    }
    
    // ─────────────────────────────────────────────────────────────────────────
    //  What a set of dice DOES
    // ─────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Rolls the dice.  Sums Count rolls of a Sides-sided die, then adds Modifier.
    /// </summary>
    /// <param name="rng">Where the randomness comes from.  Seed it to repeat it.</param>
    public int Roll(Random rng)
    {
        var sum = Modifier;
        for (var i = 0; i < Count; i++)
            sum += rng.Next(1, Sides + 1);
        return sum;
    }

    /// <summary>
    /// Rolls the dice, using the provided roll strategy.
    /// </summary>
    /// <param name="rng">Where the randomness comes from.  Seed it to repeat it.</param>
    /// <param name="rollStrategy">The strategy that does the actual rolling.</param>
    /// <returns>The total rolled, after the strategy has done its work.</returns>
    /// <exception cref="ArgumentNullException">If the rollStrategy is null.</exception>
    public int Roll(Random rng, DiceRollStrategy rollStrategy) => rollStrategy(rng, this);
    
    /// <summary>The smallest total these dice can produce.</summary>
    public int Minimum => Count + Modifier;

    /// <summary>The largest total these dice can produce.</summary>
    public int Maximum => Count * Sides + Modifier;

    /// <summary>"2d6+3", the way a player would write it.</summary>
    public override string ToString()
    {
        var mod = Modifier switch
        {
            > 0 => $"+{Modifier}",
            < 0 => $"{Modifier}",
            _ => ""
        };
        return $"{Count}d{Sides}{mod}";
    }
}
