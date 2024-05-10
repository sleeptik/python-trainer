using System.Text.Json;
using Infrastructure.ChatBot.Functions;
using Infrastructure.ChatBot.Messages;
using Infrastructure.ChatBot.ResultModels;
using OpenAI.Interfaces;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.RequestModels;

namespace Infrastructure.ChatBot;

public class SolutionVerifyingService(ApplicationDbContext context, IOpenAIService completionService)
{
    private static readonly string GptModel = Models.Gpt_3_5_Turbo_1106;

    private static readonly JsonSerializerOptions JsonSerializerOptions =
        new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public async Task<VerificationResult> VerifyAsync(int assignmentId, CancellationToken cancellationToken)
    {
        var request = new ChatCompletionCreateRequest
        {
            Messages = new List<ChatMessage>
            {
                InstructionMessageFactory.Create(context, 1), // TODO
                RequestMessageFactory.Create(context, 1) // TODO 
            },
            Functions = new List<FunctionDefinition> { VerificationFunctionFactory.Create() }
        };

        var response = await completionService.ChatCompletion.CreateCompletion(request, GptModel, cancellationToken);

        VerificationNotSuccessfulException.ThrowIfNotSuccessful(response);

        var json = response.Choices.First().Message.FunctionCall?.Arguments!;
        var verificationObject = JsonSerializer.Deserialize<VerificationResult>(json, JsonSerializerOptions)!;

        return verificationObject;
    }
}