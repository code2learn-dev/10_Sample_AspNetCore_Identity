namespace Identity.WebAppRazorPage.Areas.Admin.Pages.Categories
{
    public class DeleteModel : BasePage
    {
        private readonly ICategoryService _categoryService;

        public DeleteModel(IMapper mapper, ICategoryService categoryService) : base(mapper)
        {
            _categoryService = categoryService;
        }

        [BindProperty]
        public CategoryViewModel CategoryModel { get; set; }


        public async Task<IActionResult> OnGetAsync(long? id)
        {
            var appResult = await _categoryService.FindByIdDeleteEntityDtoAsync(id);
            if(!appResult.IsSuccess)
            {
                appResult.Errors.MappMessages(Alert.danger);
                return RedirectToPage("Index");
            }

            CategoryModel = _mapper.Map<CategoryViewModel>(appResult.Result);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var deleteAppResult = await _categoryService.DeleteEntityDtoAsync(CategoryModel.Id);
            if(deleteAppResult.IsSuccess)
            {
                deleteAppResult.Messages.MappMessages();
                return RedirectToPage("Index");
            }

            deleteAppResult.Errors.MappMessages(Alert.danger);
            return Page();
        }
    }
}
