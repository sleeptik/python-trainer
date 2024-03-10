using Domain.Trainer;

namespace Domain.Unit.Trainer;

public class AssignmentTests
{
    private readonly Assignment _assignment = Assignment.Create(default, default);

    [Fact]
    public void Constructor_AssignmentCreated_ResultIsNull()
    {
        _assignment.IsPassed.Should().BeNull();
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void SetResult_ResultWasNotSetBefore_ResultSet(bool result)
    {
        _assignment.SetResult(result);
        _assignment.IsPassed.Should().Be(result);
    }

    [Fact]
    public void SetResult_ResultWasSetBefore_ThrowsException()
    {
        _assignment.SetResult(true);
        _assignment.Invoking(assignment => assignment.SetResult(true))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Updating completion status is forbidden");
    }

    [Fact]
    public void Finish_WasNotFinishedBefore_FinishedAtSet()
    {
        _assignment.Finish("");
        _assignment.FinishedAt.Should().NotBeNull();
    }

    [Fact]
    public void Finish_WasFinishedBefore_ThrowsException()
    {
        _assignment.Finish("");
        _assignment.Invoking(assignment => assignment.Finish(""))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Updating solution is forbidden");
    }
}