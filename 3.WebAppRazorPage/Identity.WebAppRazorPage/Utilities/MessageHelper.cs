namespace Identity.WebAppRazorPage.Utilities
{
    public class MessageHelper
    {
        private static List<string> _messages;

        static MessageHelper() => _messages = [];

        public static IReadOnlyList<string> Messages => _messages;

        public void AddMessage(string? message)
        {
            if (string.IsNullOrWhiteSpace(message)) return;

            _messages.Add(message);
        }

        public static void ClearMessages() => _messages.Clear();
    }

    public enum Alert : byte
    {
        success = 1,
        danger,
        warn,
        info
    }
}
