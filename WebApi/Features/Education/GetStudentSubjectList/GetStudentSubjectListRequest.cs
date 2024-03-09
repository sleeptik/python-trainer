using Domain.Trainer;
using MediatR;

namespace WebApi.Features.Education.GetStudentSubjectList;

public record GetStudentSubjectListRequest(int StudentId) : IRequest<IList<Subject>>;