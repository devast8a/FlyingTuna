using FlyingTuna.MPI;

namespace FlyingTuna.Additions.Messages
{
    public class TextError : ErrorMessage
    {
        public TextError(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public TextError()
        {
            ErrorMessage = "General Error";
        }
        
        public string ErrorMessage;
    }
}