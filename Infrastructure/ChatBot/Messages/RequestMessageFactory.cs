using System.Text;
using OpenAI.ObjectModels.RequestModels;

namespace Infrastructure.ChatBot.Messages;

public static class RequestMessageFactory
{
    public static ChatMessage Create(int assignmentId)
    {
        var message = new StringBuilder()
            .AppendLine("### Задача")
            .AppendLine("")
            .AppendLine("### Код задачи")
            .AppendLine("```py")
            .AppendLine("")
            .AppendLine("```")
            .ToString();

        return ChatMessage.FromUser(message);
    }
}