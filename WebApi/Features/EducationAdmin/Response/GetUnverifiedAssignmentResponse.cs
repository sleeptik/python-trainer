namespace WebApi.Features.EducationAdmin.Response;

public record GetUnverifiedAssignmentResponse(
    int StudentId, int ExerciseId, string ExerciseContents
);