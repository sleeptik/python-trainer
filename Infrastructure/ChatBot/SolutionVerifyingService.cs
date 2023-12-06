using System.Text;
using System.Text.Json;
using Domain.Trainer;
using Microsoft.EntityFrameworkCore;
using OpenAI.Interfaces;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.RequestModels;

namespace Infrastructure.ChatBot;

public class SolutionVerifyingService(ApplicationDbContext context, IOpenAIService completionService)
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
                bar(assignment.Exercise, assignment.Solution!)
            }
        };

        var response = await completionService.ChatCompletion.CreateCompletion(request, GptModel, cancellationToken);
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
            .AppendLine("Я студент изучаю питон, ты мне нужен для проверки моих решений.")
            .AppendLine("Запрос всегда в формате:")
            .AppendLine("Задача: текст задачи.")
            .AppendLine("Темы: Список тем которые подразумевались в задаче.")
            .AppendLine("Обязательные условия: Список условий которые должны выполняться в решении.")
            .AppendLine("Решение: моё решение, данной задачи, которое ты должен проверить.")
            .AppendLine()
            .AppendLine(
                "Обязательные условия всегда должны соблюдаться в решении, если они не соблюдается решение - не верно"
            )
            .AppendLine("Отвечай в формате json")
            .AppendLine("valid: true/false - верно или неверно.")
            .AppendLine("errors: array - список ошибок если не верно или если выполнены не все обязательные условия.")
            .AppendLine("suggestions: array - опциональный список подсказок. может быть добавлен вместе с errors.")
            .ToString();

        return ChatMessage.FromSystem(instructions);
    }

    private ChatMessage bar(Exercise exercise, string solution)
    {
        var subjects = exercise.Subjects.Select(subject => subject.Name).ToList();
        var themes = string.Join(',', subjects);
        var condition = string.Join(", наличие", subjects);
        // TODO
        var instructions = new StringBuilder()
            .AppendLine($"Задача: {exercise.Contents}.")
            .AppendLine($"Темы: {themes}.")
            .AppendLine($"Обязательные условия: наличие {condition}.")
            .AppendLine($"Решение: {solution}")
            .ToString();
        return ChatMessage.FromUser(instructions);
    }
}