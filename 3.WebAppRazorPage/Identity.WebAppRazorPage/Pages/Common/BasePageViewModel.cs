namespace Identity.WebAppRazorPage.Pages.Common
{
	public class BasePageViewModel : PageModel
	{
		protected readonly IMapper _mapper;

        public BasePageViewModel(IMapper mapper)
        {
            _mapper = mapper;
        }

        protected virtual void PrintMessages<TResult>(ApplicationServiceResult<TResult> appServiceResult, Alert alert = Alert.success)
		{
			if (appServiceResult.IsSuccess) return;
			appServiceResult.Errors.MappMessages(Alert.danger);
		} 
	}
}
