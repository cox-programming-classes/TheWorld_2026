namespace TheWorld.GameMechanics;

public record GameDataDto(IReadOnlyList<ItemDto> Items, IReadOnlyList<AreaDto> Areas, AreaReferenceLabel? CurrentArea);

