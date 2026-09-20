namespace Identity.ApplicationService.Account.Services
{
	public class AccountService : IAccountService
	{
		private readonly IMapper _mapper;
		private readonly UserManager<AcademyUser> _userManager;
		private readonly SignInManager<AcademyUser> _signInManager;
		private readonly RoleManager<AcademyRole> _roleManager;
		private readonly IHttpContextAccessor _contextAccessor;
		private readonly IAccountModelValidator _validator;
		private readonly IAccountMessageMaker _messageMaker;
		private readonly IAccountResponse _accountResponse;
		private readonly ILogger<UserService> _logger;

		public AccountService(
			IMapper mapper,
			SignInManager<AcademyUser> signInManager,
			UserManager<AcademyUser> userManager,
			RoleManager<AcademyRole> roleManager,
			IAccountModelValidator validator,
			IAccountMessageMaker messageMaker,
			IAccountResponse accountResponse,
			ILogger<UserService> logger,
			IHttpContextAccessor contextAccessor)
		{
			_mapper = mapper;
			_signInManager = signInManager;
			_userManager = userManager;
			_roleManager = roleManager;
			_validator = validator;
			_messageMaker = messageMaker;
			_accountResponse = accountResponse;
			_logger = logger;
			_contextAccessor = contextAccessor;
		}



		public async Task<ApplicationServiceResult<AccountDtoModel?>> LoginAccountAsync(LoginDtoModel model)
		{
			(ApplicationServiceResult<AccountDtoModel?> accountResult, AcademyUser? user) = await AccountSignInResult(model);
			if (!accountResult.IsSuccess || user is null) return accountResult;

			return await SetSignInResultAsync(user, model, AccountRole.member);
		}

		public async Task<ApplicationServiceResult<AccountDtoModel?>> SignInManagerAdminAsync(LoginDtoModel model)
		{
			(ApplicationServiceResult<AccountDtoModel?> accountResult, AcademyUser? user) = await AccountSignInResult(model);
			if (!accountResult.IsSuccess || user is null) return accountResult;

			return await SetSignInResultAsync(user, model, AccountRole.admin);
		}


		public async Task<ApplicationServiceResult<AccountDtoModel?>> RegisterAccountAsync(RegisterDtoModel model)
		{
			ApplicationServiceResult<AccountDtoModel?> accountResult = _accountResponse.GetAccountResult();

			ValidationResult validationResult = await _validator.ValidateModelAsync(model);
			if (!validationResult.IsValid)
			{
				List<string> errors = validationResult.GetValidationResultErrors();
				accountResult.AddErrorsList(errors.ToArray());
				return accountResult;
			}

			AcademyUser? user = await _userManager.FindByNameAsync(model.UserName);
			if (user is not null)
			{
				accountResult.AddError("نام کاربری قبلا ثبت شده است");
				return accountResult;
			}

			user = await _userManager.FindByEmailAsync(model.Email);
			if (user is not null)
			{
				accountResult.AddError("آدرس ایمیل قبلا ثبت شده است");
				return accountResult;
			}

			user = _mapper.Map<AcademyUser>(model);
			IdentityResult result = await _userManager.CreateAsync(user);
			if (!result.Succeeded)
			{
				result.LoggUserIdentityErrors(_logger);
				List<string> errors = result.GetIdentityErrors();
				accountResult.AddErrorsList(errors.ToArray());
				return accountResult;
			}

			_messageMaker.SetMessage(CrudAccount.register, AccountStatus.ok);
			accountResult.AddMessage(_messageMaker.Message);
			accountResult.AddResult(_mapper.Map<AccountDtoModel>(user));
			return accountResult;
		}


		private async Task<(ApplicationServiceResult<AccountDtoModel?>, AcademyUser?)> AccountSignInResult(LoginDtoModel model)
		{
			ApplicationServiceResult<AccountDtoModel?> accountResult = _accountResponse.GetAccountResult();
			ValidationResult validationResult = await _validator.ValidateModelAsync(model);
			if (!validationResult.IsValid)
			{
				List<string> modelErrors = validationResult.GetValidationResultErrors();
				accountResult.AddErrorsList(modelErrors.ToArray());
				return (accountResult, null);
			}

			AcademyUser? user = await _userManager.FindByNameAsync(model.UserName);
			if (user is null)
			{
				_messageMaker.SetMessage(CrudAccount.login, AccountStatus.notfound);
				accountResult.AddError(_messageMaker.Message);
				return (accountResult, null);
			}

			return (accountResult, user);
		}


		public async Task<ApplicationServiceResult<AccountProfileDtoModel?>> GetAccountProfileInfoAsync()
		{
			ApplicationServiceResult<AccountProfileDtoModel?> userResult = new();

			var currentUser = _contextAccessor.HttpContext?.User;
			if (currentUser is null || !currentUser.Identity?.IsAuthenticated == true)
			{
				userResult.AddError("کاربری یافت نشد");
				return userResult;
			}
			AcademyUser? user = await _userManager.GetUserAsync(currentUser);
			if (user is null)
			{
				userResult.AddError("کاربری یافت نشد");
				return userResult;
			}

			var accountProfile = _mapper.Map<AccountProfileDtoModel>(user);
			string? roleName = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
			AcademyRole? role = await _roleManager.FindByNameAsync(roleName ?? "");
			accountProfile.RoleId = role?.Id ?? string.Empty;

			userResult.AddResult(accountProfile);
			return userResult;
		}


		public async Task<ApplicationServiceResult<AccountDtoModel?>> 
			LoginAccountToGenerateTokenAsync(
			LoginDtoModel model, 
			AccountRole accountRole = AccountRole.member)
		{
			ApplicationServiceResult<AccountDtoModel?> loginResult = new();

			ValidationResult validationResult = await _validator.ValidateModelAsync(model);
			if (!validationResult.IsValid)
			{
				List<string> errors = validationResult.GetValidationResultErrors();
				loginResult.AddErrorsList(errors.ToArray());
				return loginResult;
			}

			AcademyUser? user = await _userManager.FindByNameAsync(model.UserName);
			if (user is null)
			{
				_messageMaker.SetMessage(CrudAccount.login, AccountStatus.notfound);
				loginResult.AddError(_messageMaker.Message);
				return loginResult;
			}

			if (!await _userManager.CheckPasswordAsync(user, model.Password))
			{
				_messageMaker.SetMessage(CrudAccount.login, AccountStatus.notfound);
				loginResult.AddError(_messageMaker.Message);
				return loginResult;
			}

			if (await _userManager.IsInRoleAsync(user, accountRole.ToString()))
			{
				loginResult.AddResult(_mapper.Map<AccountDtoModel>(user));
			}
			else
			{
				_messageMaker.SetMessage(CrudAccount.login, AccountStatus.notfound);
				loginResult.AddError(_messageMaker.Message);
			}

			return loginResult;
		}


		private async Task<ApplicationServiceResult<AccountDtoModel?>> SetSignInResultAsync(
			AcademyUser user,
			LoginDtoModel model,
			AccountRole role)
		{
			var accountResult = _accountResponse.GetAccountResult();

			if (!await _userManager.CheckPasswordAsync(user, model.Password))
			{
				_messageMaker.SetMessage(CrudAccount.login, AccountStatus.notfound);
				accountResult.AddError(_messageMaker.Message);
				return accountResult;
			}

			if (!await _userManager.IsInRoleAsync(user, role.ToString()))
			{
				_messageMaker.SetMessage(CrudAccount.login, AccountStatus.notfound);
				accountResult.AddError(_messageMaker.Message);
				return accountResult;
			}

			await _signInManager.SignInAsync(user, model.RememberMe);
			await _signInManager.RefreshSignInAsync(user);

			_messageMaker.SetMessage(CrudAccount.login, AccountStatus.ok);
			accountResult.AddMessage(_messageMaker.Message);
			accountResult.AddResult(_mapper.Map<AccountDtoModel>(user));
			return accountResult;
		}
	}
}
