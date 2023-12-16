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
        var subjectToStudy = GetSubjectsToStudy(1);
        var newExercise = new Exercise();

        var userRank = context.Students.AsNoTracking().First();


        List<Exercise> subjectExercises = context.Exercises.AsNoTracking()
            .Include(exercise => exercise.Rank)
            .Include(exercise => exercise.Subjects)
            .AsEnumerable()
            .Where(exercise => exercise.Subjects.All(subject => subjectToStudy.Any(s => s.Id == subject.Id)))
            .ToList();
        
        subjectExercises = subjectExercises
            .Where(exercise => !history.Any(his => his.ExerciseId == exercise.Id && his.IsPassed is true))
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

        await context.Assignments.AddAsync(new(1, newExercise.Id), cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        
        var response = mapper.Map<GetNewExerciseResponse>(newExercise);

        return response;
    }

    private async Task<IList<Assignment>> GetStudentFinishedAssignments(
        int studentId, CancellationToken cancellationToken
    )
    {
        return await context.Assignments.AsNoTracking()
            .Include(assignment => assignment.Exercise)
            .ThenInclude(exercise => exercise.Subjects)
            .Where(assignment => assignment.StudentId == studentId)
            .OrderBy(assignment => assignment.FinishedAt)
            .ToListAsync(cancellationToken);
    }

    private IList<Subject> GetSubjectsToStudy(int studentId)
    {
        return context.Students.AsNoTracking()
            .Include(student => student.SubjectsToStudy)
            .First(student => student.UserId == studentId)
            .SubjectsToStudy
            .ToList();
    }
}