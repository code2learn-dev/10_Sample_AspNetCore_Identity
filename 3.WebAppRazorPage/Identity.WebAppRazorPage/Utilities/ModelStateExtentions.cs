using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Identity.WebAppRazorPage.Utilities
{
    public static class ModelStateExtentions
    {
        public static void MapModelErrors(this ModelStateDictionary modelState)
        {
            if (modelState.IsValid) return;

            IReadOnlyList<string> errors = [.. modelState.Values.SelectMany(a => a.Errors).Select(e => e.ErrorMessage)];
            errors.MappMessages(Alert.danger);
        }
    }
}
