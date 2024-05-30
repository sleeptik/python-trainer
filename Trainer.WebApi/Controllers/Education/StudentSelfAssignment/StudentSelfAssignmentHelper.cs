﻿using Microsoft.EntityFrameworkCore;
using Trainer.Database;
using Trainer.Database.Entities.Assignments;
using Trainer.Database.Entities.Exercises;

namespace Trainer.WebApi.Controllers.Education.StudentSelfAssignment;

public class StudentSelfAssignmentHelper(TrainerContext context)
{
    public async Task<Assignment> SelfAssignment(StudentSelfAssignmentRequest request)
    {
        var history = await GetStudentFinishedAssignments(request.StudentId);
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
            .Where(exercise => !history.Any(his => his.ExerciseId == exercise.Id))
            .ToList()
            .OrderBy(_ => Random.Shared.Next())
            .ToList();
        
        newExercise = subjectExercises
                .First(exercise => exercise.RankId == userRank.CurrentRankId);

        var assignmentStatus = await context.AssignmentStatuses
            .Where(status => status.Name == AssignmentStatus.New).FirstAsync();
        var assignment = Assignment.Create(request.StudentId, newExercise.Id,assignmentStatus.Id);
        await context.Assignments.AddAsync(assignment);
        await context.SaveChangesAsync();
        
        await context.Entry(assignment).Reference(assignment1=> assignment1.Exercise).LoadAsync();

        return assignment;
    }

    private async Task<IList<Assignment>> GetStudentFinishedAssignments(
        int studentId
    )
    {
        return await context.Solutions.AsNoTracking()
            .Include(solution => solution.Assignment)
            .Where(solution => solution.Assignment.StudentId == studentId)
            .Where(solution => solution.Review != null)
            .Select(solution => solution.Assignment)
            .Distinct()
            .ToListAsync();

    }

    private IList<Subject> GetSubjectsToStudy(int studentId)
    {
        return context.Students.AsNoTracking()
            .Include(student => student.Subjects)
            .First(student => student.UserId == studentId)
            .Subjects
            .ToList();
    }
}