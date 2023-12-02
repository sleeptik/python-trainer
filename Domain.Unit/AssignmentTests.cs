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
    public void SetsDateOnFinishing()
    {
        _assignment.Finish();
        _assignment.FinishedAt.Should().NotBeNull();
    }

    [Fact]
    public void ForbidsUpdatingResults()
    {
        _assignment.Finish();
        _assignment.Invoking(assignment => assignment.Finish())
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Updating finish date is forbidden");
    }
}