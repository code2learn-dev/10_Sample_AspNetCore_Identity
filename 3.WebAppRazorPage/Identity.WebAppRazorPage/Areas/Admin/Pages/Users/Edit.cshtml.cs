
namespace Identity.WebAppRazorPage.Areas.Admin.Pages.Users
{
	public class EditModel : BaseUserPageModel
	{
		private readonly IWebHostEnvironment _webHost;

		public EditModel(
			IUserService userService,
			IMapper mapper,
			IRoleService roleService,
			IWebHostEnvironment webHost) : base(userService, mapper, roleService)
		{
			_webHost = webHost;
		}

		[BindProperty]
		public UpdateUserViewModel UserModel { get; set; }

		public async Task<IActionResult> OnGetAsync(string? id)
		{
			ApplicationServiceResult<UpdateUserDtoModel?> userResult = await _userService.FindUpdateUserByIdAsync(id ?? "");
			if (!userResult.IsSuccess)
			{
				userResult.Errors.MapErrorMessages();
				return RedirectToPage(IndexPage);
			}

			UserModel = _mapper.Map<UpdateUserViewModel>(userResult.Result);
			UserModel.RoleSelectListItem = PrepareRolesList();
			UserModel.UserImageUrl = $"{UserImagesUrl}/{UserModel.Image}";
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				ModelState.MapModelErrors();
				UserModel.RoleSelectListItem = PrepareRolesList();
				UserModel.UserImageUrl = $"{UserImagesUrl}/{UserModel.Image}";
				return Page();
			}

			if (UserModel.File is not null && UserModel.File.Length > 0)
			{
				Dictionary<string, string> fileState = await UserModel.File.EditImageAsync(
													_webHost,
													UserImageDirName,
													UserModel.Image,
													$"{UserModel.FirstName} {UserModel.LastName}");
				if (fileState.TryGetValue("error", out string? error) && !string.IsNullOrEmpty(error))
				{
					MessageHelperExtensions.SetErrorMessage(error);
					UserModel.RoleSelectListItem = PrepareRolesList();
					UserModel.UserImageUrl = $"{UserImagesUrl}/{UserModel.Image}";
					return Page();
				}

				UserModel.Image = fileState["filename"];
			}

            ApplicationServiceResult<UserDtoModel?> updateResult = 
				await _userService.UpdateUserAsync(_mapper.Map<UpdateUserDtoModel>(UserModel));
			if(updateResult.IsSuccess)
			{
				updateResult.Messages.MappMessages();
				return RedirectToPage(IndexPage);
			}

			updateResult.Errors.MapErrorMessages();
			UserModel.RoleSelectListItem = PrepareRolesList();
			UserModel.UserImageUrl = $"{UserImagesUrl}/{UserModel.Image}";
			return Page();
		}
	}
}
