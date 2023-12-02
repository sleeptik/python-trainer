using AutoMapper;
using Domain.Trainer;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Features.Education.Requests;
using WebApi.Features.Education.Responses;

namespace WebApi.Features.Education.Handlers;

public class GetNewExerciseHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetNewExerciseRequest, GetNewExerciseResponse>
{
    public async Task<GetNewExerciseResponse> Handle(GetNewExerciseRequest request,
        CancellationToken cancellationToken)
    {
        var history = await GetStudentFinishedAssignments(1, cancellationToken);
        var newExercise = new Exercise();
        GetNewExerciseResponse response;
        if (history.Count == 0)
        {
            newExercise = context.Exercises
                .AsNoTracking()
                .Include(exercise => exercise.Rank)
                .Include(exercise => exercise.Subjects)
                .First(exercise => exercise.Subjects.Any(subject => subject.Id == request.SubjectId));
            response = mapper.Map<GetNewExerciseResponse>(newExercise);

            return response;
        }

        var currentExercise = history[0].Exercise;
        var userRank = context.Students.AsNoTracking().First();


        var subjectExercises = context.Exercises.AsNoTracking()
            .Include(exercise => exercise.Rank)
            .Include(exercise => exercise.Subjects)
            .Where(exercise => exercise.RankId != 3)
            .AsEnumerable()
            .Where(exercise => exercise.Subjects.Any(subject => currentExercise.Subjects.Any(s => s.Id == subject.Id)))
            .ToList();

        subjectExercises.AddRange(context.Exercises.AsNoTracking()
            .Include(exercise => exercise.Rank)
            .Include(exercise => exercise.Subjects)
            .Where(exercise => exercise.RankId == 3)
            .AsEnumerable()
            .Where(exercise =>
                currentExercise.Subjects.All(subject => exercise.Subjects.Any(s => s.Id == subject.Id))));

        subjectExercises = subjectExercises
            .Where(exercise => !history.Any(his => his.ExerciseId == exercise.Id))
            .ToList()
            .OrderBy(_ => Random.Shared.Next())
            .ToList();


        List<string>? teor;
        if (userRank.Score < 4)
        {
            teor = new List<string> { "Theory" };
            newExercise = subjectExercises
                .First(exercise => exercise.RankId == userRank.CurrentRankId);
        }
        else
        {
            newExercise = subjectExercises
                .First(exercise => exercise.RankId == userRank.CurrentRankId);
        }


        response = mapper.Map<GetNewExerciseResponse>(newExercise);

        return response;
    }

    private async Task<IList<Assignment>> GetStudentFinishedAssignments(
        int studentId, CancellationToken cancellationToken
    )
    {
        return await context.Assignments.AsNoTracking()
            .Include(assignment => assignment.Exercise)
            .ThenInclude(exercise => exercise.Subjects)
            .Where(assignment => assignment.IsPassed != null)
            .Where(assignment => assignment.StudentId == studentId)
            .OrderBy(assignment => assignment.FinishedAt)
            .ToListAsync(cancellationToken);
    }
}