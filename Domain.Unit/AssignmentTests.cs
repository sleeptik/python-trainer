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

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void SetsResult(bool result)
    {
        _assignment.SetResult(result);
        _assignment.IsPassed.Should().Be(result);
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