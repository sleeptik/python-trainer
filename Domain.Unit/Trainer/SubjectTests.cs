using Domain.Trainer;

namespace Domain.Unit.Trainer;

public class SubjectTests
{
    [Fact]
    public void SubjectCreate_ValidData_ReturnsSubject()
    {
        var subject = Subject.Create("name");
        subject.Should().NotBeNull();
    }

    [Fact]
    public void SubjectCreate_EmptyName_ThrowsException()
    {
        Action create = () => Subject.Create("");
        create.Invoking(action => action.Invoke()).Should().Throw<ArgumentException>();
    }

    [Fact]
    public void SubjectCreate_NullName_ThrowsException()
    {
        Action create = () => Subject.Create(null);
        create.Invoking(action => action.Invoke()).Should().Throw<ArgumentException>();
    }
}