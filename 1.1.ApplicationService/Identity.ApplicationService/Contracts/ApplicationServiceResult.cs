namespace Identity.ApplicationService.Contracts
{
    public abstract class ApplicationServiceResult
    {
        private List<string> _errors = [];
        private List<string> _messages = [];

        public bool IsFail => _errors.Any();
        public bool IsSuccess => !IsFail;

        public IReadOnlyList<string> Errors => _errors;
        public IReadOnlyList<string> Messages => _messages;

        public void AddError(string error) => _errors.Add(error);
        public void AddErrorsList(string[] errors) => _errors.AddRange(errors);

        public void AddMessage(string message) => _messages.Add(message);
        public void AddMessagesList(string[] messages) => _messages.AddRange(messages);
    }

    public class ApplicationServiceResult<TResult> : ApplicationServiceResult
    {
        public TResult? Result { get; private set; }

        public void AddResult(TResult result) => Result = result;
    }
}
