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

| Project | Your branch | The question |
|---|---|---|
| [1 - Commands as Types](Projects/1_CommandsAsTypes.md) | `commands-as-types` | A command is a string right now.  What if it were a thing? |
| [2 - Items That Do Things](Projects/2_ItemsThatDoThings.md) | `items-that-do-things` | A statue and a health potion are the same type.  They shouldn't be. |
| [3 - Saving and Loading](Projects/3_SavingAndLoading.md) | `saving-and-loading` | The world lives in code.  It should be able to live in a file. |
| [4 - Game States](Projects/4_GameStates.md) | `game-states` | In a fight, `go` shouldn't just walk you out of the room. |

Every project ends in a fifteen-minute presentation:  see
[The Presentation](Projects/Presentation.md).  Keep your research log in
`Projects/Notes/`, starting from [the template](Projects/Notes/Template.md).
