using Identity.ApplicationService.Common;

namespace Identity.ApplicationService.Categories.Entites
{
    public class CrudCategoryDto : BaseEntityDto
    {
        public string Title { get; set; } = string.Empty;
    }
}
