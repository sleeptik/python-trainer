using OpenAI.Builders;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels.SharedModels;
using Trainer.Verification.ChatBot.ResultModels;

namespace Trainer.Verification.ChatBot.Functions;

public static class VerificationFunctionFactory
{
    public static FunctionDefinition Create()
    {
        return new FunctionDefinitionBuilder(
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
    }
}