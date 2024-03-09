using Domain.Trainer;
using MediatR;

namespace WebApi.Features.Education.GetAssignmentDetails;

public record GetAssignmentDetailsRequest(int StudentId, int ExerciseId) : IRequest<Assignment>;