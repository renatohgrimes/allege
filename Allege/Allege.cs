// © 2025 Renato Hartmann Grimes. All rights reserved.
// See LICENSE file for further information

using System.Collections;
using System.Runtime.CompilerServices;

// ReSharper disable CheckNamespace
namespace System;

/// <summary>
/// This static class includes methods to validate conditions, check for null values, 
/// and ensure objects are not empty. It is designed to simplify runtime 
/// validation and improve code reliability by throwing appropriate exceptions 
/// when conditions are not met.
/// </summary>
public static class Allege
{
    /// <summary>
    /// Ensures that a specified boolean condition is true.
    /// Throws an exception if the condition is null or evaluates to false.
    /// </summary>
    /// <param name="condition">The boolean condition to validate.</param>
    /// <param name="message">An optional message to include in the exception if the condition fails.</param>
    /// <exception cref="ArgumentNullException">Thrown when the condition is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the condition evaluates to false.</exception>
    public static void Condition(bool? condition, string? message = null)
    {
        if (condition is null)
        {
            throw new ArgumentNullException(nameof(condition));
        }

        if (!condition.Value)
        {
            throw new InvalidOperationException(message ?? "condition failed");
        }
    }

    /// <summary>
    /// Ensures that the specified object is not null.
    /// </summary>
    /// <param name="obj">The object to validate.</param>
    /// <param name="paramName">The name of the parameter being validated. Automatically captured by the compiler.</param>
    /// <exception cref="ArgumentNullException">Thrown when the object is null.</exception>
    public static void NotNull<T>(T? obj, [CallerArgumentExpression(nameof(obj))] string? paramName = null)
    {
        if (obj is null)
        {
            throw new ArgumentNullException(paramName);
        }
    }

    /// <summary>
    /// Ensures that the specified object is not null or empty.
    /// </summary>
    /// <param name="obj">The object to validate.</param>
    /// <param name="paramName">The name of the parameter being validated. Automatically captured by the compiler.</param>
    /// <exception cref="ArgumentNullException">Thrown when the object is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the object is an empty.</exception>
    /// <exception cref="NotSupportedException">Thrown when the object is an unsupported type.</exception>
    public static void NotEmpty<T>(T? obj, [CallerArgumentExpression(nameof(obj))] string? paramName = null)
    {
        if (obj is null)
        {
            throw new ArgumentNullException(paramName);
        }

        if (obj is string str)
        {
            if (str.Length == 0)
            {
                throw new ArgumentOutOfRangeException(paramName, obj, "String is empty");
            }

            return;
        }

        if (obj is ICollection col)
        {
            if (col.Count == 0)
            {
                throw new ArgumentOutOfRangeException(paramName, obj, "Collection is empty");
            }

            return;
        }
        
        if (obj is IEnumerable e)
        {
            var enumerator = e.GetEnumerator();

            try
            {
                if (!enumerator.MoveNext())
                {
                    throw new ArgumentOutOfRangeException(paramName, obj, "Enumerable is empty");
                }
            }
            finally
            {
                if (enumerator is IDisposable disposable)
                    disposable.Dispose();
            }

            return;
        }

        if (obj is not string &&
            obj is not ICollection &&
            obj is not IEnumerable)
        {
            throw new NotSupportedException(paramName);
        }
    }
}