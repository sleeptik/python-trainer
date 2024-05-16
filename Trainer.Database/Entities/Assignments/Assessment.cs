// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Trainer.Database.Entities.Assignments;

public sealed class Assessment
{
    public float Readability { get; private set; }
    public float Complexity { get; private set; }
    public float Creativity { get; private set; }
    public float Efficiency { get; private set; }
    public float Structure { get; private set; }
    public float Reasoning { get; private set; }
}