using Identity.ApplicationService.Categories.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Identity.WebAppRazorPage.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryService _service;

        public IndexModel(ICategoryService service)
        {
            _service = service;
        }

        public void OnGet()
        {
           
        }
    }
}
