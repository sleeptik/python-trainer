using Domain.Trainer;
using MediatR;

namespace WebApi.Features.Education.GetAssignment;

public record GetAssignmentRequest(int StudentId, int ExerciseId) : IRequest<Assignment>;