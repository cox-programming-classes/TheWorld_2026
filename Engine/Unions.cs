namespace TheWorld_2026.Engine;


/// <summary>
/// Represents a validation failure with one or more error messages.
/// </summary>
/// <param name="ErrorMessages"></param>
public record ValidationFailure(params IReadOnlyList<string> ErrorMessages);
    
/// <summary>
/// Represents an error result with an error message.
/// </summary>
/// <param name="ErrorMessage"></param>
public record ErrorResult(string ErrorMessage);


/// <summary>
/// Represents a result that can be either a success, an error, or a validation failure.
/// </summary>
/// <typeparam name="T"></typeparam>
public union Result<T>(T, ErrorResult, ValidationFailure) where T : notnull;

/// <summary>
/// Represents the outcome of a validation: either the validated value or a validation failure.
/// </summary>
/// <typeparam name="T"></typeparam>
public union Validated<T>(T, ValidationFailure) where T : notnull;

