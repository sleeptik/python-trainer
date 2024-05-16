using Domain.Trainer;
using MediatR;

namespace Trainer.WebApi.Features.Education.GetStudentSubjectList;

public record GetStudentSubjectListRequest(int StudentId) : IRequest<IList<Subject>>;