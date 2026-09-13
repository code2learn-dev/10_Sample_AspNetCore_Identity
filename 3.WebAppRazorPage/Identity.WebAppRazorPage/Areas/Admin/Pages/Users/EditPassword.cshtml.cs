
namespace Identity.WebAppRazorPage.Areas.Admin.Pages.Users
{
	public class EditPasswordModel : BaseUserPageModel
	{
		public EditPasswordModel(
			IUserService userService,
			IMapper mapper,
			IRoleService roleService) : base(userService, mapper, roleService)
		{
		}

		[BindProperty]
		public UpdatePasswordViewModel PasswordModel { get; set; }

		public async Task<IActionResult> OnGetAsync(string? id)
		{
			ApplicationServiceResult<DeleteUserDtoModel?> userResult = await _userService.FindDeleteUserByIdAsync(id);
			if (!userResult.IsSuccess)
			{
				userResult.Errors.MapErrorMessages();
				return RedirectToPage(IndexPage);
			}

			PasswordModel = _mapper.Map<UpdatePasswordViewModel>(userResult.Result);
			PasswordModel.RoleSelectListItem = PrepareRolesList();
			PasswordModel.UserImageUrl = $"{UserImagesUrl}/{PasswordModel.Image}";
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				ModelState.MapModelErrors();
				PasswordModel.RoleSelectListItem = PrepareRolesList(); 
				return Page();
			}

            ApplicationServiceResult<UserDtoModel?> editResult = 
				await _userService.ChangePasswordAsync(_mapper.Map<ChangePasswordDtoModel>(PasswordModel));
			if(editResult.IsSuccess)
			{
				editResult.Messages.MappMessages();
				return RedirectToPage(IndexPage);
			}

			editResult.Errors.MapErrorMessages();
			PasswordModel.RoleSelectListItem = PrepareRolesList(); 
			return Page();
		}
	}
}
