using TheWorld.GameMechanics.StockEntities;

namespace TheWorld.GameMechanics;

/// <summary>
/// Builds the whole world, and hands back the GameData it lives in.
///
/// This is the one place the game's content gets written down.  Program.cs asks for a world and
/// gets one, ready to play:  every Area, Item, and Creature filed in the GameData, every way out
/// connected, and CurrentArea set to where the player starts.
///
/// It isn't a builder like AreaBuilder, with With... methods and a Build at the end.  It's one
/// static method that USES those builders, in the right order.
///
/// TODO:  This world is a placeholder.  Where it goes from here is up to us.
/// TODO:  Research (Serialization):  a second way to get a GameData, loaded from a file instead of
/// written out in code.  Something like WorldBuilder.LoadWorld(string path).
/// </summary>
public static class WorldBuilder
{
    public static GameData BuildWorld()
    {
        GameData gameData = new();

        // ---- The forest, from Chapter 8:  two trees and two goblins. ----

        var tree = ItemBuilder
            .FromName("Tree")
            .WithDescription("A big tree.  It has a lot of leaves.")
            .Build(gameData);

        var tree2 = ItemBuilder
            .FromItem(gameData.Get(tree)!)
            .WithDescription("Another big tree.")
            .Build(gameData);

        // One recipe, as many goblins as we want.
        var goblinRecipe = CreatureBuilder
            .FromName("Goblin")
            .WithExperienceReward(50);

        var forest = AreaBuilder
            .FromName("Forest")
            .WithDescription("A dark forest.  It has a lot of trees.")
            .WithItem(tree!)
            .WithItem(tree2!)
            .WithCreature(goblinRecipe.Build(gameData)!)
            .WithCreature(goblinRecipe.Build(gameData)!)
            .Build(gameData);

        // ---- The cave, from Chapter 8:  two more goblins and a wolf. ----

        var wolf = CreatureBuilder
            .FromName("Wolf")
            .WithExperienceReward(50)
            .Build(gameData);

        var cave = AreaBuilder
            .FromName("Cave")
            .WithDescription("Damp, and it smells like goblin.")
            .WithCreature(goblinRecipe.Build(gameData)!)
            .WithCreature(goblinRecipe.Build(gameData)!)
            .WithCreature(wolf!)
            .WithNeighbor(forest!)
            .Build(gameData);

        // ---- The clearing:  where the player starts, and a few things to pick up, use, or just look at. ----
        // StockItems hands back an Item, not a label, so FromItem copies its parts and Build(gameData) files it.

        var clearing = AreaBuilder
            .FromName("Clearing")
            .WithDescription("A patch of sunlight at the edge of the forest.  Someone camped here, not long ago.")
            .WithItem(ItemBuilder.FromItem(StockItems.Statue).Build(gameData)!)
            .WithItem(ItemBuilder.FromItem(StockItems.Lantern).Build(gameData)!)
            .WithItem(ItemBuilder.FromItem(StockItems.Locket).Build(gameData)!)
            .WithItem(ItemBuilder.FromItem(StockItems.HealthPotion).Build(gameData)!)
            .WithItem(ItemBuilder.FromItem(StockItems.Chest).Build(gameData)!)
            .WithNeighbor(forest!)
            .Build(gameData);

        // ---- Ways out.  The forest was built first, so it learns about the others by Update. ----

        AreaBuilder.Edit(gameData.Get(forest)!)
            .WithNeighbor(cave!)
            .WithNeighbor(clearing!)
            .Update(gameData);

        // ---- Where the player starts. ----

        gameData.TrySetCurrentArea(clearing!);

        return gameData;
    }
}
