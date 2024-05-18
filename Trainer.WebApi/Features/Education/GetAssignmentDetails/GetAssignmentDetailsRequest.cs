using Domain.Trainer;
using MediatR;

namespace Trainer.WebApi.Features.Education.GetAssignmentDetails;

public record GetAssignmentDetailsRequest(int StudentId, int ExerciseId) : IRequest<Assignment>;