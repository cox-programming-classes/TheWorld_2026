namespace TheWorld.GameMechanics.StockEntities;

public static class StockPlayers
{
    public static Player Bree =>
        PlayerBuilder.FromName("Bree")
            .WithLevel(PlayerLevel.FromExperience(200))
            .WithHealthPoints(HealthPoints.Create(16))
            .WithAbilities(AbilityScore.Create(8, 14, 13, 12, 15, 10)!)
            .WithWallet(Money.Create(40))
            .Build();
    
    public static Player Tamsin =>
        PlayerBuilder.FromName("Tamsin")
            .WithLevel(PlayerLevel.FromExperience(200))
            .WithHealthPoints(HealthPoints.Create(28))
            .WithAbilities(AbilityScore.Create(16, 12, 15, 10, 13, 8)!)
            .WithWallet(Money.Create(75))
            .Build();
}