using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using Trainer.Verification.Python.ResultModels;

namespace Trainer.Verification.Python;

public class SourceCodeCompilingService
{
    //Сервис для проверки возмонжности скопмиплировать код решения студента
    
    private readonly ScriptEngine _engine = IronPython.Hosting.Python.CreateEngine();

    public Task<CompilationResult> CompileAsync(string code, CancellationToken cancellationToken = default)
    {
        var source = _engine.CreateScriptSourceFromString(code);

        try
        {
            source.Compile();
            return Task.FromResult(new CompilationResult(true, null));
        }
        catch (SyntaxErrorException e)
        {
            return Task.FromResult(new CompilationResult(false, e.Message));
        }
    }
}