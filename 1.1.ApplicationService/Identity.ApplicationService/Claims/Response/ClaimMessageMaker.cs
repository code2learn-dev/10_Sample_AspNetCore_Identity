namespace Identity.ApplicationService.Claims.Response
{
    public class ClaimMessageMaker : IClaimMessageMaker
	{
		private string _message;

		public string Message => _message; 

        public void SetMessage(ClaimCrudType crudType, HttpStatusCode status)
        {
			_message = crudType switch
			{
				ClaimCrudType.add => status switch
				{
					HttpStatusCode.OK => "claim با موفقیت اضافه شد",
					HttpStatusCode.BadRequest => "خطا در ثبت claim جدید",
					_ => "خطا در ثبت claim"
				},
				ClaimCrudType.replace => status switch
				{
					HttpStatusCode.OK => "claim با موفقیت ویرایش گردید",
					HttpStatusCode.BadRequest => "خطا در ویرایش claim",
					HttpStatusCode.NotFound => "claim یافت نشد",
					_ => "خطا در ویرایش claim"
				},
				ClaimCrudType.delete => status switch
				{
					HttpStatusCode.OK => "claim با موفقیت حذف گردید",
					HttpStatusCode.BadRequest => "خطا در حذف claim",
					HttpStatusCode.NotFound => "claim یافت نشد",
					_ => "خطا در حذف claim"
				},
				ClaimCrudType.read => status switch
				{
					HttpStatusCode.NotFound => "claim یافت نشد",
					_ => "خطا در خواندن جزئیات claim"
				},

				_ => ""
			};
		}
    }
}
