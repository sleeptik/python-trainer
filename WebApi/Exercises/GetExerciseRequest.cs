using Domain.Trainer;
using MediatR;

namespace WebApi.Exercises;

public record GetExerciseRequest(int SubjectId, int DifficultyId) : IRequest<IList<Exercise>>;