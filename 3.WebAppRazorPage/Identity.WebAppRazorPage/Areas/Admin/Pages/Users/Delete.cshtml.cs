
namespace Identity.WebAppRazorPage.Areas.Admin.Pages.Users
{
    public class DeleteModel : BaseUserPageModel
    {
        private readonly IWebHostEnvironment _webHost;

        public DeleteModel(
            IUserService userService,
            IMapper mapper,
            IRoleService roleService,
            IWebHostEnvironment webHost) : base(userService, mapper, roleService)
        {
            _webHost = webHost;
        }

        [BindProperty]
        public DeleteUserViewModel UserModel { get; set; }

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            ApplicationServiceResult<DeleteUserDtoModel?> userResult = await _userService.FindDeleteUserByIdAsync(id);
            if(!userResult.IsSuccess)
            {
                userResult.Errors.MapErrorMessages();
                return RedirectToPage(IndexPage);
            }

            UserModel = _mapper.Map<DeleteUserViewModel>(userResult.Result);
            UserModel.RoleSelectListItem = PrepareRolesList();
            UserModel.UserImageUrl = $"{UserImagesUrl}/{UserModel.Image}";
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!string.IsNullOrEmpty(UserModel.Image))
                UserModel.Image.DeleteImage(_webHost, UserImageDirName);

            ApplicationServiceResult<UserDtoModel?> deleteResult = await _userService.DeleteUserAsync(UserModel.Id);
            if(deleteResult.IsSuccess)
            {
                deleteResult.Messages.MappMessages();
                return RedirectToPage(IndexPage);
            }

            deleteResult.Errors.MapErrorMessages();
            UserModel.RoleSelectListItem = PrepareRolesList();
			UserModel.UserImageUrl = $"{UserImagesUrl}/{UserModel.Image}";
			return Page();
        }
    }
}
