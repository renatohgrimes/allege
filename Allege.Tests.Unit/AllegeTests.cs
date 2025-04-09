// © 2025 Renato Hartmann Grimes. All rights reserved.
// See LICENSE file for further information

using System.Net.Sockets;

namespace Allege.Tests.Unit;

public class AllegeTests
{
    [Fact]
    public void Condition_ShouldNotThrow_WhenConditionIsTrue()
    {
        // Act & Assert
        System.Allege.Condition(true);
        System.Allege.Condition("test".AsSpan().Length > 0);
    }

    [Fact]
    public void Condition_ShouldThrowArgumentNullException_WhenConditionIsNull()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => System.Allege.Condition(null));
    }

    [Fact]
    public void Condition_ShouldThrowInvalidOperationException_WhenConditionIsFalse()
    {
        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => System.Allege.Condition(false, "Custom message"));
        Assert.Equal("Custom message", exception.Message);
    }

    [Fact]
    public void NotNull_ShouldNotThrow_WhenObjectIsNotNull()
    {
        // Act & Assert
        System.Allege.NotNull(new object());
    }

    [Fact]
    public void NotNull_ShouldThrowArgumentNullException_WhenObjectIsNull()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => System.Allege.NotNull<object>(null));
    }

    [Fact]
    public void NotEmpty_ShouldNotThrow_WhenObjectIsNotEmpty()
    {
        // Act & Assert
        System.Allege.NotEmpty("Non-empty string");
        System.Allege.NotEmpty(new[] { 1 });
        System.Allege.NotEmpty(new[] { new object() });
        System.Allege.NotEmpty(Enumerable.Range(1, 10));
    }

    [Fact]
    public void NotEmpty_ShouldThrowArgumentNullException_WhenObjectIsNull()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => System.Allege.NotEmpty<string>(null));
    }

    [Fact]
    public void NotEmpty_ShouldThrowArgumentOutOfRangeException_WhenObjectIsEmpty()
    {
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => System.Allege.NotEmpty(""));
        Assert.Throws<ArgumentOutOfRangeException>(() => System.Allege.NotEmpty(Array.Empty<int>()));
        Assert.Throws<ArgumentOutOfRangeException>(() => System.Allege.NotEmpty(Enumerable.Empty<int>()));
    }
    
    [Fact]
    public void NotEmpty_ShouldThrowNotSupportedException_WhenObjectIsUnsupportedType()
    {
        // Act & Assert
        Assert.Throws<NotSupportedException>(() => System.Allege.NotEmpty(new object()));
    }
}