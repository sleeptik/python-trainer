using Domain.Trainer;
using MediatR;

namespace WebApi.Features.Education.SelfAssignment;

public record SelfAssignmentRequest(int SubjectId, int StudentId = 0) : IRequest<Exercise>;