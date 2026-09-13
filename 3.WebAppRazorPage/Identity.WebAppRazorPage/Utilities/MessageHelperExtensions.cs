namespace Identity.WebAppRazorPage.Utilities
{
    public static class MessageHelperExtensions
    {  
        public static void MappMessages(this IReadOnlyList<string>? messages, Alert alert = Alert.success)
        {
            if (messages is null || !messages.Any()) return;

            MessageHelper messageHelper = new();
            foreach (var message in messages)
            {
                string messageHtmlText = $"<p class=\"alert alert-{alert} {alert}-bg-text\">{message}</p>";
                messageHelper.AddMessage(messageHtmlText);
            }
        }

        public static void MapErrorMessages(this IReadOnlyList<string>? errorsList)
        {
            if (errorsList is null || !errorsList.Any()) return;

			MessageHelper messageHelper = new();
			foreach (var message in errorsList)
			{
				string messageHtmlText = $"<p class=\"alert alert-danger danger-bg-text\">{message}</p>";
				messageHelper.AddMessage(messageHtmlText);
			}
		}

        public static void SetErrorMessage(string? message)
        {
            if (string.IsNullOrWhiteSpace(message)) return;

            MessageHelper messageHelper = new();
            messageHelper.AddMessage($"<p class=\"alert alert-danger danger-bg-text\">{message}</p>");
        }
    }
}
