using Domain.Trainer;
using MediatR;

namespace WebApi.Features.Education.GetStudentAssignmentList;

public record GetStudentAssignmentListRequest(int StudentId) : IRequest<IList<Assignment>>;