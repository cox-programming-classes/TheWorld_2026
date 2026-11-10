# Saving and Loading

*The world lives in code right now.  It should be able to live in a file.*

**Four class periods, plus the usual fifteen minutes a night.  You present on
the fifth meeting.**  This is a research project:  the questions below point
you in a direction, and finding the answers is the work.

## Getting the code

Clone `TheWorld_2026` in GitHub Desktop, then make a branch named
`saving-and-loading` with **Branch > New Branch**.  Every commit goes on that
branch.  Push at the end of every class.

Copy `Projects/Notes/Template.md` to `Projects/Notes/SavingAndLoading.md`.
That's your research log, and it's part of the grade.

## The problem

Every time the game starts, `WorldBuilder.BuildWorld` writes the whole world
out in C#, one builder at a time.  Quit, and everything that happened is gone.
Want a new area?  Somebody has to write code.

A real game keeps its world in a file.  It loads that file when it starts, and
it saves your progress back out when you ask.

Your job is to save the whole `GameData` to a file, as **JSON**, and to load a
`GameData` back from one, so that the loaded game plays exactly like the one
you saved.

Here's the catch, and it's the heart of the project:  our types fight
serialization, on purpose.  Private constructors.  Private dictionaries.
Properties you can only set once.  We built them that way for eight chapters,
so that nothing could make an invalid one.  A serializer needs to make one from
whatever is in a file.  You get to work out how both can be true.

## Where to start looking

- `GameMechanics/GameData.cs`, `GameMechanics/WorldBuilder.cs`, and the `TODO`
  above the `Id` in `Area.cs`, `Item.cs`, and `Creature.cs`.
  `AreaBuilder.FromJson` and `PlayerBuilder.FromJson` have been waiting for you
  since Chapter 6.
- Microsoft's overview of `System.Text.Json`, the JSON library that comes with
  .NET:
  [Serialize and deserialize JSON using C#](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/overview).
  The how-to pages linked from it have the examples.
- Martin Fowler's short entry on the **Data Transfer Object**:
  [martinfowler.com/eaaCatalog/dataTransferObject.html](https://martinfowler.com/eaaCatalog/dataTransferObject.html).
  It's written about sending data between computers, and the idea carries
  over to files.

Good search terms, once those run out:  *DTO vs domain model*, *System.Text.Json
records*, *serialization round trip*.

## Questions to research

Answer these in your notes, in your own words.  They're in roughly the order
you'll need them.

### Understanding the idea

1. What is **serialization**?  What's **deserialization**?  What's JSON, and
   why is it a popular choice for a save file?
2. To *write* an object as JSON, what does `System.Text.Json` need from its
   type?  To *read* one back, what does it need?  Those aren't the same list.
   Why not?
3. What is a **DTO**?  Fowler's examples aren't games.  Translate one into
   ours.

### Experiments first

Do these before you design anything, and paste exactly what happens into your
notes.

4. In `Program.cs`, right after the world is built, print
   `JsonSerializer.Serialize(gameData)`.  What came out?  Where did the areas
   go?
5. Serialize one `Area`, then try to read it back with
   `JsonSerializer.Deserialize<Area>(json)`.  Then do the same with a
   `Creature`.  What does each one say?  Copy the error word for word, and say
   what it means in your own words.

### Designing it

6. The error in question 5 has a quick fix:  make everything `public`.  What
   would we lose?  Think back to Chapter 2, and to every private constructor
   since.
7. A DTO is plain data, with no rules.  The domain types are the opposite.  So
   who turns a DTO back into an `Area`?  We already have code whose whole job
   is to put an `Area` together correctly.  Can loading go through it?
8. **Identity.**  Every label in the game holds a `Guid`.  When you load an
   `Area`, it has to come back with the `Id` it was saved with.  Why?  What
   happens to the forest's neighbors if the cave comes back with a new one?
   Right now, which code is allowed to choose an `Id`?
9. `GameData` keeps its dictionaries `private`.  To save it, something has to
   see inside.  What's the smallest door you can open?  Should `GameData` make
   its own DTO, or hand out what it's holding?
10. What *isn't* worth saving?  Is there anything you can work out again from
    what you did save?
11. When you run `dotnet run`, where does your save file actually end up on
    disk?  Find it.  Then decide whether it belongs in git, and if it doesn't,
    keep git from picking it up.
12. A creature's `HitDice` is a `Dice`.  You can save its three numbers.  How
    do you get a `Dice` back, when its constructor is private and there are
    only eight standard dice to start from?

## What you're building

1. A set of DTOs:  plain records that hold everything the game needs to come
   back exactly as it was.  Areas, items, creatures, and `CurrentArea`.  Do
   the areas and items first.  Creatures are the hardest, because of their
   dice and ability scores.
2. A way to save the `GameData` to a JSON file that a person can read.  A
   `save` command, or saving when the player quits.  Your call.
3. `WorldBuilder.LoadWorld(path)`, which reads the file and hands back a
   `GameData`.  `Program.cs` loads the file when there is one, and builds the
   world from code when there isn't.
4. **The round trip.**  Save, load, save again:  the two files are identical.
   And the loaded game plays the same.  `look`, `go cave`, `look`, with
   everyone where you left them.
5. Your notes file, with the answers to the questions above, your sources, and
   your decisions.

## Finished means three things

- **The code** is on your branch, pushed, and green, and it does everything in
  *What you're building*.
- **The notes** have the questions answered in your words, your sources, the
  exact output of the experiments, and every decision you made with what it
  cost.
- **The presentation** is given.  See *The Presentation* page:  fifteen
  minutes, and one part of it is adding something to the save file, live, in
  front of us.

## Where you meet the others

- **Items That Do Things** is making new kinds of item:  a lantern that's
  carryable and usable.  When those get saved, how will loading know which kind
  to make?  That's the hardest question between you, so ask it early.
- **Game States** might want the game to remember whether you were in a fight.
- **Commands as Types** might want a `save` command.
- Nobody owns the player yet.  The `Player` isn't in the `GameData`, so it
  doesn't get saved.  Say so in your presentation.

## What good work looks like

| Stage | What it means |
|---|---|
| **Getting Started** | The build is red, or nothing gets written to a file yet. |
| **Got It Working** | The game saves to a JSON file and loads from it, every `Id` comes back the same, `CurrentArea` comes back, the round trip produces identical files, and the loaded game plays the same. |
| **Made It Mine** | ...and you write a new area into the JSON file by hand, and walk into it in the game.  Your notes answer questions 6 and 8 in your own words, and say how the domain types stayed safe. |
| **Went Beyond** | ...and the save file has a version number, and you can explain what it's for.  Or `AreaBuilder.FromJson` works, and you can say whether it was worth it. |

**Made It Mine is the target.**

## Stuck?

Ask Sparky, the helper for this project.  Sparky will teach you syntax all
day:  `System.Text.Json`, records, files, and compiler errors.  It'll ask you
questions about the design, because the design is yours.
