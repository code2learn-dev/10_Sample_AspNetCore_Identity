using Identity.Domain.Teachers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Domain.Courses
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(a => a.Price)
                .HasPrecision(14, 0);

            builder.HasOne(a => a.Discount)
                .WithOne()
                .HasForeignKey<Discount>(d => d.CourseId);

            builder.HasMany(a => a.Comments)
                .WithOne()
                .HasForeignKey(f => f.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.Tags)
                .WithMany(t => t.Courses)
                .UsingEntity<CourseTag>(
                    t => t.HasOne(d => d.Tag).WithMany().HasForeignKey(f => f.TagId),
                    c => c.HasOne(d => d.Course).WithMany().HasForeignKey(f => f.CourseId),
                    k => k.HasKey(d => new {d.CourseId, d.TagId}));

            builder.HasMany(c => c.Teachers)
                .WithMany(t => t.Courses)
                .UsingEntity<TeacherCourse>(
                    t => t.HasOne(d => d.Teacher).WithMany().HasForeignKey(f => f.TeacherId),
                    c => c.HasOne(d => d.Course).WithMany().HasForeignKey(f => f.CourseId),
                    k => k.HasKey(d => new {d.CourseId, d.TeacherId}));

        }
    }
}
