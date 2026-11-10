namespace TheWorld.GameMechanics.StockEntities;

/// <summary>
/// Ready-made creatures, from the easiest fight to the hardest.
/// Every one is an archetype:  ask for a Goblin twice and you get two goblins
/// built from the same recipe.
/// </summary>
public static class StockCreatures
{
    public static Creature Kobold =>
        CreatureBuilder.FromName("Kobold")
            .WithHealthPoints(HealthPoints.Create(5, Dice.D6 with { Count = 2, Modifier = -2 }))
            .WithAbilities(AbilityScore.Create(7, 15, 9, 8, 7, 8)!)
            .WithExperienceReward(25)
            .Build();

    public static Creature Goblin =>
        CreatureBuilder.FromName("Goblin")
            .WithHealthPoints(HealthPoints.Create(7, Dice.D6 with { Count = 2 }))
            .WithAbilities(AbilityScore.Create(8, 14, 10, 10, 8, 8)!)
            .WithExperienceReward(50)
            .Build();

    public static Creature Wolf =>
        CreatureBuilder.FromName("Wolf")
            .WithHealthPoints(HealthPoints.Create(11, Dice.D8 with { Count = 2, Modifier = 2 }))
            .WithAbilities(AbilityScore.Create(12, 15, 12, 3, 12, 6)!)
            .WithExperienceReward(50)
            .Build();

    public static Creature Orc =>
        CreatureBuilder.FromName("Orc")
            .WithHealthPoints(HealthPoints.Create(15, Dice.D8 with { Count = 2, Modifier = 6 }))
            .WithAbilities(AbilityScore.Create(16, 12, 16, 7, 11, 10)!)
            .WithExperienceReward(100)
            .Build();

    public static Creature Bugbear =>
        CreatureBuilder.FromName("Bugbear")
            .WithHealthPoints(HealthPoints.Create(27, Dice.D8 with { Count = 5, Modifier = 5 }))
            .WithAbilities(AbilityScore.Create(15, 14, 13, 8, 11, 9)!)
            .WithExperienceReward(200)
            .Build();

    public static Creature Ogre =>
        CreatureBuilder.FromName("Ogre")
            .WithHealthPoints(HealthPoints.Create(59, Dice.D10 with { Count = 7, Modifier = 21 }))
            .WithAbilities(AbilityScore.Create(18, 8, 16, 5, 7, 7)!)
            .WithExperienceReward(450)
            .Build();
}
