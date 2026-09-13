namespace Identity.ApplicationService.Users.Services
{
	public class UserService : IUserService
	{
		private readonly UserManager<AcademyUser> _userManager;
		private readonly RoleManager<AcademyRole> _roleManager;

		private readonly IUserMessageMaker _userMessageMaker;
		private readonly IUserResponse _userResponse;
		private readonly ILogger<UserService> _logger;
		private readonly IUserModelValidator _modelValidator;
		private readonly IMapper _mapper;

        public UserService(
                            UserManager<AcademyUser> userManager,
                            IUserMessageMaker userMessageMaker,
                            IUserResponse userRespose,
                            ILogger<UserService> logger,
                            IUserModelValidator modelValidator,
                            IMapper mapper,
                            RoleManager<AcademyRole> roleManager)
        {
            _userManager = userManager;
            _userMessageMaker = userMessageMaker;
            _userResponse = userRespose;
            _logger = logger;
            _modelValidator = modelValidator;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        private ApplicationServiceResult<UserDtoModel?> SetUserServiceResult(
													IdentityResult identityResult,
													ApplicationServiceResult<UserDtoModel?> appResult,
													Crud crudType,
													AcademyUser? user = null)
		{
			if (!identityResult.Succeeded)
			{
				List<string> modelErrors = [];

				identityResult.LoggUserIdentityErrors(_logger);

				_userMessageMaker.SetMessage(crudType, HttpStatusCode.BadRequest);
				modelErrors.Add(_userMessageMaker.Message);

				modelErrors.AddRange(identityResult.GetIdentityErrors());
				appResult.AddErrorsList([.. modelErrors]);
				return appResult;
			}
			else if (identityResult.Succeeded && user is null)
			{
				_userMessageMaker.SetMessage(crudType, HttpStatusCode.BadRequest);
				return appResult;
			}

			appResult.AddResult(_mapper.Map<UserDtoModel>(user));
			_userMessageMaker.SetMessage(crudType, HttpStatusCode.OK);
			appResult.AddMessage(_userMessageMaker.Message);
			return appResult;
		}

		private async Task<(ApplicationServiceResult<UserDtoModel?>, AcademyUser?)> UserExistByIdAsync(string userId)
		{
			ApplicationServiceResult<UserDtoModel?> appResult = _userResponse.GetEntityResultResponse<UserDtoModel>();

			AcademyUser? user = await _userManager.FindByIdAsync(userId);
			if (user is null)
			{
				_userMessageMaker.SetMessage(Crud.read, HttpStatusCode.NotFound);
				appResult.AddError(_userMessageMaker.Message);
			}

			return (appResult, user);
		}


		public async Task<ApplicationServiceResult<TUserDtoModel?>>
			FindCrudUserByIdAsync<TUserDtoModel>(string? userId, Crud? crudType = Crud.create) where TUserDtoModel : BaseCrudUserDtoModel
		{
			ApplicationServiceResult<TUserDtoModel?> appResult = new();

			if (string.IsNullOrWhiteSpace(userId))
			{
				_userMessageMaker.SetMessage(Crud.read, HttpStatusCode.NotFound);
				appResult.AddError(_userMessageMaker.Message);
				return appResult;
			}

			AcademyUser? user = await _userManager.FindByIdAsync(userId);
			if (user is null)
			{
				_userMessageMaker.SetMessage(Crud.read, HttpStatusCode.NotFound);
				appResult.AddError(_userMessageMaker.Message);
				return appResult;
			}

			if(crudType is Crud.update or Crud.delete)
			{
				IList<string> userRoles = await _userManager.GetRolesAsync(user); 
				AcademyRole? role = await _roleManager.FindByNameAsync(userRoles.FirstOrDefault() ?? "");
				if (role is null)
				{
					appResult.AddError("نقشی برای کاربر یافت نشد");
					return appResult;
				}

                TUserDtoModel userDtoModel = _mapper.Map<TUserDtoModel>(user);
				userDtoModel.RoleId = role.Id;
				appResult.AddResult(userDtoModel);
				return appResult;
			}
           

			appResult.AddResult(_mapper.Map<TUserDtoModel>(user));
			return appResult;
		}


		protected virtual async Task<ApplicationServiceResult<UserDtoModel?>> ValidateModelAsync(BaseCrudUserDtoModel model)
		{
			ApplicationServiceResult<UserDtoModel?> appResult = _userResponse.GetEntityResultResponse<UserDtoModel>();

			ValidationResult validationResult = await _modelValidator.ValidateModelAsync(model);
			if (!validationResult.IsValid)
			{
				List<string> modelErrors = validationResult.GetValidationResultErrors();
				appResult.AddErrorsList(modelErrors.ToArray());
			}

			return appResult;
		}

		public async Task<ApplicationServiceResult<UserDtoModel?>> CreateUserAsync(CreateUserDtoModel model)
		{
			ApplicationServiceResult<UserDtoModel?> appResult = await ValidateModelAsync(model);
			if (!appResult.IsSuccess) return appResult;

			AcademyRole? role = await _roleManager.FindByIdAsync(model.RoleId);
			if (role is null || string.IsNullOrEmpty(role.Name))
			{
				appResult.AddError("نقش انتخابی برای کاربر یافت نشد");
				return appResult;
			}

			AcademyUser user = _mapper.Map<AcademyUser>(model);
			IdentityResult identityResult = await _userManager.CreateAsync(user, model.Password);

			await _userManager.AddToRoleAsync(user, role.Name);

			return SetUserServiceResult(identityResult, appResult, Crud.create, user);
		}

		public async Task<ApplicationServiceResult<UserDtoModel?>> DeleteUserAsync(string userId)
		{
			var (appResult, user) = await UserExistByIdAsync(userId);
			if (!appResult.IsSuccess || user is null) return appResult;

			IdentityResult identityResult = await _userManager.DeleteAsync(user);
			return SetUserServiceResult(identityResult, appResult, Crud.delete, user);
		}


		public async Task<ApplicationServiceResult<DeleteUserDtoModel?>> FindDeleteUserByIdAsync(string? userId)
		{
			ApplicationServiceResult<DeleteUserDtoModel?> appResult = await FindCrudUserByIdAsync<DeleteUserDtoModel>(userId);
			return appResult;
		}

		public async Task<ApplicationServiceResult<UpdateUserDtoModel?>> FindUpdateUserByIdAsync(string? userId)
		{
			ApplicationServiceResult<UpdateUserDtoModel?> appResult = 
				await FindCrudUserByIdAsync<UpdateUserDtoModel>(userId, Crud.update); 
			return appResult;
		}

		public ApplicationServiceResult<IReadOnlyCollection<UserDtoModel>> ReadUsers()
		{
			ApplicationServiceResult<IReadOnlyCollection<UserDtoModel>> appResult = _userResponse.GetEntitiesListResultResponse<UserDtoModel>();

			try
			{
				IReadOnlyCollection<AcademyUser> users = _userManager.Users.ToList();
				appResult.AddResult(_mapper.Map<IReadOnlyCollection<UserDtoModel>>(users));
				return appResult;
			}
			catch
			{
				_userMessageMaker.SetMessage(Crud.read, HttpStatusCode.BadRequest);
				appResult.AddError(_userMessageMaker.Message);
				return appResult;
			}
		}

		public async Task<ApplicationServiceResult<IReadOnlyCollection<UserDtoModel>>> ReadUsersAsync()
		{
			ApplicationServiceResult<IReadOnlyCollection<UserDtoModel>> appResult = _userResponse.GetEntitiesListResultResponse<UserDtoModel>();

			try
			{
				IReadOnlyCollection<AcademyUser> users = await _userManager.Users.ToListAsync();
				appResult.AddResult(_mapper.Map<IReadOnlyCollection<UserDtoModel>>(users));
				return appResult;
			}
			catch
			{
				_userMessageMaker.SetMessage(Crud.read, HttpStatusCode.BadRequest);
				appResult.AddError(_userMessageMaker.Message);
				return appResult;
			}
		}

		public async Task<ApplicationServiceResult<UserDtoModel?>> UpdateUserAsync(UpdateUserDtoModel model)
		{
            var (appResult, user) = await UserExistByIdAsync(model.Id.ToString());
			if (!appResult.IsSuccess || user is null) return appResult;

            ApplicationServiceResult<UserDtoModel?> validateResult = await ValidateModelAsync(model);
			if (!validateResult.IsSuccess) return validateResult;

            AcademyRole? role = await _roleManager.FindByIdAsync(model.RoleId);
			if(role is null || string.IsNullOrEmpty(role.Name))
			{
				appResult.AddError("نقش انتخابی یافت نشد");
				return appResult;
			}

            IList<string> userRoles = await _userManager.GetRolesAsync(user);
            AcademyRole? currentRole = await _roleManager.FindByNameAsync(userRoles.FirstOrDefault() ?? "");
			if(currentRole is not null && 
				!string.IsNullOrEmpty(currentRole.Name) && 
				!currentRole.Id.Equals(model.RoleId, StringComparison.OrdinalIgnoreCase))
			{

				await _userManager.RemoveFromRoleAsync(user, currentRole?.Name ?? "");
                AcademyRole? selectedNewRole = await _roleManager.FindByIdAsync(model.RoleId);
				if (selectedNewRole is not null && !string.IsNullOrEmpty(selectedNewRole.Name))
					await _userManager.AddToRoleAsync(user, selectedNewRole.Name);
			}

			user.FirstName = model.FirstName;
			user.LastName = model.LastName;
			user.UserName = model.UserName;
			user.Email = model.Email;
			user.PhoneNumber = model.PhoneNumber;
			user.Image = model.Image ?? string.Empty;

            IdentityResult identityResult = await _userManager.UpdateAsync(user);
			return SetUserServiceResult(identityResult, appResult, Crud.update, user);
        }

		public async Task<ApplicationServiceResult<UserDtoModel?>> ChangePasswordAsync(ChangePasswordDtoModel model)
		{
			ApplicationServiceResult<UserDtoModel?> appResult = _userResponse.GetEntityResultResponse<UserDtoModel>();

            ValidationResult validationResult = await _modelValidator.ValidatePasswordAsync(model);
			if(!validationResult.IsValid)
			{
                List<string> modelErrors = validationResult.GetValidationResultErrors();
				appResult.AddErrorsList([.. modelErrors]);
				return appResult;
			}

			var (result, user) = await UserExistByIdAsync(model.Id.ToString());
			if (!result.IsSuccess || user is null) return result;

			//IdentityResult identityResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
			PasswordHasher<AcademyUser> newPassword = new();
            user.PasswordHash = newPassword.HashPassword(user, model.NewPassword);
            IdentityResult identityResult = await _userManager.UpdateAsync(user);
			if(!identityResult.Succeeded)
			{
				identityResult.LoggUserIdentityErrors(_logger);
                List<string> resultErrors = identityResult.GetIdentityErrors();
				appResult.AddError("خطا در ویرایش رمز عبور");
				appResult.AddErrorsList([.. resultErrors]);
				return appResult;
			}

			appResult.AddMessage("رمز عبور با موفقیت ویرایش گردید");
			appResult.AddResult(_mapper.Map<UserDtoModel>(user));
			return appResult;
		}
	}
}
