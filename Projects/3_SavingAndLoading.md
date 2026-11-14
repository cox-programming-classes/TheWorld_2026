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

(in my own words):
The main problem is that our game doesn't function like a regular game: the programmer has to code new areas, new objects, etc., physically, and the world (through GameData) cannot be saved. However, a real game saves data into a file (JSON), which allows the person to not have to continue adding new objects to the game and have their progress saved. The project tries to solve this issue, also regarding the fact that we use private dictionaries, which stop the serializer from writing, alongside private constructors that prevent it from reading. However, we make these private so that the type cannot be built in an invalid state.



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

Serialization is the process of converting the state of an object (values of its properties) into a form that can be stored and transmitted, but the serialized form doesn't provide any information about the object's methods.
Deserialization reconstructs an object from the serialized form.
JSON, standing for JavaScript Object Notation, is a text format for writing down data. It's a popular way of saving a file because it's written clearly and organized, making it readable and allowing the programmer to edit it by hand.

2. To *write* an object as JSON, what does `System.Text.Json` need from its
   type?  To *read* one back, what does it need?  Those aren't the same list.
   Why not?

To write (serialize) an object as JSON, "System.Text.Json" needs public members (properties). To read (deserialize) one back, it needs a public constructor it can call. Thus, writing and reading are not from the same list as writing only looks at an object that already exists, while reading has to build a new one by calling a constructor. 

3. What is a **DTO**?  Fowler's examples aren't games.  Translate one into
   ours.

A DTO, or Data Transfer Object, can hold all the data for the call. For instance, Fowler gives an example of where AlbumDTO is a plain copy of the data, with no behavior, while Album is the real object, with its own behavior and rules, as well as Artist. The assembler helps turn the objects into data (DTO), and the DTO back into the real object.
Translating this into our own game, Area (such as the clearing) is the real object; however, as its constructor is not public, the serializer can not reach it. The DTO holds the plain data and no behavior, only these properties: Id, Name, Description, Creatures, Items and Neighbors. Creatures is empty, while Items and Neighbors hold only labels, not whole objects. Unlike Fowler's example, the programmer writes the code that turns an Area into a DTO and back. The serializer only turns the DTO into JSON and back.

 
### Experiments first

Do these before you design anything, and paste exactly what happens into your
notes.

4. In `Program.cs`, right after the world is built, print
   `JsonSerializer.Serialize(gameData)`.  What came out?  Where did the areas
   go?
   sers/emilyjiang/Documents/GitHub/TheWorld_2026/bin/Debug/net10.0/TheWorld_2026
   {"CurrentArea":{"Label":"Clearing","Id":"542ec8f0-885c-42d3-8786-f2759ca21289"}}
   == Kyroki ==  level 1, 0 XP
   HP 8/8    Gold 8
   STR 13 (+1)   DEX 16 (+3)   CON 12 (+1)
   INT 18 (+4)   WIS 14 (+2)   CHA 17 (+3)

The areas weren't in the JSON because JSON only expresses public properties - Areas is declared private, while CurrentArea is declared public, and thus, only CurrentArea was expressed.

5. Serialize one `Area`, then try to read it back with
   `JsonSerializer.Deserialize<Area>(json)`.  Then do the same with a
   `Creature`.  What does each one say?  Copy the error word for word, and say
   what it means in your own words.

