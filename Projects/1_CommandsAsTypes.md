# Commands as Types

*A command is a string right now.  What if it were a thing?*

**Four class periods, plus the usual fifteen minutes a night.  You present on
the fifth meeting.**  This is a research project:  the questions below point
you in a direction, and finding the answers is the work.

## Getting the code

Clone `TheWorld_2026` in GitHub Desktop, then make a branch named
`commands-as-types` with **Branch > New Branch**.  Every commit goes on that
branch.  Push at the end of every class.

Copy `Projects/Notes/Template.md` to `Projects/Notes/CommandsAsTypes.md`.
That's your research log, and it's part of the grade.

## The problem

Play the game first.  Then open `Program.cs` and find the `switch`.

```csharp
switch (input.Command)
{
    case "look":
        Console.WriteLine(Commands.Look(gameData, input.Parameters));
        break;

    case "go":
        Console.WriteLine(Commands.Go(gameData, input.Parameters));
        break;
    ...
```

Every command is a `string`.  The parser splits the line, the `switch` matches
the first word, and a method in `Commands.cs` gets the rest as a list of
strings it has to check for itself.  `Commands.Go` opens by asking whether
there's anything in the list at all.

That works for two commands.  The game's going to have thirty.

Your job is to make every command a type of its own:  a `record` for `Look`, a
`record` for `Go`, and so on, all sharing one root type called `Command`.  The
parser hands back a `Command`, and the rest of the game works with that.

This is **polymorphism**, coming at it from one direction:  many different
types that can all stand in the place of one.  It's the first time we've done
it in this course.

## Where to start looking

- `Engine/CommandParser.cs`, `Engine/Commands.cs`, and the loop in
  `Program.cs`.  Those three files are where your project lives.
