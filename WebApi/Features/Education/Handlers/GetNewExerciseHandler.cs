using AutoMapper;
using Domain.Trainer;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Features.Education.Requests;
using WebApi.Features.Education.Responses;
using WebApi.Features.Education.Services;

namespace WebApi.Features.Education.Handlers;

public class GetNewExerciseHandler(ApplicationDbContext context, IMapper mapper, AssignmentService assignmentService,
        UserRankService userRankService)
    : IRequestHandler<GetNewExerciseRequest, GetNewExerciseResponse>
{
    public Task<GetNewExerciseResponse> Handle(GetNewExerciseRequest request,
        CancellationToken cancellationToken)
    {
        var history = assignmentService.GetUserHistory(1);
        var newExercise = new Exercise();
        GetNewExerciseResponse response;
        if (history.Count == 0)
        {
            newExercise = context.Exercises
                .AsNoTracking()
                .Include(exercise => exercise.Difficulty)
                .Include(exercise => exercise.Subjects)
                .First(exercise => exercise.Subjects.Any(subject => subject.Id == request.SubjectId));
            response = mapper.Map<GetNewExerciseResponse>(newExercise);

            return Task.FromResult(response);
        }

        var currentExercise = history[0].Exercise;
        var userRank = userRankService.GetUserRank(1);


        var subjectExercises = context.Exercises.AsNoTracking()
            .Include(exercise => exercise.Difficulty)
            .Include(exercise => exercise.Subjects)
            .Where(exercise => exercise.DifficultyId != 3)
            .AsEnumerable()
            .Where(exercise => exercise.Subjects.Any(subject => currentExercise.Subjects.Any(s => s.Id == subject.Id)))
            .ToList();

        subjectExercises.AddRange(context.Exercises.AsNoTracking()
            .Include(exercise => exercise.Difficulty)
            .Include(exercise => exercise.Subjects)
            .Where(exercise => exercise.DifficultyId == 3)
            .AsEnumerable()
            .Where(exercise => currentExercise.Subjects.All(subject => exercise.Subjects.Any(s => s.Id==subject.Id))));

        subjectExercises = subjectExercises
            .Where(exercise => !history.Any(his => his.ExerciseId == exercise.Id))
            .ToList()
            .OrderBy(_ => Random.Shared.Next())
            .ToList();
        

        List<string>? teor;
        if (userRank.Metric < 4)
        {
            teor = new List<string> { "Theory" };
            newExercise=subjectExercises
                .First(exercise => exercise.DifficultyId == userRank.AssignedDifficultyId);
        }
        else
        {
            newExercise=subjectExercises
                .First(exercise => exercise.DifficultyId == userRank.AssignedDifficultyId);
        }


        response = mapper.Map<GetNewExerciseResponse>(newExercise);

        return Task.FromResult(response);
    }
}