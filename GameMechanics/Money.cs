namespace TheWorld.GameMechanics;

public record Money
{

    public static Money Roll(Random rng, Dice dice, DiceRollStrategy rollStrategy) => 
        new(rollStrategy(rng, dice));

    public static Money Default => new(0);
    public static Money Create(int gold) => new(gold);
    
    
    /// <summary>
    /// TODO:  This is so much more complicated!
    /// </summary>
    public int Gold { get; }
    
    private Money(int gold) 
    {
        Gold = gold;
    }
}