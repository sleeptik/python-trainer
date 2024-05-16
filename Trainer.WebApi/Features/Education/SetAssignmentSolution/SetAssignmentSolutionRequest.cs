using Infrastructure.ChatBot;
using MediatR;

namespace Trainer.WebApi.Features.Education.SetAssignmentSolution;

public record SetAssignmentSolutionRequest(int StudentId, int ExerciseId, string Solution)
    : IRequest<VerificationResult>;