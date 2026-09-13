
namespace Identity.WebAppRazorPage.Areas.Admin.Pages.Users
{
    public class AddModel : BaseUserPageModel
    {
        private readonly IWebHostEnvironment _webHost;

        public AddModel(
            IUserService userService,
            IMapper mapper,
            IRoleService roleService,
            IWebHostEnvironment webHost)
            : base(userService, mapper, roleService)
        {
            _webHost = webHost;
        }

        [BindProperty]
        public CreateUserViewModel UserModel { get; set; }

        public void OnGet()
        {
            UserModel = new()
            {
                RoleSelectListItem = PrepareRolesList()
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                ModelState.MapModelErrors();
                UserModel.RoleSelectListItem = PrepareRolesList();
            }

            if (UserModel.File is not null && UserModel.File.Length > 0)
            {
                Dictionary<string, string> fileState = await UserModel.File.UploadImgeAsync(_webHost, UserImageDirName, $"{UserModel.FirstName} {UserModel.LastName}"); 
                if (fileState.TryGetValue("error", out string? error) && !string.IsNullOrEmpty(error))
                {
                    MessageHelperExtensions.SetErrorMessage(error);
                    UserModel.RoleSelectListItem = PrepareRolesList();
                    return Page();
                }

                UserModel.Image = fileState["filename"];
            }

            ApplicationServiceResult<UserDtoModel?> createResult = 
                        await _userService.CreateUserAsync(_mapper.Map<CreateUserDtoModel>(UserModel));
            if(createResult.IsSuccess)
            {
                createResult.Messages.MappMessages();
                return RedirectToPage(IndexPage);
            }

            createResult.Errors.MapErrorMessages();
            UserModel.RoleSelectListItem = PrepareRolesList();
            return Page();
        }
    }
}
