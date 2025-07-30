using EmploRecrutingProject.Common.Common;

namespace EmploRecrutingProject.Infrastructure.Persistance;

public class UnitOfWork : IUnitOfWork
{
    private readonly EmploRecrutingProjectDbContext dbContext;

    public UnitOfWork(EmploRecrutingProjectDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }
}
