using OpenAI.Managers;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.RequestModels;

namespace Infrastructure.ChatBot;

public class SolutionVerifyingService(OpenAIService openAiService)
{
    public async Task<string> SendExampleRequestAsync()
    {
        openAiService.SetDefaultModelId(Models.Gpt_3_5_Turbo_1106);

        var request = new ChatCompletionCreateRequest
        {
            Messages = new List<ChatMessage>
            {
                // ChatMessage.FromSystem(""),
                ChatMessage.FromUser("Say hello")
            }
        };

        var response = await openAiService.CreateCompletion(request);

        if (!response.Successful) throw new Exception();

        return response.Choices.First().Message.Content;
    }
}