using MediatR;
using WebApi.Features.Education.Responses;

namespace WebApi.Features.Education.Requests;

public record GetNewExerciseRequest(int SubjectId) : IRequest<GetNewExerciseResponse>;