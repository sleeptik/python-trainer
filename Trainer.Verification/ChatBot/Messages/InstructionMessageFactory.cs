using System.Text;
using OpenAI.ObjectModels.RequestModels;

namespace Trainer.Verification.ChatBot.Messages;

public static class InstructionMessageFactory
{
    public static ChatMessage Create(int exerciseId)
    {
        var message = new StringBuilder()
            .Append("Ты автоматизированная система для проверки кода студентов на языке Python. ")
            .AppendLine("Твоя работа - проверять код на решение поставленных целей в задаче.")
            // ---
            .Append("Тебе будут присылать сообщения содержащие описание задачи и код, ")
            .AppendLine("который необходимо будет проверить.")
            .AppendLine("Отвечай \"Верно\" или \"Неверно\".")
            // ---
            .Append("Если задача решена \"Неверно\", то следует описать допущенные студентом ошибки ")
            .AppendLine("и, по возможности, указать, как их можно исправить. ")
            // ---
            .Append("Предложенные исправления должны быть описаны идеями, а не кодом, ")
            .AppendLine("чтобы студент сам додумывал как их реализовать.")
            // ---
            .ToString();

        return ChatMessage.FromSystem(message);
    }
}