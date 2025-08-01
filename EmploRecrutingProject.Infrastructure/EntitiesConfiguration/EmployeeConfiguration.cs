using EmploRecrutingProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmploRecrutingProject.Infrastructure.EntitiesConfiguration;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        // FK for Team
        builder.Property(e => e.TeamId)
               .IsRequired(required: false);

        builder.HasOne(e => e.Team)
               .WithMany(t => t.Employees)
               .HasForeignKey(e => e.TeamId)
               .OnDelete(DeleteBehavior.Restrict);

        // FK for VacationPackage
        builder.Property(e => e.VacationPackageId)
            .IsRequired(required: false);

        builder.HasOne(e => e.VacationPackage)
               .WithMany(vp => vp.Employees)
               .HasForeignKey(e => e.VacationPackageId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Vacations)
               .WithOne(v => v.Employee)
               .HasForeignKey(v => v.EmployeeId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
