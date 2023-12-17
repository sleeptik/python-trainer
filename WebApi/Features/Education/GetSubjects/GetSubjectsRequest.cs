using Domain.Trainer;
using MediatR;

namespace WebApi.Features.Education.GetSubjects;

public record GetSubjectsRequest(int StudentId) : IRequest<IList<Subject>>;