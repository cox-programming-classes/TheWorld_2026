namespace TheWorld.GameMechanics;

/// <summary>
/// All the data in the game!  
/// This is the one place where all the Areas, Items, and Creatures are stored.  
/// It is injected into every builder, so that they can add their new creations to the game data.
/// 
/// It also remembers where the player is, in CurrentArea.
///
/// TODO: we need to add Players here too, so that we can save and load the game state eventually.
/// </summary>
public record GameData
{
    /// <summary>
    /// All the Area's in the game!
    /// </summary>
    private Dictionary<Guid,Area> Areas { get; init; } = [];
    public IReadOnlyDictionary<Guid, Area> ReadAreas=> Areas;

    /// <summary>
    /// All the Item's in the game!
    /// </summary>
    private Dictionary<Guid, Item> Items { get; init; } = [];
    public IReadOnlyDictionary<Guid, Item> ReadItems => Items;
    
    /// <summary>
    /// All the Creature's in the game!
    /// </summary>
    private Dictionary<Guid, Creature> Creatures { get; init; } = [];

    /// <summary>
    /// Where the player is right now.  It's a label, not the Area itself, so it never goes stale:
    /// gameData.Get(gameData.CurrentArea) always finds the latest version of the area, even after an Update.
    /// It's null until WorldBuilder.BuildWorld sets it, and the only way to change it is TrySetCurrentArea.
    /// </summary>
    public AreaReferenceLabel? CurrentArea { get; private set; }

    /// <summary>
    /// Moves the player to an area.  Refuses an area that isn't in the GameData, the same way TryUpdate does,
    /// so CurrentArea can never point at a place that doesn't exist.
    /// </summary>
    public bool TrySetCurrentArea(AreaReferenceLabel area)
    {
        if (!Areas.ContainsKey(area.Id))
            return false;

        CurrentArea = area;
        return true;
    }

    //--------------------------------------------------------------
    //  Methods for adding and retrieving Areas, Items, and Creatures.
    //--------------------------------------------------------------

    // TODO:  Results, Step 4 (Lesson 9):  TryAdd and TryUpdate answer true or false.  When one answers false,
    // what went wrong?  The caller has to guess.
    // Change all six to return a Result instead of a bool.  TryAdd(Area area) becomes:
    //     public Result<AreaReferenceLabel> TryAdd(Area area)
    // Success hands back the label for the thing that was stored.  A duplicate, or an update to something that
    // was never stored, is an ErrorResult with a message that says which.
    // Once these return labels, look at Build(GameData) in AreaBuilder, ItemBuilder, and CreatureBuilder.  Each one
    // turns a bool into a label or a null.  What should Build(GameData) return now?
    // While you're here:  does the name "TryAdd" still fit?  "Try" plus a bool was C#'s old way of saying
    // "this might fail."  What says it now?

    // Three methods named TryAdd.  They're Overloads:  C# picks one by the type you hand it.
    public bool TryAdd(Area area) => Areas.TryAdd(area.Id, area);
    public bool TryAdd(Item item) => Items.TryAdd(item.Id, item);
    public bool TryAdd(Creature creature) => Creatures.TryAdd(creature.Id, creature);

    // Three methods named TryUpdate.  Each one replaces a thing that's already stored, and refuses one that isn't.
    public bool TryUpdate(Area area)
    {
        if (!Areas.ContainsKey(area.Id))
            return false;

        Areas[area.Id] = area;
        return true;
    }

    public bool TryUpdate(Item item)
    {
        if (!Items.ContainsKey(item.Id))
            return false;

        Items[item.Id] = item;
        return true;
    }

    public bool TryUpdate(Creature creature)
    {
        if (!Creatures.ContainsKey(creature.Id))
            return false;

        Creatures[creature.Id] = creature;
        return true;
    }

    //--------------------------------------------------------------
    //  Methods for retrieving Areas, Items, and Creatures by their label.
    //--------------------------------------------------------------

    // TODO:  Results, Step 3 (Lesson 9, Meeting 2):  Every Get can hand back null, and a null can't say what went wrong.
    // Was the label null?  Was it a real label, for a thing that isn't in the GameData?  Every caller has to
    // remember to check, and Commands.Go has a ! in it that promises the null never happens.
    //
    // Change all three to return a Result (in Engine/Unions.cs):
    //     public Result<Area> Get(AreaReferenceLabel? label)
    // A thing that's found comes back as itself.  A null label, or a label with nothing behind it, comes back as
    // an ErrorResult whose message says which one happened.
    //
    // Why ErrorResult and not ValidationFailure?  A ValidationFailure means the INPUT was bad:  someone typed a
    // name that's blank.  An ErrorResult means the GAME is broken:  a label points at nothing.  Be ready to
    // defend which one you picked, every time you pick one.
    //
    // Then build.  It won't compile, and that's the point:  every error is a caller that used to be allowed to
    // ignore a missing thing, and isn't anymore.  Work through the list one at a time.  Each caller gets a
    // switch that handles every case of the Result, and the compiler checks that you didn't skip one.

    // Get gets Overloaded too, now that every kind of label is its own Type.  (In Lesson 7 every key was a string, so it couldn't be.)
    // A null label finds nothing, so gameData.Get(tree)?.Examine() works even when tree might be null.
    public Area? Get(AreaReferenceLabel? label) => label is null ? null : Areas.GetValueOrDefault(label.Id);
    public Item? Get(ItemReferenceLabel? label) => label is null ? null : Items.GetValueOrDefault(label.Id);
    public Creature? Get(CreatureReferenceLabel? label) => label is null ? null : Creatures.GetValueOrDefault(label.Id);
}

/// <summary>
/// A ReferenceLabel is a thing that has a label and a unique identifier.  It is pure data, and is not displayed ever.
/// It is a reference to a thing in the GameData, and GameData.Get uses it to find that thing.
///
/// This structure is useful for saving and loading game data, as it allows us to store a reference to a thing without storing the entire object.
/// The Label and Id are written once, and the <T> says what kind of thing the label is for.
/// The short names (ItemReferenceLabel, etc.) are Aliases, in GlobalIncludes.cs.
///
/// This is called a Strongly Typed Key.  The Guid inside could open anything, but the label's Type says what kind of thing it opens,
/// so the compiler won't let an ItemReferenceLabel go where an AreaReferenceLabel belongs.
/// </summary>
/// <typeparam name="T"></typeparam>
public record ReferenceLabel<T>
{
    public required string Label { get; init; }

    public required Guid Id { get; init; }
}

