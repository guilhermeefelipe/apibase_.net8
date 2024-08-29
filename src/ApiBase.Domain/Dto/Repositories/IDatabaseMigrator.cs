namespace ApiBase.Repositories;

public interface IDatabaseMigrator
{
    Task MigrateAsync();
}