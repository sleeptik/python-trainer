using Domain.Trainer;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Exercises;

public class GetExerciseHandler(ApplicationDbContext context) : IRequestHandler<GetExerciseRequest, IList<Exercise>>
{
    public async Task<IList<Exercise>> Handle(GetExerciseRequest request, CancellationToken cancellationToken)
    {
        return await context.Exercises
            .Where(exercise => exercise.DifficultyId == request.DifficultyId)
            .Where(exercise => exercise.Subjects.Any(subject => subject.Id == request.SubjectId))
            .ToListAsync(cancellationToken: cancellationToken);
    }
}