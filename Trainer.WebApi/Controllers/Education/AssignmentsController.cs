using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trainer.Database.Entities.Assignments;
using Trainer.Verification.InputData;
using Trainer.WebApi.Common;
using Trainer.WebApi.Controllers.Education.DTO;
using Trainer.WebApi.Controllers.Education.SetAssignmentSolution;
using Trainer.WebApi.Controllers.Education.StudentSelfAssignment;
using Trainer.WebApi.Services;

namespace Trainer.WebApi.Controllers.Education;

[Route("api/education/assignments")]
public sealed class AssignmentsController(InstantVerificationService instantVerificationService, RankService rankService) : ApiController
{
    //Контроллер отвечающий за взаимодействия с назначенными заданиями
    
    private int StudentId => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1");

    //Метод получения всех назначенных заданий для текущего пользователя
    [HttpGet("")]
    public async Task<IActionResult> GetStudentAssignments()
    {
        var assignments = await TrainerContext.Assignments.AsNoTracking()
            .Include(assignment => assignment.Exercise)
            .ThenInclude(exercise => exercise.Subjects)
            .Include(assignment => assignment.Exercise)
            .ThenInclude(exercise => exercise.Rank)
            .Include(assignment => assignment.AssignmentStatus)
            .Where(assignment => assignment.StudentId == StudentId)
            .ToListAsync();

        return Ok(assignments);
    }

    //Метод для получения деталей конкретнного задания
    [HttpGet("{assignmentId:int}")]
    public async Task<IActionResult> GetAssignmentDetails(int assignmentId)
    {
        var assignment = await TrainerContext.Assignments.AsNoTracking()
                .Include(assignment => assignment.Exercise)
                .ThenInclude(exercise => exercise.Rank)
                .Include(assignment => assignment.Exercise)
                .ThenInclude(exercise => exercise.Subjects)
                .Include(
                    assignment => assignment.Solutions
                        .OrderByDescending(solution => solution.VerifiedAt)
                        .Take(1)
                )
                .ThenInclude(solution => solution.Review)
                .ThenInclude(review => ((FaultyReview)review!).Suggestions)
                .Select(assignment => new
                    {
                        assignment.Id,
                        assignment.Exercise,
                        Solution = assignment.Solutions.FirstOrDefault(),
                        Suggestions = assignment.Solutions
                            .SelectMany(solution => ((FaultyReview)solution.Review!).Suggestions)
                            .ToList(),
                    }
                )
                .SingleAsync(assignment => assignment.Id == assignmentId);

        return Ok(
            new AssignmentDetailsDto(assignment.Id, assignment.Exercise, assignment.Solution, assignment.Suggestions)
        );
    }

    //Метод для назначения нового задания пользователю
    [HttpPost("")]
    public async Task<IActionResult> AssignYourself(StudentSelfAssignmentDto dto)
    {
        var assignmentHelper = new StudentSelfAssignmentHelper(TrainerContext);

        await assignmentHelper.SelfAssignment(
            new StudentSelfAssignmentRequest(StudentId, dto.SubjectId)
        );

        return NoContent();
    }
    
    //Метод добавления нового решения к назначенному заданию, попытка проверить решение и обновление рейтинга если проверка успешна
    [HttpPost("{assignmentId:int}/solutions")]
    public async Task<IActionResult> SetAssignmentSolution(int assignmentId, SetAssignmentSolutionDto dto)
    {
        var assignment = await TrainerContext.Assignments
            .Include(assignment1 => assignment1.Exercise)
            .ThenInclude(exercise => exercise.Subjects)
            .SingleAsync(assignment1 => assignment1.Id == assignmentId);

        var solution = Solution.Create(dto.Solution);
        var assignmentStatus = await TrainerContext.AssignmentStatuses
            .Where(status => status.Name == AssignmentStatus.Finished).FirstAsync();
        assignment.AddSolution(solution);
        assignment.SetStatus(assignmentStatus.Id);

        await TrainerContext.SaveChangesAsync();

        try
        {
            var subjectIds = solution.Assignment.Exercise.Subjects.Select(subject => subject.Id).ToList();

            var customInstructions = TrainerContext.Prompts
                .Where(prompt => subjectIds.Any(id => id == prompt.SubjectId))
                .Select(prompt => prompt.Content)
                .ToList();

            var instructions = new VerificationInstructionsSet(
                assignment.Exercise.Details, dto.Solution, customInstructions
            );

            var review = await instantVerificationService.VerifyOnceOrThrowAsync(instructions);
            if (review is FaultyReview faultyReview)
            {
                TrainerContext.Suggestions.AttachRange(faultyReview.Suggestions);
                assignmentStatus = await TrainerContext.AssignmentStatuses
                    .Where(status => status.Name == AssignmentStatus.Failed).FirstAsync();
                solution.Assignment.SetStatus(assignmentStatus.Id);
            }
            else
            {
                assignmentStatus = await TrainerContext.AssignmentStatuses
                    .Where(status => status.Name == AssignmentStatus.Verified).FirstAsync();
                solution.Assignment.SetStatus(assignmentStatus.Id);
            }
                

            TrainerContext.Reviews.Attach(review);

            solution.SetReview(review);

            await TrainerContext.SaveChangesAsync();

            var solutions = await TrainerContext.Solutions
                .Where(sol => sol.AssignmentId == solution.AssignmentId)
                .ToListAsync();

            if (solutions.Count == 1)
            {
                await new UpdateRankHelper(TrainerContext, rankService).UpdateRank(StudentId, assignment.ExerciseId);
            }
        }
        catch
        {
            // ignored
        }

        return NoContent();
    }
}