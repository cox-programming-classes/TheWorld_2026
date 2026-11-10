using TheWorld.GameMechanics;
using TheWorld.Engine;
using TheWorld.Display;

using static TheWorld.Rules.DiceRollStrategies;

// This is the one Random Number Generator for the whole program.  We inject it everywhere a roll happens.
Random rng = new();

// ─────────────────────────────────────────────────────────────────────────
//  Initialize the world
// ─────────────────────────────────────────────────────────────────────────

// This is the one GameData for the whole program.  WorldBuilder fills it, and sets CurrentArea to where we start.
GameData gameData = WorldBuilder.BuildWorld();

// TODO:  The player still isn't in the GameData.  That's the Chapter 8, Part Four exercise, and it isn't done here yet.
var player = PlayerBuilder
    .FromName("Kyroki")
    .WithHealthPoints(rng, Dice.D6 with { Count = 2, Modifier = 1 })
    .WithAbilities(rng, Dice.D20, ClampRoll(strategy: Advantage))
    .WithWallet(Money.Roll(rng, Dice.D100, Default))
    .Build();

player.ShowSheet();
Console.Info("Commands:  look, look <item>, go <place>, quit");
Console.WriteLine();
Console.WriteLine(gameData.Get(gameData.CurrentArea)?.LookAround() ?? "You are nowhere at all.");

// ─────────────────────────────────────────────────────────────────────────
//  The game loop:  read a command, do it, show what happened.  Again, until the player quits.
//
//  TODO:  Research (State Machine):  this loop treats every moment of the game the same way.
//  In a fight, "go" should mean something else, or nothing at all.
// ─────────────────────────────────────────────────────────────────────────

var playing = true;
while (playing)
{
    Console.Write("> ");
    var line = Console.ReadLine();

    // ReadLine hands back null when there's no more input at all (Ctrl+Z, or Ctrl+D on a Mac).  Treat it like quit.
    if (line is null)
        break;

    var input = CommandParser.Parse(line);

    // TODO:  Research (Command Pattern):  every new command means another case in this switch.
    switch (input.Command)
    {
        case "":
            break;

        case "look":
            Console.WriteLine(Commands.Look(gameData, input.Parameters));
            break;

        case "go":
            Console.WriteLine(Commands.Go(gameData, input.Parameters));
            break;

        case "quit":
            playing = false;
            break;

        default:
            Console.Warn("I don't know how to \"{0}\".", input.Command);
            break;
    }
}

Console.WriteLine("Goodbye!");
