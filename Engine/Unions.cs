namespace TheWorld.Engine;


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

// TODO:  Results, Step 7 (Lesson 9):  By now you've written this switch over and over:
//     someResult switch
//     {
//         Area area           => /* the next step, using area */,
//         ErrorResult e       => e,
//         ValidationFailure f => f,
//     };
// Only the first line ever changes.  The other two just pass the failure along.
// Pull that pattern out into one extension method, in a new file Engine/ResultExtensions.cs:
//     public static Result<U> Then<T, U>(this Result<T> result, Func<T, Result<U>> next)
// It runs `next` when the result is a success, and passes any failure straight through untouched.
// Two hints, when you get there:  Result<T> only allows a T that can't be null, so Then has to promise the same
// thing about T and U.  And if your lambda returns a plain string, C# can't work out what U is (error CS0411).
// Read that error carefully:  it tells you two ways out.
// Then rewrite Commands.Go as a chain of Thens, with no switch in it at all.
// Research:  what you just wrote is called "bind," and a type with a bind like this is called a Monad.

