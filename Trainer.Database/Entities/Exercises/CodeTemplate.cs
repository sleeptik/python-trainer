namespace Trainer.Database.Entities.Exercises;

public sealed class CodeTemplate
{
    public int Id { get; private set; } = default;
    public string SkippedPartName { get; private set; } = null!;
    public string Code { get; private set; } = null!;
    
    public int ExerciseId { get; private set; } = default;
    public Exercise Exercise { get; private set; } = null!;
}