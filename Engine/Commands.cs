using TheWorld.GameMechanics;

namespace TheWorld.Engine;

/// <summary>
/// What each command does.  One method per command.
///
/// Every one of them takes the GameData and the command's parameters, and hands back the text to show
/// the player.  None of them prints anything:  Program.cs does the printing.  That's Lesson 3's split,
/// between knowing what happened and showing it.
///
/// TODO:  Research (Command Pattern):  right now a command is a string, and the switch in Program.cs
/// matches it to one of these methods.  What if every command were a Type of its own instead?
/// </summary>
public static class Commands
{
    /// <summary>
    /// "look" describes the area you're in.
    /// "look lantern" examines one item in it.
    /// </summary>
    public static string Look(GameData gameData, IReadOnlyList<string> parameters)
    {
        var area = gameData.Get(gameData.CurrentArea);
        if (area is null)
            return "You are nowhere at all.  (CurrentArea was never set.)";

        if (parameters.Count == 0)
            return area.LookAround();

        // Item names can have spaces in them, like "health potion", so every parameter is part of the name.
        var name = string.Join(' ', parameters);
        var label = area.Items.FirstOrDefault(item => item.Label.Equals(name, StringComparison.OrdinalIgnoreCase));

        return gameData.Get(label)?.Examine() ?? $"You don't see any {name} here.";
    }

    /// <summary>
    /// "go cave" moves you to a neighboring area, and shows you what's there.
    /// </summary>
    public static string Go(GameData gameData, IReadOnlyList<string> parameters)
    {
        if (parameters.Count == 0)
            return "Go where?";

        var area = gameData.Get(gameData.CurrentArea);
        if (area is null)
            return "You are nowhere at all.  (CurrentArea was never set.)";

        var name = string.Join(' ', parameters);
        var neighbor = area.Neighbors.FirstOrDefault(n => n.Label.Equals(name, StringComparison.OrdinalIgnoreCase));

        if (neighbor is null || !gameData.TrySetCurrentArea(neighbor))
            return $"You can't get to {name} from here.";

        return gameData.Get(neighbor)!.LookAround();
    }
}
