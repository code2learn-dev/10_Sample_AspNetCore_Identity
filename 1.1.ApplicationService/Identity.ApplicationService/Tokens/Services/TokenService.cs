namespace Identity.ApplicationService.Tokens.Services
{
	public class TokenService : ITokenService
	{
		private readonly UserManager<AcademyUser> _userManager;
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;
		private readonly ILogger<TokenService> _logger;
		private readonly IAccountModelValidator _validator;
		private JwtSectionConfiguration _jwt;

        public TokenService(
            UserManager<AcademyUser> userManager,
            IUserRepository userRepository,
            IMapper mapper,
            ILogger<TokenService> logger,
            IOptionsMonitor<JwtSectionConfiguration> monitor,
            IAccountModelValidator validator)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _validator = validator;

            _jwt = monitor.CurrentValue;
            monitor.OnChange(update =>
            {
                _jwt = update;
            });
        }

        public async Task<ApplicationServiceResult<bool>> ValidateToken(TokenValidatedContext? context)
		{
			ApplicationServiceResult<bool> tokenResult = new();
			bool isValid = false;
			tokenResult.AddResult(isValid);

			if (context?.Principal?.Identity is not ClaimsIdentity claimsIdentity ||
				!claimsIdentity.Claims.Any())
			{
				_logger.LogError("user claim not found");
				tokenResult.AddError("کاربری یافت نشد");
				return tokenResult;
			}

			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
			var userName = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value ?? "";
			if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(userName))
			{
				_logger.LogError("user claims not found");
				tokenResult.AddError("کاربری یافت نشد");
				return tokenResult;
			}

			var user = await _userManager.FindByNameAsync(userName);
			if (user is null)
			{
				_logger.LogError("user not found");
				tokenResult.AddError("کاربری یافت نشد");
				return tokenResult;
			}

			isValid = true;
			tokenResult.AddResult(isValid);
			return tokenResult;
		}

		public async Task<ApplicationServiceResult<UserTokenDtoModel?>> GenerateTokenAsync(string userId)
		{
			ApplicationServiceResult<UserTokenDtoModel?> tokenResult = new();

			AcademyUser? user = await _userManager.FindByIdAsync(userId);
			if (user is null ||
				string.IsNullOrEmpty(user.UserName))
			{
				_logger.LogError("user not found");
				tokenResult.AddError("کاربری یافت نشد");
				return tokenResult;
			}

			IReadOnlyCollection<Claim> claims = new List<Claim>()
			{
				new(ClaimTypes.NameIdentifier, user.Id),
				new(ClaimTypes.Name, user.UserName)
			};

			if (string.IsNullOrEmpty(_jwt.Issuer) ||
				string.IsNullOrEmpty(_jwt.Audience) ||
				string.IsNullOrEmpty(_jwt.Key))
			{
				tokenResult.AddError("دادهای کاربر یافت نشد");
				_logger.LogError("jwt section not found");
				return tokenResult;
			}

			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
			var expireToken = DateTime.Now.AddDays(7);

			var jwtToken = new JwtSecurityToken(
				issuer: _jwt.Issuer,
				audience: _jwt.Audience,
				expires: expireToken,
				notBefore: DateTime.Now,
				claims: claims,
				signingCredentials: credentials);

			var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

			// create refresh token
			var refreshTokenPlain = Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N");
			var refreshTokenHash = refreshTokenPlain.ConvertToHash();
			var expireRefreshToken = DateTime.Now.AddDays(21);


			// store token
			UserToken token = new()
			{
				UserId = user.Id,
				RefreshToken = refreshTokenHash,
				ExpireRefreshToken = expireRefreshToken,
				DeviceName = "Desktop-Window 10-DKPL-234",
				IsActive = true
			};
			UserToken? userToken = await _userRepository.AddUserToken(token);
			if (userToken is not null)
			{
				UserTokenDtoModel userTOkenDto = new()
				{
					UserId = user.Id,
					Token = accessToken,
					ExpireDate = expireToken,
					RefreshToken = refreshTokenPlain,
					ExpireRefreshToken = expireRefreshToken,
					IsActive = true
				};
				tokenResult.AddResult(userTOkenDto);
			}
			else
				tokenResult.AddError("خطا در ایجاد و ذخیره توکن");

			return tokenResult;
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
				loginResult.AddError("نام کاربری و رمز عبور اشتباه است");
				return loginResult;
			}

			if (!await _userManager.CheckPasswordAsync(user, model.Password))
			{ 
				loginResult.AddError("نام کاربری و رمز عبور اشتباه است");
				return loginResult;
			}

			if (await _userManager.IsInRoleAsync(user, accountRole.ToString()))
			{
				loginResult.AddResult(_mapper.Map<AccountDtoModel>(user));
			}
			else
			{ 
				loginResult.AddError("کاربری یافت نشد");
			}

			return loginResult;
		}

        public async Task<ApplicationServiceResult<UserTokenDtoModel?>> RefreshTokenAsync(RefreshTokenDtoModel model)
        {
			var tokenResult = new ApplicationServiceResult<UserTokenDtoModel?>();

			if(string.IsNullOrWhiteSpace(model.RefreshToken) ||
				string.IsNullOrWhiteSpace(model.AccessToken))
			{
				tokenResult.AddError("توکنی یافت نشد");
				return tokenResult;
			}

			// 1. find refresh token
			var hashedToken = model.RefreshToken.ConvertToHash();
            UserToken? storedRefreshToken = await _userRepository.FindUserTokenByHashedTokenAsync(hashedToken);
			if(storedRefreshToken is null)
			{
				_logger.LogError("Refresh token not found");
				tokenResult.AddError("توکنی یافت نشد");
				return tokenResult;
			}

			// 2. check is it still active and not expired
			if(!storedRefreshToken.IsActive && storedRefreshToken.ExpireRefreshToken < DateTime.UtcNow)
			{
				_logger.LogError("Refresh token is refused");
				tokenResult.AddError("کاربری یافت نشد");
				return tokenResult;
			}

			// 3. check token belongs to the current user
			var handler = new JwtSecurityTokenHandler();
			if(handler.CanReadToken(model.AccessToken))
			{
				var jwtToken = handler.ReadJwtToken(model.AccessToken);
				var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

				if(userId != storedRefreshToken.UserId)
				{
					_logger.LogError("Access token mismatch to the current user");
					tokenResult.AddError("کاربری یافت نشد");
					return tokenResult;
				}
			}


			// 4. Roration: Deactivate the old refresh token
			storedRefreshToken.IsActive = false;
            UserToken? updatedToken = await _userRepository.UpdateUserTokenAsync(storedRefreshToken);
			if(updatedToken is null)
			{
				_logger.LogError("Update refresh token is failed");
				tokenResult.AddError("کاربری یافت نشد");
				return tokenResult;
			}


            // 5. generate new pair tokens
            var newTokenResult = await GenerateTokenAsync(storedRefreshToken.UserId);
			if (!newTokenResult.IsSuccess && newTokenResult.Result is null)
			{
				_logger.LogError("Generate new token is failed");
				tokenResult.AddErrorsList(newTokenResult.Errors.ToArray());
			}
			else
				tokenResult.AddResult(newTokenResult.Result);
			
			return tokenResult;
        }

        public async Task<ApplicationServiceResult<bool>> RevokeRefreshToken(string refreshToken)
        {
			var tokenResult = new ApplicationServiceResult<bool>();

			if(string.IsNullOrWhiteSpace(refreshToken))
			{
				tokenResult.AddError("توکنی یافت نشد");
				return tokenResult;
			}

			// 1. find user token
			var hashedRefreshToken = refreshToken.ConvertToHash();
            UserToken? storedRefreshToken = await _userRepository.FindUserTokenByHashedTokenAsync(hashedRefreshToken);
			if(storedRefreshToken is null)
			{
				_logger.LogError("User token not found");
				tokenResult.AddError("کاربری یافت نشد");
				return tokenResult;
			}


			// 2. diactive stored refresh token
			storedRefreshToken.IsActive = false;
            UserToken? updatedToken = await _userRepository.UpdateUserTokenAsync(storedRefreshToken);
			if(updatedToken is null)
			{
				_logger.LogError("update refresh token has been failed");
				tokenResult.AddError("کاربری یافت نشد");
				return tokenResult;
			}

			tokenResult.AddResult(true);
			return tokenResult;
        }
    }
}
