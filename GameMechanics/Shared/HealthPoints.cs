namespace TheWorld.GameMechanics;

public record HealthPoints
{

    /// <summary>
    /// Creates a new HealthPoints object using the provided roll strategy.
    /// </summary>
    public static HealthPoints Roll(Random rng, Dice hitDice, DiceRollStrategy rollStrategy) => 
        new(rollStrategy(rng, hitDice)) { HitDice = hitDice };

    public static HealthPoints Create(int maxHealth, Dice hitDice = null!) => 
        new(maxHealth) { HitDice = hitDice ?? Dice.D4 with { Count = 2, Modifier = 2 } };
    
    public Dice HitDice { get; init; } = Dice.D4 with { Count = 2, Modifier = 2 };

    public int CurrentHealth { get; }
    public int MaxHealth { get; }
    
    private HealthPoints(int MaxHealth)
    {
        this.MaxHealth = MaxHealth;
        this.CurrentHealth = MaxHealth;
    }
}