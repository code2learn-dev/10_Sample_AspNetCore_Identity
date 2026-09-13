namespace Identity.ApplicationService.Courses.Response
{
	public class CourseMessageMaker() : MessageMaker("دوره آموزشی"), ICourseMessageMaker
	{
		public override void SetMessage(Crud crud, HttpStatusCode status)
		{
			_message = crud switch
			{
				Crud.create => status switch
				{
					HttpStatusCode.OK => "دسته آموزشی با موفقیت ذخیره گردید",
					HttpStatusCode.BadRequest => "خطا در ثبت دسته آموزشی جدید",
					_ => "دوره آموزشی برای ثبت یافت نشد"
				},

				Crud.update => status switch
				{
					HttpStatusCode.OK => "دوره آموزشی با موفقیت ویرایش گردید",
					HttpStatusCode.BadRequest => "خطا در ویرایش دوره آموزشی",
					HttpStatusCode.NotFound => "دوره آموزشی برای ویرایش یافت نشد",
					_ => "دوره آموزشی برای ویرایش یافت نشد"
				},

				Crud.delete => status switch
				{
					HttpStatusCode.OK => "دوره آموزشی برای حذف یافت نشد",
					HttpStatusCode.BadRequest => "خطا در حذف دوره آموزشی",
					_ => "دوره آموزشی برای حذف یافت نشد"
				},

				Crud.read when status is HttpStatusCode.BadRequest => "خطا در خواندن لیست دوره آموزشی",

				Crud.find => status switch
				{
					HttpStatusCode.BadRequest => "خطا در یافتن دوره آموزشی",
					_ => "دوره آموزشی یافت نشد"
				},

				_ => "دوره آموزشی یافت نشد"
			};
		}
	}
}