(for Area, first run, JSON was label) {"Label":"Clearing","Id":"bcab29b0-3e60-496a-ae68-0e6e427e0f46"}
Unhandled exception. System.NotSupportedException: Deserialization of types without a parameterless constructor, a singular parameterized constructor, or a parameterized constructor annotated with 'JsonConstructorAttribute' is not supported. Type 'TheWorld.GameMechanics.Area'. Path: $ | LineNumber: 0 | BytePositionInLine: 1.
---> System.NotSupportedException: Deserialization of types without a parameterless constructor, a singular parameterized constructor, or a parameterized constructor annotated with 'JsonConstructorAttribute' is not supported. Type 'TheWorld.GameMechanics.Area'.
--- End of inner exception stack trace ---
at System.Text.Json.ThrowHelper.ThrowNotSupportedException(ReadStack& state, Utf8JsonReader& reader, Exception innerException)
at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryRead(Utf8JsonReader& reader, Type typeToConvert, JsonSerializerOptions options, ReadStack& state, T& value)
   at System.Text.Json.Serialization.JsonConverter`1.TryRead(Utf8JsonReader& reader, Type typeToConvert, JsonSerializerOptions options, ReadStack& state, T& value, Boolean& isPopulatedValue)
at System.Text.Json.Serialization.JsonConverter`1.ReadCore(Utf8JsonReader& reader, T& value, JsonSerializerOptions options, ReadStack& state)
   at System.Text.Json.Serialization.Metadata.JsonTypeInfo`1.Deserialize(Utf8JsonReader& reader, ReadStack& state)
at System.Text.Json.JsonSerializer.ReadFromSpan[TValue](ReadOnlySpan`1 utf8Json, JsonTypeInfo`1 jsonTypeInfo, Nullable`1 actualByteCount)
   at System.Text.Json.JsonSerializer.ReadFromSpan[TValue](ReadOnlySpan`1 json, JsonTypeInfo`1 jsonTypeInfo)
at TheWorld.GameMechanics.WorldBuilder.BuildWorld() in /Users/emilyjiang/Documents/GitHub/TheWorld_2026/GameMechanics/WorldBuilder.cs:line 89
at Program.<Main>$(String[] args) in /Users/emilyjiang/Documents/GitHub/TheWorld_2026/Program.cs:line 16

For Area, the error states that the serializer can only use three kinds of constructor: a public parameterless one, a single public parameterized one, or one marked with a parameterized constructor. The JSON printed was an AreaReferenceLabel (for clearing), and the exception regarded the Area's internal constructor. 

(for Area, second run, JSON was full Area: ID, Name, Description, Items, Creatures, Neighbors) /Users/emilyjiang/Documents/GitHub/TheWorld_2026/bin/Debug/net10.0/TheWorld_2026
{"Id":"21abb70c-376e-407f-b4d3-15234e64487b","Name":"Clearing","Description":"A patch of sunlight at the edge of the forest.  Someone camped here, not long ago.","Creatures":[],"Items":[{"Label":"Statue","Id":"1186a931-0704-496a-93f9-b0d7d6fd8636"},{"Label":"Lantern","Id":"464b9147-29ed-4ada-82f6-6217ad146bad"},{"Label":"Locket","Id":"a0ea55b0-e7dc-49a7-9b76-1f9b273db18a"},{"Label":"Health Potion","Id":"ab62aec1-031b-4d29-b0f9-052a177dde54"},{"Label":"Chest","Id":"97055b32-8658-4a60-98fc-eab956fa03cd"}],"Neighbors":[{"Label":"Forest","Id":"f8500936-bd07-4671-b810-e205c1989df4"}]}
Unhandled exception. System.NotSupportedException: Deserialization of types without a parameterless constructor, a singular parameterized constructor, or a parameterized constructor annotated with 'JsonConstructorAttribute' is not supported. Type 'TheWorld.GameMechanics.Area'. Path: $ | LineNumber: 0 | BytePositionInLine: 1.
---> System.NotSupportedException: Deserialization of types without a parameterless constructor, a singular parameterized constructor, or a parameterized constructor annotated with 'JsonConstructorAttribute' is not supported. Type 'TheWorld.GameMechanics.Area'.
--- End of inner exception stack trace ---
at System.Text.Json.ThrowHelper.ThrowNotSupportedException(ReadStack& state, Utf8JsonReader& reader, Exception innerException)
at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryRead(Utf8JsonReader& reader, Type typeToConvert, JsonSerializerOptions options, ReadStack& state, T& value)
   at System.Text.Json.Serialization.JsonConverter`1.TryRead(Utf8JsonReader& reader, Type typeToConvert, JsonSerializerOptions options, ReadStack& state, T& value, Boolean& isPopulatedValue)
at System.Text.Json.Serialization.JsonConverter`1.ReadCore(Utf8JsonReader& reader, T& value, JsonSerializerOptions options, ReadStack& state)
   at System.Text.Json.Serialization.Metadata.JsonTypeInfo`1.Deserialize(Utf8JsonReader& reader, ReadStack& state)
at System.Text.Json.JsonSerializer.ReadFromSpan[TValue](ReadOnlySpan`1 utf8Json, JsonTypeInfo`1 jsonTypeInfo, Nullable`1 actualByteCount)
   at System.Text.Json.JsonSerializer.ReadFromSpan[TValue](ReadOnlySpan`1 json, JsonTypeInfo`1 jsonTypeInfo)
