using Domain.Trainer;
using MediatR;

namespace WebApi.Features.Education.StudentSelfAssignment;

public record StudentSelfAssignmentRequest(int StudentId, int SubjectId) : IRequest<Exercise>;