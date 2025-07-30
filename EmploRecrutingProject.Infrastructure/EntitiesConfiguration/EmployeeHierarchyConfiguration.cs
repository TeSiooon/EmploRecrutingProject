using EmploRecrutingProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmploRecrutingProject.Infrastructure.EntitiesConfiguration;

public class EmployeeHierarchyConfiguration : IEntityTypeConfiguration<EmployeeHierarchy>
{
    public void Configure(EntityTypeBuilder<EmployeeHierarchy> builder)
    {
        builder.HasKey(eh => new { eh.EmployeeId, eh.SuperiorId });

        builder.Property(eh => eh.RelationLevel)
            .IsRequired();

        builder.HasOne(eh => eh.Employee)
            .WithMany(e => e.HierarchyChildren)
            .HasForeignKey(eh => eh.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(eh => eh.Superior)
            .WithMany(e => e.HierarchyParents)
            .HasForeignKey(eh => eh.SuperiorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
