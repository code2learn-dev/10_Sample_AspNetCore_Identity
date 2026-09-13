using Identity.Domain.Categories;
using Identity.Domain.Courses;
using Identity.Domain.Teachers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Identity.Domain.IDentityContent
{
    public class AcademyDbContext : IdentityDbContext<AcademyUser, AcademyRole, string>
    {
        public DbSet<Category>  Categories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Degree>Degrees { get; set; }

        public AcademyDbContext(DbContextOptions<AcademyDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(local);Initial Catalog=sample_aspnetcore_identity_academy;TrustServerCertificate=True;Integrated Security=True;MultipleActiveResultSets=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(),
                options => options.GetInterfaces().Any(a =>
                    a.IsGenericType && a.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));
        }
    }
}
