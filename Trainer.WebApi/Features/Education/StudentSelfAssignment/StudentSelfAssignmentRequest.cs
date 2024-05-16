using Domain.Trainer;
using MediatR;

namespace Trainer.WebApi.Features.Education.StudentSelfAssignment;

public record StudentSelfAssignmentRequest(int StudentId, int SubjectId) : IRequest<Assignment>;