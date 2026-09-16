namespace Identity.ApplicationService.Tokens.Services
{
	public class TokenService : ITokenService
	{
		private readonly UserManager<AcademyUser> _userManager;
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;
		private readonly ILogger<TokenService> _logger;
		private JwtSectionConfiguration _jwt;

		public TokenService(
			UserManager<AcademyUser> userManager,
			IUserRepository userRepository,
			IMapper mapper,
			ILogger<TokenService> logger,
			IOptionsMonitor<JwtSectionConfiguration> monitor)
		{
			_userManager = userManager;
			_userRepository = userRepository;
			_mapper = mapper;
			_logger = logger;
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

		public async Task<ApplicationServiceResult<UserTokenDtoModel?>> GenerateTokenAsync(UserTokenDtoModel model)
		{
			ApplicationServiceResult<UserTokenDtoModel?> tokenResult = new();

			AcademyUser? user = await _userManager.FindByIdAsync(model.UserId);
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

			var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
			var refreshToken = Guid.NewGuid().ToString();
			var expireRefreshToken = DateTime.Now.AddDays(21);

			model.RefreshToken = refreshToken;
			model.ExpireRefreshToken = expireRefreshToken;
			UserToken? userToken = await _userRepository.AddUserToken(_mapper.Map<UserToken>(userTokenDto));
			if (userToken is null) 
				tokenResult.AddError("خطا در ایجاد و ذخیره توکن"); 
			else 
				tokenResult.AddResult(model); 

			return tokenResult;
		}

	}
}
