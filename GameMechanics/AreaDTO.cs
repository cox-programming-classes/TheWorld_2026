namespace TheWorld.GameMechanics;

//all those reference labels - pointers/references to the items, creatures, neighbors (areas), without actually holding those objects itself in this
public record AreaDto(Guid Id, string Name, string Description, IReadOnlyList<ItemReferenceLabel> Items,  IReadOnlyList<CreatureReferenceLabel> Creatures,  IReadOnlyList<AreaReferenceLabel> Neighbors);
