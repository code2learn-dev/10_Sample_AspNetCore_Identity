
using Identity.ApplicationService.Courses.Services;
using Identity.WebAppRazorPage.ViewModels.Courses;

namespace Identity.WebAppRazorPage.Areas.Admin.Pages.Courses
{
    public class IndexModel : BasePage
    {
        private readonly ICourseService _courseService;

        public IndexModel(IMapper mapper, ICourseService courseService) : base(mapper)
        {
            _courseService = courseService;
        }

        public IReadOnlyCollection<CourseViewModel> Courses{ get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var appResult = await _courseService.GetAllEntityDtosAsync("خطا در فرخوانی لیست دوره های آموزشی");
            if(!appResult.IsSuccess)
            {
                appResult.Errors.MapErrorMessages();
                return RedirectToPage("/Admin/Dashboard/Index");
            }

            Courses = _mapper.Map<IReadOnlyCollection<CourseViewModel>>(appResult.Result);
            return Page();
        }
    }
}
