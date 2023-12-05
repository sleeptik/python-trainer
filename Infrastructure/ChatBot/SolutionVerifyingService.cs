using System.Text;
using System.Text.Json;
using Domain.Trainer;
using Microsoft.EntityFrameworkCore;
using OpenAI.Interfaces;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.RequestModels;

namespace Infrastructure.ChatBot;

public class SolutionVerifyingService(ApplicationDbContext context, IChatCompletionService completionService)
{
    private static readonly string GptModel = Models.Gpt_3_5_Turbo_1106;

    public async Task<VerificationResult> VerifyAsync(int studentId, int exerciseId,
        CancellationToken cancellationToken)
    {
        var assignment = await context.Assignments.AsNoTracking()
            .Include(assignment1 => assignment1.Exercise)
            .FirstAsync(
                assignment1 => assignment1.StudentId == studentId && assignment1.ExerciseId == exerciseId,
                cancellationToken
            );

        var request = new ChatCompletionCreateRequest
        {
            Messages = new List<ChatMessage>
            {
                foo(),
                bar(assignment.Exercise, "")
            }
        };

        var response = await completionService.CreateCompletion(request, GptModel, cancellationToken);
        if (!response.Successful) throw new Exception();

        var content = response.Choices.First().Message.Content;

        var verification = JsonSerializer.Deserialize<VerificationResult>(content);

        // TODO probably throw?
        return verification;
    }

    private ChatMessage foo()
    {
        // TODO
        var instructions = new StringBuilder()
            .AppendLine()
            .ToString();

        return ChatMessage.FromSystem(instructions);
    }

    private ChatMessage bar(Exercise exercise, string solution)
    {
        // TODO
        var instructions = new StringBuilder()
            .AppendLine()
            .ToString();

        return ChatMessage.FromUser(instructions);
    }
}