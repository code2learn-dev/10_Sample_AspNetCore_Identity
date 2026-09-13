using Identity.Domain.IDentityContent;
using System.Security.Claims;

namespace Identity.WebAppRazorPage.ViewComponents
{
    public class NavbarUserIdentityStateViewComponent : ViewComponent
    {
        private readonly UserManager<AcademyUser> _userManager;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMapper _mapper;

        public NavbarUserIdentityStateViewComponent(
            UserManager<AcademyUser> userManager,
            IHttpContextAccessor httpContext,
            IMapper mapper)
        {
            _userManager = userManager;
            _httpContext = httpContext;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ClaimsPrincipal? currentUser = _httpContext.HttpContext?.User;
            if (currentUser is null) return View("Denied");

            AcademyUser? user = await _userManager.GetUserAsync(currentUser);
            if (user is null) return View("Denied");
             
            if((currentUser.Identity?.IsAuthenticated ?? false) && (await _userManager.IsInRoleAsync(user, "member")))
            {
                UserDtoModel userDtoModel = _mapper.Map<UserDtoModel>(user);
                return View(userDtoModel);
            }

            return View("Denied");
        }
    }
}
