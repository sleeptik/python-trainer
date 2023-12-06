using Infrastructure.ChatBot;
using MediatR;

namespace WebApi.Features.Education.Requests;

public record FinishExerciseCommand(int StudentId, int ExerciseId, string Solution) : IRequest<VerificationResult>;