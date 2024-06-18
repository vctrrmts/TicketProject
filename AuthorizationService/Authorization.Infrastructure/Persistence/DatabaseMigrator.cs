using Microsoft.EntityFrameworkCore;
using Authorization.Application.Abstractions.Persistence;

namespace Authorization.Infrastructure.Persistence;

internal sealed class DatabaseMigrator : IDatabaseMigrator
{
    private readonly ApplicationDbContext _dbApplicationDbContext;

    public DatabaseMigrator(ApplicationDbContext dbApplicationDbContext)
    {
        _dbApplicationDbContext = dbApplicationDbContext;
    }

    public Task MigrateAsync(CancellationToken cancellationToken)
    {
        return _dbApplicationDbContext.Database.MigrateAsync(cancellationToken);
    }

    public void Migrate()
    {
        _dbApplicationDbContext.Database.Migrate();
    }

    public IEnumerable<string> GetPendingMigrations()
    {
        return _dbApplicationDbContext.Database.GetPendingMigrations();
    }
}