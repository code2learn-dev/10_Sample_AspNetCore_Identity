using Identity.WebAppRazorPage.Pages.Common;
using Microsoft.AspNetCore.Authorization;

namespace Identity.WebAppRazorPage.Pages.Default
{
    [AllowAnonymous]
    public class IndexModel : BasePageViewModel
    {
        private readonly ICourseService _courseService;

        public IndexModel(ICourseService courseService, IMapper mapper) : base(mapper) 
        {
            _courseService = courseService;
        }

        public IReadOnlyCollection<CourseViewModel> Courses { get; set; }

        public string BaseImageUrl => "https://localhost:7286/assets/images/courses/";

        public async Task<IActionResult> OnGetAsync()
        {
            var appResult = await _courseService.GetAllEntityDtosAsync();
            if(!appResult.IsSuccess)
            {
                appResult.Errors.MapErrorMessages();
                return RedirectToPage("/Errors/NotFound");
            }

            Courses = _mapper.Map<IReadOnlyCollection<CourseViewModel>>(appResult.Result);

			return Page();
        }
    }
}
