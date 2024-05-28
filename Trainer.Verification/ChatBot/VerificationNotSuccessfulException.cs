using OpenAI.ObjectModels.ResponseModels;

namespace Trainer.Verification.ChatBot;

public class VerificationNotSuccessfulException : Exception
{
    public Error? ResponseError { get; }

    private VerificationNotSuccessfulException(Error? responseError)
    {
        ResponseError = responseError;
    }

    public static void ThrowIfNotSuccessful(BaseResponse response)
    {
        if (response.Successful)
            return;

        throw new VerificationNotSuccessfulException(response.Error);
    }
}