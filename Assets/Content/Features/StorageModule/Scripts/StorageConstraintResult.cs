namespace Content.Features.StorageModule.Scripts
{
    public class StorageConstraintResult
    {
        public bool IsValid { get; }
        public string Message { get; }

        public StorageConstraintResult(bool isValid, string message = null)
        {
            IsValid = isValid;
            Message = message;
        }
    }
}