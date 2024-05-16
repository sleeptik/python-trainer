using System.Text.Json;
using OpenAI.Interfaces;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.RequestModels;
using Trainer.Verification.ChatBot.Messages;
using Trainer.Verification.ChatBot.ResultModels;
using Trainer.Verification.ChatBot.Tools;
using Trainer.Verification.Data;

namespace Trainer.Verification.ChatBot;

public class AiVerificationService(IOpenAIService completionService)
{
    private static readonly string GptModel = Models.Gpt_3_5_Turbo_1106;

    private static readonly JsonSerializerOptions JsonSerializerOptions =
        new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public async Task<VerificationResult> VerifyAsync(
        VerificationInstructionsSet instructions, CancellationToken cancellationToken
    )
    {
        var tool = VerificationToolFactory.Create();

        var request = new ChatCompletionCreateRequest
        {
            Messages = new List<ChatMessage>
            {
                InstructionMessageFactory.Create(instructions.CustomInstructions),
                RequestMessageFactory.Create(instructions.Task, instructions.Solution)
            },
            Tools = new List<ToolDefinition>
            {
                tool.Tool
            },
            ToolChoice = tool.Choice
        };

        var response = await completionService.ChatCompletion.CreateCompletion(request, GptModel, cancellationToken);

        VerificationNotSuccessfulException.ThrowIfNotSuccessful(response);

        var json = response.Choices.First().Message.FunctionCall?.Arguments!;
        var verificationObject = JsonSerializer.Deserialize<VerificationResult>(json, JsonSerializerOptions)!;

        return verificationObject;
    }
}