using Trainer.Verification.Python.ResultModels;

namespace Trainer.Verification.Python;

public class SourceCodeCompilingService
{
    public async Task<CompilationResult> VerifyAsync(string code, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();

        // Запустить процесс компиляции
        var fileInfo = await Compile(code, cancellationToken);

        // Дождаться конца компиляции
        // Подготовить `CompilationResult` с выставленными полями
        var result = new CompilationResult(true, "");

        // удалить созданный при компиляции файл 
        Clear(fileInfo);

        return result;
    }

    private async Task<FileInfo?> Compile(string code, CancellationToken cancellationToken)
    {
        // Скомпилировать файл и если он был успешно скомпилирован переда
        return new FileInfo("");
    }

    private async void Clear(FileInfo fileInfo)
    {
        if (fileInfo.Exists)
            fileInfo.Delete();
    }
}