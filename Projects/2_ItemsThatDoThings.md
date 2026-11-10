# Items That Do Things

*A statue and a health potion are the same type right now.  They shouldn't be.*

**Four class periods, plus the usual fifteen minutes a night.  You present on
the fifth meeting.**  This is a research project:  the questions below point
you in a direction, and finding the answers is the work.

## Getting the code

Clone `TheWorld_2026` in GitHub Desktop, then make a branch named
`items-that-do-things` with **Branch > New Branch**.  Every commit goes on that
branch.  Push at the end of every class.

Copy `Projects/Notes/Template.md` to `Projects/Notes/ItemsThatDoThings.md`.
That's your research log, and it's part of the grade.

## The problem

Open `GameMechanics/Items/StockItems.cs`.  The items in it are sorted into
groups:  scenery you can only look at, things you can carry, things you can
use, things you can carry *and* use, and a few that are harder to sort.

That sorting lives in the comments, and nowhere else.  To the compiler, a
`Statue` and a `HealthPotion` are both an `Item`, with a `Name` and a
`Description`, and that's all.  The game can't tell which one you're allowed
to pick up.

Your job is to make the code know.  You'll design and build at least two
**interfaces**, `ICarryable` and `IUsable`, and a handful of items of each
kind, some of them both.  Then you'll add `take` and `use` to the game so we
can see them work.

The part of this project I care most about is the *why*.  There's another way
to do this, with child classes of `Item`:  a `CarryableItem`, a `UsableItem`.
By the end, you should be able to stand up in front of the class and explain
why interfaces are the better tool here, and what they cost.

## Where to start looking

- `GameMechanics/Items/`:  `Item.cs`, `ItemBuilder.cs`, and `StockItems.cs`.
  Then `Engine/Commands.cs` and the loop in `Program.cs`, where the new
  commands go.
- Microsoft's page on interfaces:
  [Interfaces - define behavior for multiple types](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/interfaces).
- Microsoft's page on inheritance, for the other side of the argument:
  [Object-oriented programming - inheritance](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/inheritance).
- Microsoft's page on records, especially the part on inheritance, because
  `Item` is a record:
  [Records - C# reference](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/record).

Good search terms, once those run out:  *interface vs abstract class*,
*composition over inheritance*, *is-a vs can-do*, *C# is pattern*.

## Questions to research

Answer these in your notes, in your own words.  They're in roughly the order
you'll need them.

### Understanding the idea

1. What is an **interface** in C#?  What does it promise, and what does it
   leave to the type that implements it?
2. What's the difference between saying a lantern **is an** item and saying a
   lantern **can be** carried?  Which of those is inheritance for, and which
   is an interface for?
3. How many types can a C# class or record inherit from?  How many interfaces
   can it implement?  Find the reason the language chose that, and write it
   down.

### Reading our code

4. Sort every item in `StockItems.cs` into a table:  carryable or not, usable
   or not.  Which ones were hard to place, and why?  The Brass Key, the Chest,
   and the Wanted Poster are there on purpose.
5. Where in our game would the difference show up?  Which commands need to
   know whether an item can be carried?  Which need to know whether it can be
   used?

### Designing it

6. **Draw it with child classes first.**  On paper, make `CarryableItem` and
   `UsableItem`, both children of `Item`.  Where does the `Lantern` go?  Now
   add a third kind, something you can read, like the Wanted Poster.  How many
   classes do you need?  Then draw it again with interfaces, and count again.
7. What does `ICarryable` need to promise?  Does it need any members at all?
   What about `IUsable`:  what does using something need to know about the
   world, and what does it hand back?
8. How does `take` find out whether the thing you named can be taken?  Look up
   the `is` keyword in the pattern-matching docs.
9. Taking an item changes the area it was in.  In Chapter 8, how did a stored
   area change?
10. Where does an item go when you pick it up?  The `Player` isn't in the
    `GameData` yet.  Decide where carried things live, and defend it.
11. `ItemBuilder.Build()` always makes a plain `Item`.  How does a `Lantern`
    get built, and filed in the `GameData`?  Before you decide, read how
    `WorldBuilder` files the stock items.  Then make `StockItems.Lantern` hand
    back a `Lantern`, and check whether the lantern in the clearing is one.  Write
    down what you find, and why.
12. Using the lantern should light it.  But a `Lantern` is a record, and it
    can't change.  In Chapter 8, how did a stored thing change?

## What you're building

1. `ICarryable` and `IUsable`, as interfaces.
2. At least three items that are carryable, at least three that are usable,
   and at least one that's both.  Scenery, like the statue, stays neither.
3. `take <item>` picks up a carryable item and takes it out of the area.  Try
   to take the statue, and the game says no.
4. `use <item>` does something you can see, and says no to an item that can't
   be used.
5. `inventory` lists what you're carrying.
6. Add the commands the way `look` and `go` work now:  a new `case` in
   `Program.cs`, and a method in `Commands.cs`.  The Commands as Types project
   is changing how commands work, and that's fine.
7. Your notes file, with the answers to the questions above, your sources, and
   your decisions.

## Finished means three things

- **The code** is on your branch, pushed, and green, and it does everything in
  *What you're building*.
- **The notes** have the questions answered in your words, your sources, the
  drawings from question 6, and every decision you made with what it cost.
- **The presentation** is given.  See *The Presentation* page:  fifteen
  minutes, and one part of it is adding a new item, live, in front of us.  You
  are teaching the class interfaces, and they haven't met them yet.

## Where you meet the others

- **Saving and Loading** has to save your items.  When a `Lantern` gets saved
  and loaded, how will the loader know to make a `Lantern`, and not a plain
  `Item`?  That's the hardest question between you, so ask it early.
- **Commands as Types** will turn your `take` and `use` into records, once you
  both have something working.
- **Game States** might want `use` to work in a fight.  A health potion would.

## What good work looks like

| Stage | What it means |
|---|---|
| **Getting Started** | The build is red, or every item is still a plain `Item`. |
| **Got It Working** | `ICarryable` and `IUsable` exist, at least three items of each kind and one of both, and `take`, `use`, and `inventory` work.  The statue can't be taken. |
| **Made It Mine** | ...and some of the items are your own invention, with uses that do something interesting.  Your notes have both drawings from question 6, counted, and you can explain with the Lantern why interfaces fit here and child classes don't. |
| **Went Beyond** | ...and a third interface, for one of the harder-to-sort items.  The Brass Key opens the Chest, maybe:  an item you use *on* another one.  Or research *default interface methods*, and decide whether they belong in our game. |

**Made It Mine is the target.**

## Stuck?

Ask Sparky, the helper for this project.  Sparky will teach you syntax all
day:  interfaces, inheritance, pattern matching, and compiler errors.  It'll
ask you questions about the design, because the design is yours.
