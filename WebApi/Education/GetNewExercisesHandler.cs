using AutoMapper;
using Domain.Trainer;
using Domain.Users;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Education;

public class GetNewExercisesHandler(ApplicationDbContext context, IMapper mapper, HistoryService historyService, UserRankService userRankService)
    : IRequestHandler<GetNewExercisesRequest, IList<GetNewExercisesResponse>>
{
    public Task<IList<GetNewExercisesResponse>> Handle(GetNewExercisesRequest request,
        CancellationToken cancellationToken)
    {
        var history = historyService.GetUserHistory(1);
        List<Exercise> exercises;
        IList<GetNewExercisesResponse> responses;
        if (history.Count == 0)
        {
            exercises = new List<Exercise>();
            exercises.AddRange(context.Exercises.AsNoTracking()
                .Include(exercise => exercise.Difficulty)
                .Include(exercise => exercise.Subjects)
                .Where(exercise => exercise.Subjects.Any(subject => subject.Id == request.SubjectId))
                .ToList().Take(1));
            responses = mapper.Map<IList<GetNewExercisesResponse>>(exercises);

            return Task.FromResult(responses);
        }

        var currentExercise = history[0].Exercise;
        var userRank = userRankService.GetUserRank(1);


        var subjectExercises = context.Exercises.AsNoTracking()
            .Include(exercise => exercise.Difficulty)
            .Include(exercise => exercise.Subjects)
            .Where(exercise => exercise.DifficultyId != 3)
            .Where(exercise => exercise.Subjects.Any(subject => currentExercise.Subjects.Any(s => s.Id == subject.Id)))
            .ToList();

        subjectExercises.AddRange(context.Exercises.AsNoTracking()
            .Include(exercise => exercise.Difficulty)
            .Include(exercise => exercise.Subjects)
            .Where(exercise => exercise.DifficultyId == 3)
            .Where(exercise => currentExercise.Subjects.All(subject => exercise.Subjects.Contains(subject))));

        subjectExercises = subjectExercises
            .Where(exercise => !history.Any(his => his.ExerciseId == exercise.Id))
            .ToList()
            .OrderBy(_ => Random.Shared.Next())
            .ToList();
        

        exercises = new List<Exercise>();
        if (history[^1].IsPassed)
        {
            userRank.Metric += currentExercise.DifficultyId * 1.5f;
            if (userRank.Metric >= 10f) userRank.AssignedDifficultyId = 2;
            else if (userRank.Metric >= 16f) userRank.AssignedDifficultyId = userRank.AssignedDifficultyId = 3;
        }
        else
        {
            userRank.Metric -= currentExercise.DifficultyId * 1.5f;
            if (userRank.Metric < 8.5F) userRank.AssignedDifficultyId = 1;
            else if (userRank.Metric < 14.5f) userRank.AssignedDifficultyId = 2;
        }
        List<string>? teor;
        if (userRank.Metric < 4)
        {
            teor = new List<string>(){ "Theory"};
            exercises.AddRange(subjectExercises.Where(exercise => exercise.DifficultyId == 1).Take(1));
        }
        else
        {
            exercises.AddRange(subjectExercises.Where(exercise => exercise.DifficultyId == userRank.AssignedDifficultyId).Take(1));
        }
        

        responses = mapper.Map<IList<GetNewExercisesResponse>>(exercises);

        return Task.FromResult(responses);
    }
}