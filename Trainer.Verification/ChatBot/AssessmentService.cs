using System.Text.Json;
using OpenAI.Interfaces;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.RequestModels;
using Trainer.Verification.ChatBot.Messages;
using Trainer.Verification.ChatBot.ResultModels;
using Trainer.Verification.ChatBot.Tools;

namespace Trainer.Verification.ChatBot;

/// <summary>
///     Класс предоставляющий возможность оценить код студента по нескольким критериям
/// </summary>
public class AssessmentService(IOpenAIService completionService)
{
    private static readonly string GptModel = Models.Gpt_3_5_Turbo_1106;

    public async Task<AssessmentResult> AssessAsync(
        string task, string solution, CancellationToken cancellationToken = default
    )
    {
        var tool = AssessmentToolFactory.Create();

        var request = new ChatCompletionCreateRequest
        {
            Messages = new List<ChatMessage>
            {
                AssessmentMessageFactory.Create(),
                RequestMessageFactory.Create(task, solution)
            },
            Tools = new List<ToolDefinition>
            {
                tool.Tool
            },
            ToolChoice = tool.Choice
        };

        var response = await completionService.ChatCompletion.CreateCompletion(request, GptModel, cancellationToken);

        VerificationNotSuccessfulException.ThrowIfNotSuccessful(response);

        var json = response.Choices.First().Message.ToolCalls?.First().FunctionCall?.Arguments!;
        var verificationObject = JsonSerializer.Deserialize<AssessmentResult>(json)!;

        return verificationObject;
    }
}