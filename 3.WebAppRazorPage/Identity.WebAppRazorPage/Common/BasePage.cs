namespace Identity.WebAppRazorPage.Common
{
    public abstract class BasePage : PageModel
    {
        protected readonly IMapper _mapper;

        protected const string IndexPage = "Index";


        protected BasePage(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
