namespace WebApi.Features.EducationAdmin.Response;

public record GetAssignmentStatusResponse(
    bool AlreadyCompleted,
    bool HasErrors
);