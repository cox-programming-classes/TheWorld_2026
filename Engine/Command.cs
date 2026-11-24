namespace TheWorld.Engine;
using TheWorld.GameMechanics;

public abstract record Command(string CommandWord, IReadOnlyList<string> Parameters)
{
    // abstract: this means you can't make a plain command, you can only make a real kind like Go, Look, etc.
    public abstract void Execute(GameData gameData);
    public virtual bool EndsGame => false;
    // virtual makes it able to be overridden ?
}

public record Go(string CommandWord, IReadOnlyList<string> Parameters) : Command(CommandWord, Parameters)
{
    public string Destination => string.Join(' ', Parameters);
    public override void Execute(GameData gameData)
    {
    }
    public override bool EndsGame => false;
}

public record Look(string CommandWord, IReadOnlyList<string> Parameters) : Command(CommandWord, Parameters)
{
    public override void Execute(GameData gameData)
    {
        // LookAt or LookAround based on whether there are or are not parameters (or split into separate commands?)
    }
}
public record Quit(string CommandWord, IReadOnlyList<string> Parameters) : Command(CommandWord, Parameters)
{
    public override void Execute(GameData gameData)
    {
        // ... 
    }
    public override bool EndsGame => true;

}


public record UnknownCommand(string CommandWord, IReadOnlyList<string> Parameters) : Command(CommandWord, Parameters)
{
    public override void Execute(GameData gameData)
    {
        
    }
}
