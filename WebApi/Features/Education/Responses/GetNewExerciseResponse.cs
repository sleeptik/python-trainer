namespace WebApi.Features.Education.Responses;

public record GetNewExerciseResponse(
    int Id,
    string Contents,
    string Rank,
    string[] Subjects
);