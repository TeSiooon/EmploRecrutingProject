using EmploRecrutingProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmploRecrutingProject.Infrastructure.EntitiesConfiguration;

public class VacationConfiguration : IEntityTypeConfiguration<Vacation>
{
    public void Configure(EntityTypeBuilder<Vacation> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.DateSince)
            .IsRequired();

        builder.Property(v => v.DateUntil)
            .IsRequired();

        builder.Property(v => v.NumberOfHours)
            .IsRequired();

        builder.Property(v => v.IsPartialVacation)
            .IsRequired();

        // FK for Employee
        builder.Property(v => v.EmployeeId)
            .IsRequired();

        builder.HasOne(v => v.Employee)
               .WithMany(e => e.Vacations)
               .HasForeignKey(v => v.EmployeeId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
