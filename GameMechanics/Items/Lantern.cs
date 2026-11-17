using TheWorld.GameMechanics;

namespace TheWorld.GameMechanics.Items;


public record Lantern : Item, ICarryable, IUsable
{
    public Lantern(string name, string description) : base(name, description)
    { }
    public bool IsLit { get; private set; }

    //Weight property, matching int Weight { get; }
    // a Use method, matching string Use(GameData gameData)

    public int Weight { get; init; } = 7;

    public void Use(GameData gameData)
    {
        IsLit = !IsLit;
        Console.WriteLine($"You have turned the lantern {(IsLit ? "on" : "off")}");
    }
}



