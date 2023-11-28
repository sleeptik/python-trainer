namespace WebApi.Features.Education.Responses;

public record GetNewExerciseResponse(
    int Id,
    string Contents,
    string Difficulty,
    string[] Subjects
);