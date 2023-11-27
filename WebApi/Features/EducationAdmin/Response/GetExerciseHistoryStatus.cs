namespace WebApi.Features.EducationAdmin.Response;

public record GetExerciseHistoryStatus(
    bool AlreadyCompleted,
    bool HasErrors
);