using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace _10_Identity.WebApiApp.Utilities
{
    public static class ErrorMappingExtensions
    {
        public static string[] GetModelStateErros(this ModelStateDictionary modelState)
        {
            if (modelState.IsValid) return [];

            return modelState.Values.SelectMany(a => a.Errors)
                                    .Select(e => e.ErrorMessage).ToArray();
        }
    }
}
