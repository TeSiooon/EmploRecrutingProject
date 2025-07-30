using EmploRecrutingProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmploRecrutingProject.Infrastructure.EntitiesConfiguration;

public class VacationPackageConfiguration : IEntityTypeConfiguration<VacationPackage>
{
    public void Configure(EntityTypeBuilder<VacationPackage> builder)
    {
        builder.HasKey(vp => vp.Id);

        builder.Property(vp => vp.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(vp => vp.GrantedDays)
            .IsRequired();

        builder.Property(vp => vp.Year)
         .IsRequired();

        // 1 VacationPackage -> * Employees
        builder.HasMany(vp => vp.Employees)
            .WithOne(e => e.VacationPackage)
            .HasForeignKey(e => e.VacationPackageId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
