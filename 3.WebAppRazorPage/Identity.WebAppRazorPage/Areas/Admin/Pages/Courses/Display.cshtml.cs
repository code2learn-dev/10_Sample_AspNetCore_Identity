namespace Identity.WebAppRazorPage.Areas.Admin.Pages.Courses
{
    public class DisplayModel : BaseAdminCoursePage
    { 

        public DisplayModel(
            IMapper mapper,
            ICategoryService categoryService,
            ITeacherService teacherService,
            ICourseService courseService) 
            : base(mapper, categoryService, teacherService, courseService)
        { 
        }

        public DeleteCourseViewModel CourseModel { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            DeleteCourseViewModel? deleteCourseViewModel = await PrepareDeleteCourseModel(id, null);
            if (deleteCourseViewModel is null) return RedirectToPage(IndexPage);

            CourseModel = deleteCourseViewModel;
            return Page();
        }
    }
}
