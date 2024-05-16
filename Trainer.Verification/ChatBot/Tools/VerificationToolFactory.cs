using OpenAI.Builders;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels.SharedModels;
using Trainer.Verification.ChatBot.ResultModels;

namespace Trainer.Verification.ChatBot.Tools;

public static class VerificationToolFactory
{
    public static VerificationTool Create()
    {
        var tool = CreateTool();
        var choice = CreateToolChoice(tool);
        return new VerificationTool(tool, choice);
    }

    private static ToolDefinition CreateTool()
    {
        var function = new FunctionDefinitionBuilder(
                "send_verification_result",
                "sends result of assignment verification to student"
            )
            .AddParameter(nameof(VerificationResult.IsCorrect), PropertyDefinition.DefineBoolean())
            .AddParameter(nameof(VerificationResult.Mistakes), PropertyDefinition.DefineArray(
                PropertyDefinition.DefineObject(
                    new Dictionary<string, PropertyDefinition>
                    {
                        { nameof(CodeMistake.Mistake), PropertyDefinition.DefineString() },
                        { nameof(CodeMistake.FixSuggestion), PropertyDefinition.DefineString() }
                    }, new[] { nameof(CodeMistake.Mistake) }, null, null, null)
            ))
            .Validate()
            .Build();


        return ToolDefinition.DefineFunction(function);
    }

    private static ToolChoice CreateToolChoice(ToolDefinition tool)
    {
        return ToolChoice.FunctionChoice(tool.Function!.Name);
    }
}