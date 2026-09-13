using AutoMapper;
using Identity.WebAppRazorPage.Common;
using Microsoft.AspNetCore.Mvc;

namespace Identity.WebAppRazorPage.Areas.Admin.Pages.Categories
{
    public class AddModel : BasePage
    {
        private readonly ICategoryService _categoryService;

        public AddModel(
            IMapper mapper, 
            ICategoryService categoryService) : base(mapper)
        {
            _categoryService = categoryService;
        }

        [BindProperty]
        public CrudCategoryViewModel CategoryModel { get; set; }

        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                ModelState.MapModelErrors();
                return Page();
            }

            var appResult = await _categoryService.AddEntityDtoAsync(_mapper.Map<CrudCategoryDto>(CategoryModel));
            if(appResult.IsSuccess)
            {
                appResult.Messages.MappMessages(Alert.success);
                return RedirectToPage("Index");
            }

            appResult.Errors.MappMessages(Alert.danger);
            return Page();
        }
    }
}
