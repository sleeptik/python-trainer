using Domain.Trainer;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Features.Education.StudentSelfAssignment;

public class StudentSelfAssignmentHandler(ApplicationDbContext context)
    : IRequestHandler<StudentSelfAssignmentRequest, Assignment>
{
    public async Task<Assignment> Handle(StudentSelfAssignmentRequest request,
        CancellationToken cancellationToken)
    {
        var history = await GetStudentFinishedAssignments(request.StudentId, cancellationToken);
        var subjectToStudy = GetSubjectsToStudy(request.StudentId);
        var newExercise = new Exercise();

        var userRank = context.Students.AsNoTracking().First();
        
        var subjectExercises = context.Exercises.AsNoTracking()
            .Include(exercise => exercise.Rank)
            .Include(exercise => exercise.Subjects)
            .AsEnumerable()
            .Where(exercise => exercise.Subjects.All(subject => subjectToStudy.Any(s => s.Id == subject.Id)))
            .ToList();
        if (request.SubjectId != 0)
        {
            subjectExercises = subjectExercises
                .Where(exercise => exercise.Subjects.Any(subject => subject.Id == request.SubjectId))
                .ToList();
        }

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

        var assignment = Assignment.Create(request.StudentId, newExercise.Id);
        await context.Assignments.AddAsync(assignment, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return assignment;
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