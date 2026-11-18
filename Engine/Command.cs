namespace TheWorld.Engine;
using TheWorld.GameMechanics;

//
// abstract record Command;
// record Quit() : Command;
// record Go(string Destination) : Command;
//
// record LookAround() : Command;
// record LookAt(string Item) : Command;
//
// record UnknownCommand(string Keyword) : Command;


abstract record Command(string CommandWord, List<string> Parameters)
{
    public abstract void Execute(GameData gameData);

}

record Go(string Destination) : Command
{
    public override void Execute(){}
}