namespace Identity.WebAppRazorPage.Areas.Admin.Pages.Categories
{
    public class EditModel : BasePage
    {
        private readonly ICategoryService _categoryService;

        public EditModel(IMapper mapper, ICategoryService categoryService) : base(mapper)
        {
            _categoryService = categoryService;
        }

        [BindProperty]
        public CrudCategoryViewModel CategoryModel { get; set; }

        public async Task<IActionResult> OnGet(long? id)
        {
            var appResult = await _categoryService.FindByIdUpdateEntityDtoAsync(id);  
            if(!appResult.IsSuccess)
            {
                appResult.Errors.MappMessages(Alert.danger);
                return RedirectToPage("Index");
            }

            CategoryModel = _mapper.Map<CrudCategoryViewModel>(appResult.Result);
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                ModelState.MapModelErrors();
                return Page();
            }

            var appResult = await _categoryService.UpdateEntityDtoAsync(_mapper.Map<CrudCategoryDto>(CategoryModel));
            if(appResult.IsSuccess)
            {
                appResult.Messages.MappMessages();
                return RedirectToPage("Index");
            }

            appResult.Messages.MappMessages(Alert.danger);
            return Page();
        }
    }
}
