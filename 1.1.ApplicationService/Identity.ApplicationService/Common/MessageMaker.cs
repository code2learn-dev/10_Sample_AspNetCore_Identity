using Identity.ApplicationService.Contracts;
using System.Net;

namespace Identity.ApplicationService.Common
{
    public abstract class MessageMaker(string entityName) : IMessageMaker
    {
        protected string _message = string.Empty;
        private string _entityName = entityName; 

        public string Message => _message; 

        public virtual void SetMessage(Crud crud, HttpStatusCode status)
        {
            _message = crud switch
            {
                Crud.update => status switch
                {
                    HttpStatusCode.OK => $"{_entityName} با موفقیت ویرایش گردید",
                    HttpStatusCode.NotFound => $"{_entityName}ی یافت نشد",
                    HttpStatusCode.BadRequest => $"خطا در ویرایش {_entityName}",
                    _ => $"{_entityName}ی برای ویرایش یافت نشد"
                },
                Crud.create => status switch
                {
                    HttpStatusCode.OK => $"{_entityName} جدید با موفقیت ذخیره گردید",
                    HttpStatusCode.BadRequest => $"خطا در ثبت {_entityName} جدید",
                    _ => $"{_entityName}ی برای ذخیره یافت نشد"
                },
                Crud.delete => status switch
                {
                    HttpStatusCode.OK => $"{_entityName} با موفقیت حذف گردید",
                    HttpStatusCode.BadRequest => $"خطا در حذف {_entityName}",
                    HttpStatusCode.NotFound => $"{_entityName}ی برای حذف یافت نشد",
                    _ => $"{_entityName}ی یافت نشد"
                },
                Crud.find => status switch
                {
                    HttpStatusCode.NotFound => $"{_entityName}ی یافت نشد",
                    HttpStatusCode.BadRequest => $"خطا در بازیابی {_entityName}"
                },
                Crud.read => status switch
                {
                    HttpStatusCode.OK => string.Empty,
                    HttpStatusCode.BadRequest => $"خطا در بازیابی لیست {_entityName}",
                    HttpStatusCode.NotFound => $"لیست {_entityName} یافت نشد",
                    _ => $"{_entityName}ی ثبت نشده است"
                },
                _ => string.Empty,
            };
        }
    }
}