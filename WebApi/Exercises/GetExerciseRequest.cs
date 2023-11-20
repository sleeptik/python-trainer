using Domain.Trainer;
using MediatR;

namespace WebApi.Exercises;

public record GetExerciseRequest(int SubjectId) : IRequest<IList<Exercise>>;