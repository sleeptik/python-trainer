using System.Text;
using OpenAI.ObjectModels.RequestModels;

namespace Trainer.Verification.ChatBot.Messages;

public class AssessmentMessageFactory
{
    public static ChatMessage Create()
    {
        var message = new StringBuilder()
            .Append("Evaluate the code solution for exercise on these six criteria from 1 to 10: readability, complexity, creativity, efficiency, structure, logic. Here's what each of the criteria means:")
            .AppendLine("readability: This criterion evaluates how easy the code is to read and understand. Readability is related to the use of clear and informative variable names (CamelCase, snake_case, etc.) and functions, good code formatting (indentation, spaces, line breaks), and comments that explain what each part of the code does;")
            .AppendLine("complexity: This criterion is responsible for how difficult the code is written, maybe it was possible to shorten some part of the code, convert some lines into one. (the higher the score, the easier the code is written);")
            .AppendLine("creativity: This criterion assesses the user's ability to solve a problem using non-standard or inventive approaches. It includes the ability to apply creative solution methods as well as the use of different programming patterns;")
            .AppendLine("efficiency: This criterion refers to how well and how fast the code executes. This includes using optimal algorithms and data structures to achieve a goal, minimising execution time and resource usage (such as memory or CPU time);")
            .AppendLine("structure: This criterion evaluates the organisation and structure of the code. It includes the use of modularity and the division of code into logical blocks, compliance with DRY (Don't Repeat Yourself) principles, and the presence of a clear plan or algorithm for solving the problem;")
            .AppendLine("logic: This criterion evaluates the logical consistency and correctness of algorithms and solutions. It takes into account the correctness of logical operations, exception handling, condition checking and the overall logic of programme execution.");

        return ChatMessage.FromSystem(message.ToString());
    }
}