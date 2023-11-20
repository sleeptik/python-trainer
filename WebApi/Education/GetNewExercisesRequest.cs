using Domain.Trainer;
using MediatR;

namespace WebApi.Education;

public record GetNewExercisesRequest(int SubjectId) : IRequest<IList<Exercise>>;