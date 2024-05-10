using OpenAI.ObjectModels.ResponseModels;

namespace Infrastructure.ChatBot;

public class VerificationNotSuccessfulException : Exception
{
    private VerificationNotSuccessfulException(Error? responseError)
    {
        throw new NotImplementedException();
    }

    public static void ThrowIfNotSuccessful(BaseResponse response)
    {
        if (response.Successful)
            return;

        throw new VerificationNotSuccessfulException(response.Error);
    }
}