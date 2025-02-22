using System.Diagnostics.CodeAnalysis;

namespace BoardBard.Core.Tools;

public abstract class ArgumentExceptionExtended : ArgumentException
{
    public static void ThrowIfAnyNullOrWhiteSpace(params string?[] values)
    {
        if (values == null || values.Any(string.IsNullOrWhiteSpace))
            throw new ArgumentException("One or more arguments are null, empty, or white-space.");
    }
}