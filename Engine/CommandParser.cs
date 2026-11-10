namespace TheWorld.Engine;

/// <summary>
/// What the player typed, taken apart.
/// "go dark forest" becomes Command "go", with Parameters ["dark", "forest"].
/// </summary>
/// <param name="Command">The first word, in lower case, so "GO" and "go" are the same command.  Empty if the player typed nothing.</param>
/// <param name="Parameters">Every word after the first, exactly as typed.  Empty if there was only one word.</param>
public record ParsedInput(string Command, IReadOnlyList<string> Parameters);

/// <summary>
/// The most basic parser there is:  break the line apart at the spaces.
/// The first word is the command, and the rest are its parameters.
///
/// It knows nothing about which commands exist, or what their parameters mean.
/// That's somebody else's job, for now the switch in Program.cs.
/// </summary>
public static class CommandParser
{
    public static ParsedInput Parse(string input)
    {
        // RemoveEmptyEntries:  "go    north" is two words, not five.
        // TrimEntries:  stray tabs and spaces come off each word.
        var words = input.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        if (words.Length == 0)
            return new(string.Empty, []);

        return new(words[0].ToLowerInvariant(), words[1..]);
    }
}
