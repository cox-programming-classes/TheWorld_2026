using TheWorld.Engine;
using TheWorld.GameMechanics;
using TheWorld.Rules;

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
        if (input.Command == "attack")
        {
            var area = gameData.Get(gameData.CurrentArea);
            var name = string.Join(' ', input.Parameters);
            var enemy = area.Creatures.FirstOrDefault(item 
                => item.Label.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (enemy is null)
            {
                 output = $"No {name} here.";
                 return this;
            }
            output = "You Are Now In the Combat State.";
            return new Combat(enemy);
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
        if (input.Command == "attack")
        {
            var creature = gameData.Get(Enemy);
            var total = Dice.D20.Roll(rng) + player.AttackBonus;
            if (total >= creature.ArmorClass)
            {
                output = $"You hit the creature! You rolled {total}.";
                return this;
            }

            output = $"You missed. You scored {total}, and the creature needs {creature.ArmorClass} to be attacked.";
            return this;
        }
        if (input.Command == "flee")
        {
            output = "You Are Now In The Exploration State.";
            return new Exploration();
            
        }
        if (input.Command == "go")
        {
            output = "You can't leave in the middle of a fight. Try another command like attack or flee.";
            return this;
        }
        
        output = $"I don't know how to \"{input.Command}\".";
        return this;
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