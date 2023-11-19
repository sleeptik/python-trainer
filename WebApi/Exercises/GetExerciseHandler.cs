using Domain.Trainer;
using Infrastructure;
using MediatR;

namespace WebApi.Exercises;

public class GetExerciseHandler(ApplicationDbContext context) : IRequestHandler<GetExerciseRequest,IList<Exercise>>
{
    public Task<IList<Exercise>> Handle(GetExerciseRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}