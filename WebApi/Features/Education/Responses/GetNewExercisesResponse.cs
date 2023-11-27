namespace WebApi.Features.Education.Responses;

public record GetNewExercisesResponse(
    string Contents,
    string Difficulty,
    string[] Subjects
);