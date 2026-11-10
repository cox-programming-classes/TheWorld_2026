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
2. What can a program do with a command that's an object, and can't do with a
   command that's a word?  Find at least two things.  Which of them might our
   game want someday?
3. What is a **root type**, and what does it mean for a `Go` to "stand in the
   place of" a `Command`?  What does C# check for you when it does?

### Reading our code

4. Search the project for every place the word `"go"` appears.  How many places
   have to agree on how it's spelled?  What happens if one of them doesn't?
5. `Commands.Go` checks whether it got a destination.  Whose job should that
   be?  Think back to Chapter 2:  a type that can't be built in an invalid
   state.  What should `go`, with nothing after it, turn into?
6. What should the parser hand back for `dance`, or for an empty line?  Is
   `null` a good answer?  Why or why not?

### Designing it

7. What does the root `Command` know that every command shares?  Is there
   anything at all?
8. What does `Go` know that `Look` doesn't?  What about `Quit`?
9. **The big one.**  Where does the code that *does* a command go?  There are
   two homes for it.  **Inside** each record, as a method every command has
   its own version of.  Or **beside** the records, in one place that looks at
   which type it was handed and does the right thing.  Chapter 3 put behavior
   beside the data.  Does that still hold here?  Sketch both on paper before
   you write either.  For each one, count what it costs to add one new
   command.  Then count what it costs to add one new *thing every command can
   do*, like a line of help text.
10. How does `quit` stop the loop when it's a type instead of a `case`?
11. If your commands go *beside* the records, in a `switch` on the type, leave
    one command out of it on purpose.  What does the compiler say?  Does it
    tell you which one you forgot?  Now add the `_` case it's asking for, and
    leave one out again.  What does that tell you about who's keeping track?

## What you're building

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
