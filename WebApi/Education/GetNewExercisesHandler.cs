using Domain.Trainer;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Education;

public class GetNewExercisesHandler
    (ApplicationDbContext context) : IRequestHandler<GetNewExercisesRequest, IList<Exercise>>
{
    public Task<IList<Exercise>> Handle(GetNewExercisesRequest request, CancellationToken cancellationToken)
    {
        var subjectExercises = context.Exercises.AsNoTracking()
            .Include(exercise => exercise.Difficulty)
            .Include(exercise => exercise.Subjects)
            .Where(exercise => exercise.Subjects.Any(subject => subject.Id == request.SubjectId))
            .ToList()
            .OrderBy(_ => Random.Shared.Next())
            .ToList();

        var exercises = new List<Exercise>();
        exercises.AddRange(subjectExercises.Where(exercise => exercise.DifficultyId == 1).Take(3));
        exercises.AddRange(subjectExercises.Where(exercise => exercise.DifficultyId == 2).Take(2));
        exercises.AddRange(subjectExercises.Where(exercise => exercise.DifficultyId == 3).Take(1));

        return Task.FromResult<IList<Exercise>>(exercises);
    }
}