at TheWorld.GameMechanics.WorldBuilder.BuildWorld() in /Users/emilyjiang/Documents/GitHub/TheWorld_2026/GameMechanics/WorldBuilder.cs:line 94
at Program.<Main>$(String[] args) in /Users/emilyjiang/Documents/GitHub/TheWorld_2026/Program.cs:line 16

(Creature): /Users/emilyjiang/Documents/GitHub/TheWorld_2026/bin/Debug/net10.0/TheWorld_2026
{"Id":"eb0657b3-7b8a-4df7-9853-b7e99469a38f","Name":"Wolf","HealthPoints":{"HitDice":{"Count":2,"Sides":8,"Modifier":2,"Minimum":4,"Maximum":18},"CurrentHealth":11,"MaxHealth":11},"Abilities":{"Strength":12,"Dexterity":15,"Constitution":12,"Intelligence":3,"Wisdom":12,"Charisma":6},"ExperienceReward":50}
Unhandled exception. System.NotSupportedException: Deserialization of types without a parameterless constructor, a singular parameterized constructor, or a parameterized constructor annotated with 'JsonConstructorAttribute' is not supported. Type 'TheWorld.GameMechanics.Creature'. Path: $ | LineNumber: 0 | BytePositionInLine: 1.
---> System.NotSupportedException: Deserialization of types without a parameterless constructor, a singular parameterized constructor, or a parameterized constructor annotated with 'JsonConstructorAttribute' is not supported. Type 'TheWorld.GameMechanics.Creature'.
--- End of inner exception stack trace ---
at System.Text.Json.ThrowHelper.ThrowNotSupportedException(ReadStack& state, Utf8JsonReader& reader, Exception innerException)
at System.Text.Json.Serialization.Converters.ObjectDefaultConverter`1.OnTryRead(Utf8JsonReader& reader, Type typeToConvert, JsonSerializerOptions options, ReadStack& state, T& value)
   at System.Text.Json.Serialization.JsonConverter`1.TryRead(Utf8JsonReader& reader, Type typeToConvert, JsonSerializerOptions options, ReadStack& state, T& value, Boolean& isPopulatedValue)
at System.Text.Json.Serialization.JsonConverter`1.ReadCore(Utf8JsonReader& reader, T& value, JsonSerializerOptions options, ReadStack& state)
   at System.Text.Json.Serialization.Metadata.JsonTypeInfo`1.Deserialize(Utf8JsonReader& reader, ReadStack& state)
at System.Text.Json.JsonSerializer.ReadFromSpan[TValue](ReadOnlySpan`1 utf8Json, JsonTypeInfo`1 jsonTypeInfo, Nullable`1 actualByteCount)
   at System.Text.Json.JsonSerializer.ReadFromSpan[TValue](ReadOnlySpan`1 json, JsonTypeInfo`1 jsonTypeInfo)
at TheWorld.GameMechanics.WorldBuilder.BuildWorld() in /Users/emilyjiang/Documents/GitHub/TheWorld_2026/GameMechanics/WorldBuilder.cs:line 99
at Program.<Main>$(String[] args) in /Users/emilyjiang/Documents/GitHub/TheWorld_2026/Program.cs:line 16

For the Creature run, we serialized the real Creature, and the error states that the serializer can only use three kinds of constructor: a public parameterless one, a single public parameterized one, or a constructor marked with JsonConstructor. Creature has a private constructor, making it run the exception for the call "Deserialize <Creature>(json)."

Both Creature and Area failed to deserialize because Area had an internal constructor while Creature had a private constructor. However, deserialization requires public constructors.


Source:
Microsoft's overview of `System.Text.Json`, the JSON library that comes with .NET:
[Serialize and deserialize JSON using C#](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/overview).
Martin Fowler's short entry on the **Data Transfer Object**:
  [martinfowler.com/eaaCatalog/dataTransferObject.html](https://martinfowler.com/eaaCatalog/dataTransferObject.html).


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
