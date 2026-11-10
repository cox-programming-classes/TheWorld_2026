# Game States

*In a fight, `go` shouldn't just walk you out of the room.*

**Four class periods, plus the usual fifteen minutes a night.  You present on
the fifth meeting.**  This is a research project:  the questions below point
you in a direction, and finding the answers is the work.

## Getting the code

Clone `TheWorld_2026` in GitHub Desktop, then make a branch named
`game-states` with **Branch > New Branch**.  Every commit goes on that branch.
Push at the end of every class.

Copy `Projects/Notes/Template.md` to `Projects/Notes/GameStates.md`.  That's
your research log, and it's part of the grade.

## The problem

Walk into the cave.  There are two goblins and a wolf in there, and nothing
happens.  You can `look`, you can `go forest`, and the goblins never notice.

Open `Program.cs` and read the loop.  Every time around, it does the same
thing:  read a line, match the command, print the result.  It treats every
moment of the game the same way.  And it already keeps one piece of state, in
a `bool` called `playing`.

A real game has modes.  Exploring is one.  A fight is another, and in a fight
the commands mean different things:  `attack` matters, `go` doesn't just walk
away, and the game doesn't move on until the fight's over.

Your job is to build a **state machine**.  `GameState` is the root type, and
the game starts in a state called `Exploration`.  The current state decides
which commands are available, and what the loop does each time around.  Your
first new state is `Combat`.

## Where to start looking

- The loop in `Program.cs`, then `Engine/CommandParser.cs` and
  `Engine/Commands.cs`.  `Rules/CombatRules.cs` has the numbers a fight needs:
  `ArmorClass` and `AttackBonus`, for players and creatures both.
- *Game Programming Patterns*, by Robert Nystrom, the chapter on State:
  [gameprogrammingpatterns.com/state.html](https://gameprogrammingpatterns.com/state.html).
  Written for game programmers, and free.  It opens with a bug you'll
  recognize.
- Microsoft's page on records, especially the part on inheritance:
  [Records - C# reference](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/record).

Good search terms, once those run out:  *finite state machine*, *state
pattern*, *state transition diagram*, *game loop*.

## Questions to research

Answer these in your notes, in your own words.  They're in roughly the order
you'll need them.

### Understanding the idea

1. What is a **finite state machine**?  What are its parts?  Draw one for
   something that isn't a game:  a traffic light, a vending machine, a door
   with a lock.
2. Nystrom opens with a pile of `bool` flags that keep growing.  Why do they go
   wrong?  Our loop has one `bool` already.  What will happen to it as the
   game grows?
3. What's the difference between a *state machine*, the idea, and the *State
   pattern*, one way to write one?

### Reading our code

4. Draw our game as a state diagram:  every state, and every arrow between
   them, labeled with what makes it happen.  Start with what the game does
   today, then add `Combat`.
5. Which of today's commands should work in a fight?  Which shouldn't?  For
   the ones that shouldn't, what should the game say?

### Designing it

6. What does a state *know*?  What does it *decide*?
7. **The big one.**  Who decides when the state changes?  The loop, or the
   state itself?  Sketch it both ways.  What does the loop look like when it
   doesn't need to know which state it's in?
8. What does `Combat` need to remember?  Who you're fighting, surely.  What
   else?  Where you go back to when it's over?
9. A fight needs dice.  Where does the `Random` come from?  Think back to
   Chapter 6, and the one `Random` at the top of `Program.cs`.
10. How do you get into a fight?  How do you get out?  Is there more than one
    way, each way?
11. What replaces the `playing` flag?
12. **For Made It Mine.**  A hit should hurt.  Where does the creature's
    damage live?  There are two places it could go:  in the `Combat` state,
    which remembers how hurt the creature is for as long as the fight lasts,
    or in the creature itself, stored back in the `GameData`.  `HealthPoints`
    has no way to go down today.  What happens to the damage, each way, when
    you `flee` and come back?

## What you're building

1. A root type, `GameState`, with `Exploration` and `Combat` under it, and a
   way for the game to end that replaces the `playing` flag.
2. The game starts in `Exploration`, and everything that works today still
   works there.
3. The loop asks the current state what to do.  It doesn't check which state
   it's in.
4. `attack <creature>`, in an area where that creature is, starts a fight.
5. In `Combat`, `attack` rolls a d20 plus your `AttackBonus` against the
   creature's `ArmorClass`, and says whether you hit.  `flee` goes back to
   `Exploration`.  `go` gets refused, with a message that makes sense.
6. Your notes file, with the answers to the questions above, your state
   diagram, your sources, and your decisions.

## Finished means three things

- **The code** is on your branch, pushed, and green, and it does everything in
  *What you're building*.
- **The notes** have the questions answered in your words, your state diagram,
  your sources, and every decision you made with what it cost.
- **The presentation** is given.  See *The Presentation* page:  fifteen
  minutes, and one part of it is adding a new state, live, in front of us.

## Where you meet the others

- **Commands as Types** decides *what a command is*, and you decide *which
  commands are available*.  You're both changing the loop in `Program.cs`.
  Talk to each other in the first class, and agree on what the loop hands to a
  state.
- **Items That Do Things** might want `use` to work in a fight.  A health
  potion would.
- **Saving and Loading** might want to save which state the game is in.  Can
  you save in the middle of a fight?

## What good work looks like

| Stage | What it means |
|---|---|
| **Getting Started** | The build is red, or the loop still runs on the `playing` flag. |
| **Got It Working** | `GameState`, `Exploration`, and `Combat` exist.  The loop asks the state what to do, a fight starts with `attack`, attack rolls hit or miss against `ArmorClass`, `flee` ends it, and `go` is refused in a fight.  Everything that worked before still works. |
| **Made It Mine** | ...and the fight can be won:  a hit does damage, the creature's health goes down, and the fight ends when it reaches zero.  Your notes have your state diagram, and answer questions 7 and 12 with what each choice cost. |
| **Went Beyond** | ...and the creature fights back.  Or some creatures attack the moment you walk in.  Or a third state of your own design, on the diagram first. |

**Made It Mine is the target.**

## Stuck?

Ask Sparky, the helper for this project.  Sparky will teach you syntax all
day:  records, inheritance, pattern matching, and compiler errors.  It'll ask
you questions about the design, because the design is yours.
