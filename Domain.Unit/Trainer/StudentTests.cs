using Domain.Trainer;
using FluentAssertions;

namespace Domain.Unit.Trainer;

public class StudentTests
{
    private readonly Student _student = new(default);

    [Theory]
    [InlineData(25)]
    [InlineData(50)]
    [InlineData(75)]
    [InlineData(100)]
    public void AddsToScore(float change)
    {
        _student.Score += change;
        _student.Score.Should().Be(change);
    }

    [Theory]
    [InlineData(-25)]
    [InlineData(-50)]
    [InlineData(-75)]
    [InlineData(-100)]
    public void ClampsScoreToZero(float change)
    {
        _student.Score += change;
        _student.Score.Should().Be(0f);
    }
}