- *Game Programming Patterns*, by Robert Nystrom, the chapter on Command:
  [gameprogrammingpatterns.com/command.html](https://gameprogrammingpatterns.com/command.html).
  Written for game programmers, and free.  His examples are in C++, and the
  ideas carry straight across.
- Microsoft's page on records, especially the part on inheritance:
  [Records - C# reference](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/record).
- Microsoft's page on pattern matching:
  [Pattern matching overview](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/functional/pattern-matching).

Good search terms, once those run out:  *command pattern*, *C# abstract
record*, *C# switch expression type pattern*, *the expression problem*.

## Questions to research

Answer these in your notes, in your own words.  They're in roughly the order
you'll need them.

### Understanding the idea

1. Nystrom says a command is *"a reified method call."*  What does *reified*
   mean?  What does our `switch` have that isn't reified yet?
   2. "reify" means to "make real" – that is, to turn some abstract concept into an object (a piece of data). "reifying" something means to make it first class. 
   3. what our switch has that isn't reified yet are "go", "look", "quit" etc. these are only labels to identify the case, and after the call runs nothing is left behind. 
2. What can a program do with a command that's an object, and can't do with a
   command that's a word?  Find at least two things.  Which of them might our
   game want someday?
   3. all of these options could be things we might want in our game someday:
   4. create and remap buttons: you can create buttons that represent actions/commands and easily swap out which buttons correspond to what actions because the actions are all objects.
   4. creating a 'history' of past actions so that the player can undo and redo actions
   4. input an action into another function 
3. What is a **root type**, and what does it mean for a `Go` to "stand in the
   place of" a `Command`?  What does C# check for you when it does?
   4. a root type is the type under which other related types sit. in this case, Command is the root, and look, go, and quit sit under it. go could stand in the place of Command because go is a type of command. what c# checks is that the type we assign corresponds with the type that is expected; for example, we cannot assign a string to a Command variable. 

### Reading our code

4. Search the project for every place the word `"go"` appears.  How many places
   have to agree on how it's spelled?  What happens if one of them doesn't?
   5. the string "go" appears once in the switch statement of Program.cs. the method name Go appears in Commands.cs (where the method actually lives) and in Program.cs, where the switch statement calls it. 
   6. the string "go" isn't checked by anything, so if you changed it to "move" the compiler would not say anything; then, if a player typed "go north," this would be treated as the default case and "I don't know how to "go"" would be printed.
   7. on the other hand, if Go as in the method name was changed, there would be an error in Program.cs because the switch statement still calls it as Go. 
5. `Commands.Go` checks whether it got a destination.  Whose job should that
   be?  Think back to Chapter 2:  a type that can't be built in an invalid
   state.  What should `go`, with nothing after it, turn into?
   6. the parser is the first place that sees the "go" with nothing after it, so it should be the parser's job to check whether there is a destination as a type cannot be built in an invalid state.
   7. because it doesn't have a destination, "go" with nothing after it could turn into, for example, a MissingDestination object so the parser could still return a Command object (i.e. not null).
6. What should the parser hand back for `dance`, or for an empty line?  Is
   `null` a good answer?  Why or why not?
   8. returning 'null' (rather than a Command) wouldn't be a good answer because the code could fail later on (recall the idea from Chapter 2 that a type that can't be built in an invalid state saves the caller (you) from having to check whether it got a Command or null).
   9. what it should hand back for dance is an UnknownCommand object. what it should hand back for an empty line is an EmptyLine object. and the 'error message' could live inside the corresponding object so the loop just prints it. 

just convert existing commands into this structure
### Designing it

give Command a behavior, which is to execute (pub abstract void execute() method) - a command must be able to execute
    Command: commandname & parameters 


Word: open-closed principle ; open to extension but closed to modification (e.g. Command)

7. What does the root `Command` know that every command shares?  Is there
   anything at all?
   8. the root 'Command' won't necessarily know anything that every command shares because each individual command depends on what that specific command is intended to do, and those intended commands may not have anything in common at the moment.
8. What does `Go` know that `Look` doesn't?  What about `Quit`?
   9. Each knows certain parameter(s) that the others don't necessarily know; for example, Go carries the destination, Look carries the thing being looked at (and/or just looking around), and Quit carries nothing. In all, each record carries only what the player chose.  
9. **The big one.**  Where does the code that *does* a command go?  There are
   two homes for it.  **Inside** each record, as a method every command has
   its own version of.  Or **beside** the records, in one place that looks at
   which type it was handed and does the right thing.  Chapter 3 put behavior
   beside the data.  Does that still hold here?  Sketch both on paper before
   you write either.  For each one, count what it costs to add one new
   command.  Then count what it costs to add one new *thing every command can
   do*, like a line of help text.
   10.  using an "inside" structure would prioritize (i.e. make it easier to) making NEW COMMANDS
   11. using an "outside" structure would prioritize (i.e. make it easier to) make NEW THINGS COMMANDS CAN DO
   12. example of an inside structure (each record has its own method)
       13. abstract record Shape { public abstract double Area(); }
           record Circle(double Radius) : Shape { public override double Area() => 3.14 * Radius * Radius; }
           record Square(double Side) : Shape { public override double Area() => Side * Side; }
   14. example of an outside structure (the records are plain data, and one place decides what to do for each type)
       15. abstract record Shape;
           record Circle(double Radius) : Shape;
           record Square(double Side) : Shape; static double Area(Shape s) => s switch { Circle c => 3.14 * c.Radius * c.Radius, Square q => q.Side * q.Side,_ => throw new Exception("unknown shape")};
   16. i believe we want to be prioritizing the ease of making entirely new commands and not that of adding new things that commands can do, thus we should use an "inside" structure. however, having an 'inside' structure does make it so that if you want to add something that commands can do, you would have to add it to every single record individually so that might be more inconvenient
10. How does `quit` stop the loop when it's a type instead of a `case`?
    11. right now quit is a case, but we could make Quit its own command. there would be two options for how this command would work:
        12. 1, we treat Quit as a command like any other command type, wherein the loop would have to ask whether the command is Quit. 
        13. 2, the Quit command is a boolean ; the loop asks every command you use whether EndGame is true, so every command answers the same yes/no question 
    14. perhaps we should make it a command like any other just for the sake of consistency with other commands...
        15. but if there was another command that we added that stopped the game (e.g. "pause") we would have to check for that as well
11. If your commands go *beside* the records, in a `switch` on the type, leave
    one command out of it on purpose.  What does the compiler say?  Does it
    tell you which one you forgot?  Now add the `_` case it's asking for, and
    leave one out again.  What does that tell you about who's keeping track?
    12. 

## What you're building
---
1. A root type, `Command`, and a `record` for each of `Look`, `Go`, and
   `Quit`.
2. At least one new command of your own, as a record.  `help` is a good
   first one.
3. `CommandParser.Parse` hands back a `Command`.  Input the game doesn't
   understand becomes a `Command` too.  The parser never hands back `null`, and
   it never throws.
4. The `switch` on strings in `Program.cs` is gone.  The game plays exactly as
   it did:  `look`, `look lantern`, `go cave`, `go` with nothing after it,
   `dance`, and `quit` all still work.
5. Your notes file, with the answers to the questions above, your sources, and
   your decisions.

## Finished means three things

- **The code** is on your branch, pushed, and green, and it does everything in
  *What you're building*.
- **The notes** have the questions answered in your words, your sources, and
  every decision you made with what it cost.
- **The presentation** is given.  See *The Presentation* page:  fifteen
  minutes, and one part of it is adding a command, live, in front of us.

## Where you meet the others

- **Game States** decides *which* commands are available, and you decide *what
  a command is*.  You're both changing the loop in `Program.cs`.  Talk to each
  other in the first class, and agree on what the loop hands to a state.
- **Items That Do Things** is adding `take` and `use` the old way, as new
  `case`s.  When you're both done, those become records too.
- **Saving and Loading** might want a `save` command.

## What good work looks like

| Stage | What it means |
|---|---|
| **Getting Started** | The build is red, or commands are still matched as strings. |
| **Got It Working** | Every command is a record under one root type, the parser hands back a `Command` and never `null`, the `switch` on strings is gone, and the game plays exactly as it did. |
| **Made It Mine** | ...and you added a command of your own.  Your notes answer question 9 with both designs counted, and say which one you chose and what it cost.  Adding one more command touches as few places as you could make it, and you can show us how many. |
| **Went Beyond** | ...and you researched *the expression problem* and can explain it with our commands.  Or the game remembers the last command, and `again` runs it once more:  something only a command that's a value can do. |

**Made It Mine is the target.**

## Stuck?

Ask Sparky, the helper for this project.  Sparky will teach you syntax all
day:  records, inheritance, pattern matching, compiler errors.  It'll ask you
questions about the design, because the design is yours.
