namespace TheWorld.GameMechanics;


public record AreaDto(Guid Id, string Name, string Description, IReadOnlyList<ItemReferenceLabel> Items,  IReadOnlyList<CreatureReferenceLabel> Creatures,  IReadOnlyList<AreaReferenceLabel> Neighbors);
