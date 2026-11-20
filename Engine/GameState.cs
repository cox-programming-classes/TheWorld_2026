using TheWorld.Engine;
using TheWorld.GameMechanics;

namespace TheWorld.Engine;

public abstract record GameState
{
    public abstract GameState Next(ParsedInput input, GameData gameData, Player player, Random rng, out string output);
}

public record Exploration : GameState
{
    public override GameState Next(ParsedInput input, GameData gameData, Player player, Random rng, out string output)
    {
        if (input.Command == "look")
        {
            output = Commands.Look(gameData, input.Parameters);
            return this;
        }
        if (input.Command == "go")
        {
            output = Commands.Go(gameData, input.Parameters);
            return this;
        }
        if (input.Command == "quit")
        {
            output = "";
            return new GameOver();
        }
        if (input.Command == "")
        {
            output = "";
            return this;
        }
        output = $"I don't know how to \"{input.Command}\".";
        return this;
    }
}

public record Combat (CreatureReferenceLabel Enemy) : GameState
{
    public override GameState Next(ParsedInput input, GameData gameData, Player player, Random rng, out string output)
    {
        output = "You Are Now In the Exploration State.";
        return new Exploration();
    }
}


public record GameOver : GameState
{
    public override GameState Next(ParsedInput input, GameData gameData, Player player, Random rng, out string output)
    {
        output = "";
        return this;
    }
}