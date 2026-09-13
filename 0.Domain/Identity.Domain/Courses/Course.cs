using Identity.Domain.Categories;
using Identity.Domain.Common;
using Identity.Domain.Teachers;

namespace Identity.Domain.Courses
{
    public class Course : BaseEntity
    {
        public string Title { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string Description { get; set; } = string.Empty;

        public string Image { get; set; } = string.Empty;

        public long CategoryId { get; set; }
        public Category? Category { get; set; }

        public Discount? Discount { get; set; }

        public ICollection<Tag> Tags { get; set; } = [];

        public ICollection<Teacher> Teachers { get; set; } = [];

        public ICollection<Comment> Comments { get; set; } = [];
    }
}
