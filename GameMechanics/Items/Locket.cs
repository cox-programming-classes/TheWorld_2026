using TheWorld.GameMechanics;

namespace TheWorld.GameMechanics.Items;


public record Locket : Item, ICarryable
{
    public Locket(string name, string description) : base(name, description)
    { }

    //Weight property, matching int Weight { get; }
   

    public int Weight { get; init; } = 2;
}