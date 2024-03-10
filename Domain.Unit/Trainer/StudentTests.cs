using Domain.Trainer;

namespace Domain.Unit.Trainer;

public class StudentTests
{
    private readonly Student _student = new(default);

    [Theory]
    [InlineData(25)]
    [InlineData(50)]
    [InlineData(75)]
    [InlineData(100)]
    public void SetScore_ChangeIsPositive_ScoreIncreasing(float change)
    {
        _student.Score += change;
        _student.Score.Should().Be(change);
    }

    [Theory]
    [InlineData(-25)]
    [InlineData(-50)]
    [InlineData(-75)]
    [InlineData(-100)]
    public void SetScore_ChangeIsNegative_ClampsToZero(float change)
    {
        _student.Score += change;
        _student.Score.Should().Be(0f);
    }
}