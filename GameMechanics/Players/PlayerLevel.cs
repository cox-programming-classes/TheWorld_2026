namespace TheWorld.GameMechanics;

public record PlayerLevel
{
    public static PlayerLevel Level1 = new(0);
    public static PlayerLevel FromExperience(int xp) => new(xp);
    
    
    public int Experience { get; }
    
    /// <summary>
    /// TODO:  Calculate this in a FUTURE LESSON!
    /// </summary>
    public int Level { get; }

    private PlayerLevel(int Experience)
    {
        this.Experience = Experience;
        // TODO:  THIS IS BAD!
        this.Level = 1 + Experience / 100;
    }
}