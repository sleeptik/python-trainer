using Domain.Trainer;

namespace Domain.Unit.Trainer;

public class StudentTests
{
    [Fact]
    public void SetScore_ChangeIsPositive_ScoreIncreasing()
    {
        // Arrange
        const float change = 25f;
        var student = Student.Create(1, MockRankFactory.CreateSingle(), Array.Empty<Subject>());

        // Act
        student.Score += change;

        // Assert
        student.Score.Should().Be(change);
    }

    [Fact]
    public void SetScore_ChangeIsNegative_ClampsToZero()
    {
        // Arrange
        const float change = -25f;
        var student = Student.Create(1, MockRankFactory.CreateSingle(), Array.Empty<Subject>());

        // Act
        student.Score += change;

        // Assert
        student.Score.Should().Be(0f);
    }

    [Fact]
    public void StudentCreate_ValidArguments_CreatesStudentAndTakesLowestRank()
    {
        // Arrange
        const int userId = 1;

        var ranks = MockRankFactory.CreateMultiple().ToList();
        var minRank = ranks.MinBy(rank => rank.LowerBound)!;

        var subjects = new[] { Subject.Create("Subject") };

        // Act
        var student = Student.Create(userId, ranks, subjects);

        // Assert
        student.CurrentRankId.Should().Be(minRank.Id);
    }

    [Fact]
    public void StudentCreate_NoRanks_ThrowsException()
    {
        // Arrange
        const int userId = 1;
        var ranks = MockRankFactory.CreateEmpty();

        var subjects = new[] { Subject.Create("Subject") };

        // Act & Assert 
        var action = () => Student.Create(userId, ranks, subjects);
        action.Invoking(func => func.Invoke()).Should().Throw<ArgumentException>();
    }

    [Fact]
    public void StudentCreate_NoSubjects_CreatesStudentWithoutSubjects()
    {
        // Arrange
        const int userId = 1;

        var ranks = MockRankFactory.CreateMultiple().ToList();
        var minRank = ranks.MinBy(rank => rank.LowerBound)!;

        var subjects = Enumerable.Empty<Subject>();

        // Act
        var student = Student.Create(userId, ranks, subjects);

        // Assert
        student.CurrentRankId.Should().Be(minRank.Id);
    }
}