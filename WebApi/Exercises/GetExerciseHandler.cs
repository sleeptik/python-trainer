using Infrastructure;
using MediatR;

namespace WebApi.Exercises;

public class GetExerciseHandler(ApplicationDbContext context) : IRequestHandler<GetExerciseRequest>
{
    public Task Handle(GetExerciseRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}