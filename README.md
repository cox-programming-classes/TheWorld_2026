# Interlude - The Game Loop

```
dotnet run
```

This is the last starter code you'll get from me.  From here on, the game is
ours, and this repository is where it lives.

It starts from the finished Chapter 8 code and adds four things:

| What | Where | What it does |
|---|---|---|
| The game loop | `Program.cs` | Builds the world, then reads a command, does it, and shows what happened, until you type `quit` |
| A command parser | `Engine/CommandParser.cs` | Splits a line at the spaces.  The first word is the command, and the rest are its parameters |
| Two commands | `Engine/Commands.cs` | `look`, `look <item>`, and `go <place>` |
| `WorldBuilder` | `GameMechanics/WorldBuilder.cs` | One static method, `BuildWorld`, that builds every area, item, and creature and hands back the `GameData` |

`GameData` also remembers where the player is now, in `CurrentArea`.
`BuildWorld` sets it before it returns, and `go` changes it through
`TrySetCurrentArea`, which refuses an area that isn't in the `GameData`.

Play it before you read it.  Walk from the clearing to the cave and back, and
then try `look lantern`, `LOOK   health potion`, and `dance`.

## Four research projects

Each of you takes one of these and runs it in parallel with the other three.
Each one is a question the code above can't answer yet.  You'll research it,
reason about it, build it, and then teach it to the rest of us, because the
other three projects will be building on top of yours.

Work on a branch of your own, named for your project.

### 1 - Commands as Types

Right now a command is a `string`, and the `switch` in `Program.cs` matches it
to a method.  Every new command means another `case`, and nothing stops
`"look"` from being misspelled in one place and spelled right in the other.

Make every command a `record` of its own:  `Look`, `Go`, `Quit`, and whatever
comes next, all sharing one root type.  The parser should hand back one of
those records instead of a `ParsedInput`.

This is polymorphism, coming at it from one direction:  many types that can all
stand in the place of one.

- What does the root type know, and what does each command record know?
- Where does the code that *does* a command go:  inside the record, or beside it?
  Argue it, using Chapter 3.
- What happens to the `switch`?

### 2 - Items That Do Things

A Statue and a Health Potion are both just an `Item` right now, with a name and
a description.  `StockItems.cs` sorts them into scenery, carryable, usable, and
harder-to-sort, and that sorting only lives in the comments.

Design and build at least `ICarryable` and `IUsable` as interfaces, and a
handful of items of each kind.  Some items should be both.

- Why interfaces, and not `CarryableItem` and `UsableItem` as child classes of
  `Item`?  The Lantern is the case to think about.  So is the Brass Key.
- Be ready to explain to the class how an interface is used differently from a
  child class.  That explanation is part of the project.
- You'll want `take` and `use` commands to test with.  Add them the way `look`
  and `go` work now.  Project 1 will change how commands work, and that's
  fine.

### 3 - Saving and Loading

Every time the game starts, `WorldBuilder` writes the world out in code.  A
real game loads its world from a file, and saves your progress to one.

Research JSON serialization in .NET (`System.Text.Json`), and the idea of a
**DTO**, a Data Transfer Object.  Then build a way to save a `GameData` to a
file, and to load one back:  something like `WorldBuilder.LoadWorld(path)`.

- Our types fight serialization on purpose:  private constructors, private
  dictionaries in `GameData`, `init`-only properties.  Why is a DTO the answer
  to that, and not making everything `public`?
- Read the `TODO` above every `Id`.  A loaded `Area` has to come back with the
  identity it was saved with.  Why does that matter for every label that points
  at it?
- `AreaBuilder.FromJson` and `PlayerBuilder.FromJson` are waiting for you.

### 4 - Game States

The game loop treats every moment the same way.  In a fight, `go` shouldn't
just walk you out of the room, and `attack` means nothing when nothing's there.

Build a **state machine**.  `GameState` is the root type, and the game starts
in an `Exploration` state.  The current state decides which commands are
available, and what the loop does each time around.

- What does a state know?  What does it decide?  Who decides when the state
  changes?
- Your first TODO is a `Combat` state.  How do you get into it, what can you do
  while you're in it, and how do you get out?  `Rules/Combat.cs` has the
  numbers a fight needs.

### Where the projects meet

None of these stays separate for long.

- **1 and 4** both reach into the game loop.  Project 4 decides *which*
  commands are available, and Project 1 decides *what a command is*.  Talk to
  each other early.
- **2** needs commands (`take`, `use`) that **1** will rebuild.
- **3** has to save whatever **2** and **4** add to the game:  items of new
  kinds, and which state the game is in.

When your project is done, write down where it touches the others, as a
question for them.  That list becomes our next conversation.
