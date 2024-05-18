using System.Text;
using OpenAI.ObjectModels.RequestModels;

namespace Trainer.Verification.ChatBot.Messages;

public static class RequestMessageFactory
{
    public static ChatMessage Create(string task, string code)
    {
        var message = new StringBuilder()
            .AppendLine("### Задача")
            .AppendLine($"{task}")
            .AppendLine("### Код задачи")
            .AppendLine("```py")
            .AppendLine($"{code}")
            .AppendLine("```")
            .ToString();

        return ChatMessage.FromUser(message);
    }
}