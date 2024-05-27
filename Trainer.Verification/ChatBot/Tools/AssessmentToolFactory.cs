using OpenAI.Builders;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels.SharedModels;
using Trainer.Verification.ChatBot.ResultModels;

namespace Trainer.Verification.ChatBot.Tools;

public static class AssessmentToolFactory
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
                "send_assessment_result",
                "sends result of assessment solution to student"
            )
            .AddParameter(nameof(AssessmentResult.Readability), PropertyDefinition.DefineInteger())
            .AddParameter(nameof(AssessmentResult.Complexity), PropertyDefinition.DefineInteger())
            .AddParameter(nameof(AssessmentResult.Creativity), PropertyDefinition.DefineInteger())
            .AddParameter(nameof(AssessmentResult.Efficiency), PropertyDefinition.DefineInteger())
            .AddParameter(nameof(AssessmentResult.Structure), PropertyDefinition.DefineInteger())
            .AddParameter(nameof(AssessmentResult.Logic), PropertyDefinition.DefineInteger())
            .Validate()
            .Build();


        return ToolDefinition.DefineFunction(function);
    }
    
    private static ToolChoice CreateToolChoice(ToolDefinition tool)
    {
        return ToolChoice.FunctionChoice(tool.Function!.Name);
    }
}