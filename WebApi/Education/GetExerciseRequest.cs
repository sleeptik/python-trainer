using Domain.Trainer;
using MediatR;

namespace WebApi.Education;

public record GetExerciseRequest(int SubjectId) : IRequest<IList<Exercise>>;