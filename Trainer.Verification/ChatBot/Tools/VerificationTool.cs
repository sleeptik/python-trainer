using OpenAI.ObjectModels.RequestModels;

namespace Trainer.Verification.ChatBot.Tools;

public record VerificationTool(ToolDefinition Tool, ToolChoice Choice);