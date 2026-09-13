namespace Identity.WebAppRazorPage.Areas.Admin.Pages.Categories
{
    public class IndexModel : BasePage
    {
        private readonly ICategoryService _categoryService;

        public IndexModel(ICategoryService categoryService, IMapper mapper) : base(mapper)
        {
            _categoryService = categoryService;
        }

        public IReadOnlyCollection<CategoryViewModel> Categories { get; set; }

        public async Task<IActionResult> OnGet()
        {
            ApplicationServiceResult<IReadOnlyCollection<CategoryDto>> appResult = await _categoryService.GetAllEntityDtosAsync("خطا در نمایش لیست دسته های آموزشی");
            if(!appResult.IsSuccess)
            {
                appResult.Errors.MappMessages(Alert.danger);
                return RedirectToPage("/Dashboard/Index");
            }

            Categories = _mapper.Map<IReadOnlyCollection<CategoryViewModel>>(appResult.Result);
            return Page();
        }
    }
}
