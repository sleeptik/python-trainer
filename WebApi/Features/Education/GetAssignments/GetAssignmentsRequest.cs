using Domain.Trainer;
using MediatR;

namespace WebApi.Features.Education.GetAssignments;

public record GetAssignmentsRequest(int StudentId) : IRequest<IList<Assignment>>;