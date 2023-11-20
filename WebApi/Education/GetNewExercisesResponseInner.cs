namespace WebApi.Education;

public record GetNewExercisesResponseInner(
    string Contents,
    string Difficulty,
    string[] Subjects
);