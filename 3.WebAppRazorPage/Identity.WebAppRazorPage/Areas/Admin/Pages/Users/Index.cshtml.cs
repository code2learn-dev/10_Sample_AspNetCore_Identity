namespace Identity.WebAppRazorPage.Areas.Admin.Pages.Users
{
    public class IndexModel : BaseUserPageModel
    {
        public IndexModel(
            IUserService userService, 
            IMapper mapper,
            IRoleService roleService) 
            : base(userService, mapper, roleService)
        {
        }

        public IReadOnlyCollection<UserViewModel> Users { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ApplicationServiceResult<IReadOnlyCollection<UserDtoModel>> appResult = await _userService.ReadUsersAsync();
            if(!appResult.IsSuccess)
            {
                appResult.Errors.MapErrorMessages();
                return RedirectToPage("/Admin/Dashboard");
            }

            Users = _mapper.Map<IReadOnlyCollection<UserViewModel>>(appResult.Result);
            return Page();
        }
    }
}
