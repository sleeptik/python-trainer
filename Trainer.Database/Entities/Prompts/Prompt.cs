using Trainer.Database.Entities.Exercises;

// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Trainer.Database.Entities.Prompts;

public sealed class Prompt
{
    public int Id { get; private set; }
    public string Content { get; private set; } = null!;

    public int SubjectId { get; private set; }
    public Subject Subject { get; private set; } = null!;
}