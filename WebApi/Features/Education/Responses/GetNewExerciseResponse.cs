namespace WebApi.Features.Education.Responses;

public record GetNewExerciseResponse(
    string Contents,
    string Difficulty,
    string[] Subjects
);