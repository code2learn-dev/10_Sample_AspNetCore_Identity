using Identity.Domain.Common;

namespace Identity.Domain.Teachers
{
    public class Degree : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public Grade Grade { get; set; } = Grade.دیپلم;
        public int GraduteYear { get; set; }
        public decimal Average { get; set; }
        public string Image { get; set; } = string.Empty;

        public long TeacherId { get; set; }
    }

    public enum Grade : byte
    {
        دیپلم = 1,
        کاردانی,
        کارشناسی,
        کارشناسی_ارشد,
        دکتری
    }
}
