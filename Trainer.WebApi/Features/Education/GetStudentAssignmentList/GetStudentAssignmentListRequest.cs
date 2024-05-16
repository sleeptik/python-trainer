using Domain.Trainer;
using MediatR;

namespace Trainer.WebApi.Features.Education.GetStudentAssignmentList;

public record GetStudentAssignmentListRequest(int StudentId) : IRequest<IList<Assignment>>;