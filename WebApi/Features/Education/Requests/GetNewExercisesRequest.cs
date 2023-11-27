using MediatR;
using WebApi.Features.Education.Responses;

namespace WebApi.Features.Education.Requests;

public record GetNewExercisesRequest(int SubjectId) : IRequest<IList<GetNewExercisesResponse>>;