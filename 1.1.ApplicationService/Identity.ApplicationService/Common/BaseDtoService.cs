namespace Identity.ApplicationService.Common
{
	public abstract class BaseDtoService<
		TService,
		TEntity,
		TEntityDtoModel,
		TCreateEntityDtoModel,
		TUpdateEntityDtoModel,
		TDeleteEntityDtoModel>

		: IBaseDtoService<
			TService,
			TEntity,
			TEntityDtoModel,
			TCreateEntityDtoModel,
			TUpdateEntityDtoModel,
			TDeleteEntityDtoModel>

		where TService : class
		where TEntity : BaseEntity
		where TEntityDtoModel : BaseEntityDto, new()
		where TCreateEntityDtoModel : BaseEntityDto
		where TUpdateEntityDtoModel : BaseEntityDto
		where TDeleteEntityDtoModel : BaseEntityDto 
	{
		protected readonly IGenericRepository<TEntity> _repository;
		protected readonly IServiceResponse _serviceResponse;
		protected readonly IMapper _mapper;
		protected readonly IMessageMaker _messageMaker;
		protected readonly ILogger<TService> _logger;
		protected readonly IModelValidator _modelValidator;

		protected BaseDtoService(
			IGenericRepository<TEntity> repository,
			IServiceResponse serviceResponse,
			IMapper mapper,
			IMessageMaker messageMaker,
			ILogger<TService> logger,
			IModelValidator modelValidator)
		{
			_repository = repository;
			_serviceResponse = serviceResponse;
			_mapper = mapper;
			_messageMaker = messageMaker;
			_logger = logger;
			_modelValidator = modelValidator;
		}

		protected virtual async Task<ApplicationServiceResult<TEntityDtoModel?>> ValidateModelAsync<TCrudEntityDtoModel>(
			TCrudEntityDtoModel? model,
			string errorMessage = "داده های ارسالی نامعتبر می باشد")
			where TCrudEntityDtoModel : BaseEntityDto
		{
			ApplicationServiceResult<TEntityDtoModel?> result = _serviceResponse.GetEntityResultResponse<TEntityDtoModel>();

			if (model is null)
			{
				result.AddError(errorMessage);
				return result;
			}
			var validationResult = await _modelValidator.ValidateModelAsync(model);
			if (!validationResult.IsValid)
			{
				List<string> modelErrors = validationResult.GetValidationResultErrors();
				result.AddErrorsList([.. modelErrors]);
				return result;
			}

			return result;
		}

		public virtual ApplicationServiceResult<IReadOnlyCollection<TEntityDtoModel>> GetAllEntityDtos()
		{
			ApplicationServiceResult<IReadOnlyCollection<TEntityDtoModel>> appServiceResult = 
				_serviceResponse.GetAllEntitiesResultResponse<TEntityDtoModel>();
			try
			{
				IEnumerable<TEntity> entites = _repository.GetAll();
				appServiceResult.AddResult(_mapper.Map<IReadOnlyCollection<TEntityDtoModel>>(entites));
				return appServiceResult;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				_messageMaker.SetMessage(Crud.read, HttpStatusCode.BadRequest);
				appServiceResult.AddError(_messageMaker.Message);
				return appServiceResult;
			}
		}

		public virtual async Task<ApplicationServiceResult<IReadOnlyCollection<TEntityDtoModel>>> GetAllEntityDtosAsync(string errorMessage = "خطا در خواندن لیست داده ها")
		{
			ApplicationServiceResult<IReadOnlyCollection<TEntityDtoModel>> appServiceResult = _serviceResponse.GetAllEntitiesResultResponse<TEntityDtoModel>();
			try
			{
				IEnumerable<TEntity> entites = await _repository.GetAllAsync();
				appServiceResult.AddResult(_mapper.Map<IReadOnlyCollection<TEntityDtoModel>>(entites));
				return appServiceResult;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				_messageMaker.SetMessage(Crud.read, HttpStatusCode.BadRequest);
				appServiceResult.AddError(_messageMaker.Message);
				return appServiceResult;
			}
		}

		public virtual async Task<ApplicationServiceResult<TEntityDtoModel?>> AddEntityDtoAsync(TCreateEntityDtoModel? model)
		{
			ApplicationServiceResult<TEntityDtoModel?> appServiceResult = await ValidateModelAsync(model);
			if (appServiceResult.IsFail) return appServiceResult;

			TEntity? entity = await _repository.AddAsync(_mapper.Map<TEntity>(model));
			if (entity is not null)
			{
				_messageMaker.SetMessage(Crud.create, HttpStatusCode.OK);
				appServiceResult.AddMessage(_messageMaker.Message);
			}
			else
			{
				_messageMaker.SetMessage(Crud.create, HttpStatusCode.BadRequest);
				appServiceResult.AddError(_messageMaker.Message);
			}

			return appServiceResult;

		}

		public virtual async Task<ApplicationServiceResult<TEntityDtoModel?>> UpdateEntityDtoAsync(TUpdateEntityDtoModel? model)
		{
            var (ServiceResult, Entity) = await CheckEntityModelExistByIdAsync<TEntityDtoModel>(model?.Id ?? 0, Crud.update);
			if (ServiceResult.IsFail || Entity is null) return ServiceResult;

            ApplicationServiceResult<TEntityDtoModel?> validateResult = await ValidateModelAsync(model);
			if (!validateResult.IsSuccess) return validateResult;

			TEntity? updatedEntity = await _repository.UpdateAsync(_mapper.Map<TEntity>(model));
			return SetApplicationResult(updatedEntity, Crud.update, ServiceResult);
		}

		public virtual async Task<ApplicationServiceResult<TEntityDtoModel?>> DeleteEntityDtoAsync(long? id)
		{
            var (ServiceResult, Entity) = await CheckEntityModelExistByIdAsync<TEntityDtoModel>(id, Crud.delete);
			if (ServiceResult.IsFail || Entity is null) return ServiceResult; 
			
			TEntity? deletedEntity = await _repository.DeleteAsync(Entity);
			return SetApplicationResult(deletedEntity, Crud.delete, ServiceResult);
		}

        public virtual async Task<ApplicationServiceResult<TEntityDtoModel?>> FindByIdEntityDtoAsync(long? id)
        {
            var (ServiceResult, Entity) = await CheckEntityModelExistByIdAsync<TEntityDtoModel>(id, Crud.find);
			return ServiceResult;
        }

        public virtual ApplicationServiceResult<TEntityDtoModel?> FindByIdEntityDto(long? id)
		{
            var (ServiceResult, entity) = CheckEntityModelExistById<TEntityDtoModel>(id, Crud.find, false);
			return ServiceResult;
		}

		public virtual async Task<ApplicationServiceResult<TUpdateEntityDtoModel?>> FindByIdUpdateEntityDtoAsync(long? id)
		{
			var (serviceResult, entity) = await CheckEntityModelExistByIdAsync<TUpdateEntityDtoModel>(id, Crud.find);
			return serviceResult;
		}

		public virtual async Task<ApplicationServiceResult<TDeleteEntityDtoModel?>> FindByIdDeleteEntityDtoAsync(long? id)
		{
			var (serviceResult, entity) = await CheckEntityModelExistByIdAsync<TDeleteEntityDtoModel>(id, Crud.find);
			return serviceResult;
		}

		/// <summary>
		/// Check if entity exist with specified Id and generate desired response message
		/// by the CRUD operation type
		/// </summary>
		/// <param name="id">Entity Id</param>
		/// <param name="crudType">CRUD operation type (create, update, delete, ...)</param>
		/// <param name="asyncRun">Determine the asyncronous state for finding entity</param>
		/// <returns>Return an instance of ApplicationServiceResult</returns>
		protected virtual async Task<(ApplicationServiceResult<TEntityDtoResult?> ServiceResult, TEntity? Entity)> 
				CheckEntityModelExistByIdAsync<TEntityDtoResult>(long? id, Crud crudType)
				where TEntityDtoResult : BaseEntityDto
		{
            ApplicationServiceResult<TEntityDtoResult?> appServiceResult = _serviceResponse.GetEntityResultResponse<TEntityDtoResult>();

			if(id is null or <= 0)
			{
				_messageMaker.SetMessage(crudType, HttpStatusCode.NotFound);
				appServiceResult.AddError(_messageMaker.Message);
				return (appServiceResult, default);
			}

			TEntity? entity = await _repository.FindByIdAsync(id ?? 0);

			if(entity is null)
			{
				_messageMaker.SetMessage(crudType, HttpStatusCode.NotFound);
				appServiceResult.AddError(_messageMaker.Message);
				return (appServiceResult, entity);
			}

			appServiceResult.AddResult(_mapper.Map<TEntityDtoResult>(entity));
			return (appServiceResult, entity);
		}


		protected virtual (ApplicationServiceResult<TEntityDtoResult?> ServiceResult, TEntity? entity)
				CheckEntityModelExistById<TEntityDtoResult>(
										long? id, 
										Crud crudType, 
										bool asyncRun = true)
				where TEntityDtoResult : BaseEntityDto
		{
            ApplicationServiceResult<TEntityDtoResult?> appServiceResult = _serviceResponse.GetEntityResultResponse<TEntityDtoResult>();
			if(id is null or <= 0)
			{
				_messageMaker.SetMessage(crudType, HttpStatusCode.NotFound);
				appServiceResult.AddError(_messageMaker.Message);
				return (appServiceResult, null);
			}

            TEntity? entity = _repository.FindById(id ?? 0);
			if(entity is null)
			{
				_messageMaker.SetMessage(crudType, HttpStatusCode.NotFound);
				appServiceResult.AddError(_messageMaker.Message);
				return (appServiceResult, null);
			}

			appServiceResult.AddResult(_mapper.Map<TEntityDtoResult>(entity));
			return (appServiceResult, entity);
		}

		/// <summary>
		/// Specify CRUD operations like update and create, return value based on the returned entity
		/// from repository operation has value or null and specify the desaired return message by
		/// CRUD operation if the returned entity has value or not
		/// </summary>
		/// <param name="entity">Returned entity from CRUD operation</param>
		/// <param name="crudType">Specify CRUD operation (e.x: update, create, delete, ...)</param>
		/// <param name="appServiceResult">if an instance of it has already been created for one unit iperation then
		///  reuse that and prevent create double instance of ApplicationServiceResult with different outputs</param>
		/// <returns></returns>
		protected virtual ApplicationServiceResult<TEntityDtoResult?> 
			SetApplicationResult<TEntityDtoResult>(
				TEntity? entity, 
				Crud crudType,
				ApplicationServiceResult<TEntityDtoResult?>? appServiceResult = null)
				where TEntityDtoResult : BaseEntityDto
		{
            appServiceResult = appServiceResult is null ? _serviceResponse.GetEntityResultResponse<TEntityDtoResult>() : appServiceResult;

			if(entity is not null)
			{
				_messageMaker.SetMessage(crudType, HttpStatusCode.OK);
				appServiceResult.AddMessage(_messageMaker.Message);
			}
			else
			{
				_messageMaker.SetMessage(crudType, HttpStatusCode.BadRequest);
				appServiceResult.AddError(_messageMaker.Message);
			}

			appServiceResult.AddResult(_mapper.Map<TEntityDtoResult>(entity));
			return appServiceResult;
		}  
    }
}
