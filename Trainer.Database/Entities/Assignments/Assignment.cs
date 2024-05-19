// ReSharper disable UnusedAutoPropertyAccessor.Local

using Trainer.Database.Entities.Exercises;
using Trainer.Database.Entities.Students;

namespace Trainer.Database.Entities.Assignments;

public sealed class Assignment
{
    private Assignment()
    {
    }

    public int Id { get; private set; }

    public int StudentId { get; private set; }
    public Student Student { get; private set; } = null!;

    public int ExerciseId { get; private set; }
    public Exercise Exercise { get; private set; } = null!;

    public DateTime AssignedAt { get; private set; }

    public IList<Solution> Solutions { get; private set; } = new List<Solution>();

    public static Assignment Create(int studentId, int exerciseId)
    {
        return new Assignment
        {
            StudentId = studentId,
            ExerciseId = exerciseId
        };
    }

    public void AddSolution(Solution solution)
    {
        Solutions.Add(solution);
    }

}