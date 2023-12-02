using Domain.Trainer;
using FluentAssertions;

namespace Domain.Unit;

public class AssignmentTests
{
    private readonly Assignment _assignment = new(1, 1);

    [Fact]
    public void ResultDefaultsToNull()
    {
        _assignment.IsPassed.Should().BeNull();
    }

    [Fact]
    public void SetsSuccessResult()
    {
        _assignment.SetResult(true);
        _assignment.IsPassed.Should().BeTrue();
    }

    [Fact]
    public void SetsFailResult()
    {
        _assignment.SetResult(false);
        _assignment.IsPassed.Should().BeFalse();
    }

    [Fact]
    public void ForbidsUpdateResults()
    {
        _assignment.SetResult(true);
        _assignment.Invoking(assignment => assignment.SetResult(true)).Should().Throw<InvalidOperationException>();
    }
}