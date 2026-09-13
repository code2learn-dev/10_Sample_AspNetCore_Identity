using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Domain.Teachers
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasOne(a => a.Degree)
                .WithOne()
                .HasForeignKey<Degree>(f => f.TeacherId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(a => a.NationaCode).HasPrecision(10, 0);
        }
    }
}
