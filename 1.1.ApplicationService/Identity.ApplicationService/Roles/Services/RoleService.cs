namespace Identity.ApplicationService.Roles.Services
{
	public class RoleService : IRoleService
	{
		private readonly RoleManager<AcademyRole> _roleManager;
		private readonly IRoleMessageMaker _messageMaker;
		private readonly IRoleResponse _roleResponse;
		private readonly ILogger<RoleService> _logger;
		private readonly IRoleModelValidator _roleValidator;
		private readonly IMapper _mapper;


		public RoleService(
			RoleManager<AcademyRole> roleManager,
			IRoleMessageMaker messageMaker,
			IRoleResponse roleResponse,
			ILogger<RoleService> logger,
			IRoleModelValidator roleValidator,
			IMapper mapper)
		{
			_roleManager = roleManager;
			_messageMaker = messageMaker;
			_roleResponse = roleResponse;
			_logger = logger;
			_roleValidator = roleValidator;
			_mapper = mapper;
		}

		private ApplicationServiceResult<RoleDtoModel?> SetRoleResult(
														IdentityResult identityResult,
														ApplicationServiceResult<RoleDtoModel?> appResult,
														Crud crudType,
														AcademyRole? role = null)
		{
			if (!identityResult.Succeeded)
			{
				identityResult.LoggRoleIdentityErrors(_logger);
				List<string> errors = identityResult.GetIdentityErrors();

				_messageMaker.SetMessage(crudType, HttpStatusCode.BadRequest);
				appResult.AddError(_messageMaker.Message);
				appResult.AddErrorsList([.. errors]);
				return appResult;
			}

			if (role is not null)
				appResult.AddResult(_mapper.Map<RoleDtoModel>(role));

			_messageMaker.SetMessage(crudType, HttpStatusCode.OK);
			appResult.AddMessage(_messageMaker.Message);
			return appResult;
		}

		private async Task<(ApplicationServiceResult<TRoleDtoModel?>, AcademyRole?)> GetRoleForCrudByIdAsync<TRoleDtoModel>(
																			string? roleId,
																			Crud crud)
																	where TRoleDtoModel : RoleDtoModel
		{
			ApplicationServiceResult<TRoleDtoModel?> appResult = _roleResponse.GetRoleEntityResult<TRoleDtoModel>();

			if (string.IsNullOrWhiteSpace(roleId))
			{
				_messageMaker.SetMessage(crud, HttpStatusCode.NotFound);
				appResult.AddError(_messageMaker.Message);
				return (appResult, null);
			}

			AcademyRole? role = await _roleManager.FindByIdAsync(roleId);
			if (role is null)
			{
				_messageMaker.SetMessage(crud, HttpStatusCode.NotFound);
				appResult.AddError(_messageMaker.Message);
				return (appResult, null);
			}

			appResult.AddResult(_mapper.Map<TRoleDtoModel>(role));
			return (appResult, role);
		}



		public async Task<ApplicationServiceResult<RoleDtoModel?>> CreateAsync(CreateRoleDtoModel model)
		{
			ApplicationServiceResult<RoleDtoModel?> appResult = _roleResponse.GetRoleEntityResult<RoleDtoModel>();

			ApplicationServiceResult<RoleDtoModel?> validateResult = await ValidateRoleModelAsync(model);
			if (!validateResult.IsSuccess) return validateResult;

            AcademyRole? academyRole = await _roleManager.FindByNameAsync(model.Name);
			if(academyRole is not null)
			{
				appResult.AddError("نقشی با این نام قبلا ثبت شده است");
				return appResult;
			}

			AcademyRole role = _mapper.Map<AcademyRole>(model);
			IdentityResult identityResult = await _roleManager.CreateAsync(role);

			return SetRoleResult(identityResult, appResult, Crud.create);
		}

		public async Task<ApplicationServiceResult<RoleDtoModel?>> Delete(string? roleId)
		{
			(ApplicationServiceResult<RoleDtoModel?>? appResult, AcademyRole? role) =
									await GetRoleForCrudByIdAsync<RoleDtoModel>(roleId, Crud.delete);
			if (!appResult.IsSuccess || role is null) return appResult;

			IdentityResult identityResult = await _roleManager.DeleteAsync(role);
			return SetRoleResult(identityResult, appResult, Crud.delete);
		}

		public async Task<ApplicationServiceResult<DeleteRoleDtoModel?>> FindDeleteRoleByIdAsync(string? roleId)
		{
			(ApplicationServiceResult<DeleteRoleDtoModel?> appResult, AcademyRole? role) =
					await GetRoleForCrudByIdAsync<DeleteRoleDtoModel>(roleId, Crud.delete);

			return appResult;
		}

		public async Task<ApplicationServiceResult<UpdateRoleDtoModel?>> FindUpdateRoleByIdAsync(string? roleId)
		{
			(ApplicationServiceResult<UpdateRoleDtoModel?> appResult, AcademyRole? role) =
				await GetRoleForCrudByIdAsync<UpdateRoleDtoModel>(roleId, Crud.update);

			return appResult;
		}

		public ApplicationServiceResult<IReadOnlyCollection<RoleDtoModel>> ReadAllRoles()
		{
			ApplicationServiceResult<IReadOnlyCollection<RoleDtoModel>> appResult = _roleResponse.GetRolesListResult<RoleDtoModel>();
			try
			{
				List<AcademyRole> roles = _roleManager.Roles.ToList();
				appResult.AddResult(_mapper.Map<IReadOnlyCollection<RoleDtoModel>>(roles));

				return appResult;
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error in reading roles with error message: {ex.Message}");
				_messageMaker.SetMessage(Crud.read, HttpStatusCode.BadRequest);
				appResult.AddError(_messageMaker.Message);
				return appResult;
			}
		}

		public async Task<ApplicationServiceResult<RoleDtoModel?>> Update(UpdateRoleDtoModel model)
		{
			ApplicationServiceResult<RoleDtoModel?> appResult = _roleResponse.GetRoleEntityResult<RoleDtoModel>();
			ApplicationServiceResult<RoleDtoModel?> validateResult = await ValidateRoleModelAsync(model);
			if (!validateResult.IsSuccess) return validateResult;

			(ApplicationServiceResult<UpdateRoleDtoModel?> result, AcademyRole? role) =
				await GetRoleForCrudByIdAsync<UpdateRoleDtoModel>(model.Id, Crud.update);
			if (!result.IsSuccess || role is null)
			{
				appResult.AddErrorsList(result.Errors.ToArray());
				return appResult;
			}

			role.Name = model.Name;
			role.Description = model.Description;
			IdentityResult identityResult = await _roleManager.UpdateAsync(role);
			return SetRoleResult(identityResult, appResult, Crud.update);
		}

		private async Task<ApplicationServiceResult<RoleDtoModel?>> ValidateRoleModelAsync(BaseCrudRoleDtoModel model)
		{
			ApplicationServiceResult<RoleDtoModel?> validateResult = _roleResponse.GetRoleEntityResult<RoleDtoModel>();

			ValidationResult validationResult = await _roleValidator.ValidateModelAsync(model);
			if (!validationResult.IsValid)
			{
				var validationErrors = validationResult.GetValidationResultErrors();
				validateResult.AddErrorsList(validationErrors.ToArray());
			}

			return validateResult;
		}
	}
}
