namespace WebApi.Education;

public record GetNewExercisesResponse(
    string Contents,
    string Difficulty,
    string[] Subjects
);